using System;
using UnityEngine;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ArucoUnity.Cameras
{
    /// <summary>
    /// Captures images of a webcam.
    /// </summary>
    public class ArucoMyCam : ArucoCamera
    {
        // Constants

        protected const int cameraId = 0;

        // Editor fields


        // IArucoCamera properties

        public override int CameraNumber { get { return 1; } }

        public override string Name { get; protected set; }


        // ARCamera

        public ARCameraManager cameraManager;

        Texture2D m_Texture;


        // Properties

        /// <summary>
        /// Gets or set the id of the webcam to use.
        /// </summary>
        //public int WebcamId { get { return webcamId; } set { webcamId = value; } }

        /// <summary>
        /// Gets the controller of the webcam to use.
        /// </summary>
        //public WebcamController WebcamController { get; private set; }

        // MonoBehaviour methods

        /// <summary>
        /// Initializes <see cref="WebcamController"/> and subscribes to.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
        }

        /// <summary>
        /// Unsubscribes to <see cref="WebcamController"/>.
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        // ConfigurableController methods

        /// <summary>
        /// Calls <see cref="WebcamController.Configure"/> and sets <see cref="Name"/>.
        /// </summary>
        protected override void Configuring()
        {
            base.Configuring();
        }

        /// <summary>
        /// Calls <see cref="WebcamController.StartWebcams"/>.
        /// </summary>
        protected override void Starting()
        {
            base.Starting();
        }

        /// <summary>
        /// Calls <see cref="WebcamController.StopWebcams"/>.
        /// </summary>
        protected override void Stopping()
        {
            base.Stopping();
        }

        /// <summary>
        /// Blocks <see cref="ArucoCamera.OnStarted"/> until <see cref="WebcamController.IsStarted"/>.
        /// </summary>
        protected override void OnStarted()
        {
        }

        // ArucoCamera methods

        /// <summary>
        /// Copy current webcam images to <see cref="ArucoCamera.NextImages"/>.
        /// </summary>
        protected override unsafe bool UpdatingImages()
        {
            XRCpuImage image;
            if (!cameraManager.TryAcquireLatestCpuImage(out image))
                return false;

            var conversionParams = new XRCpuImage.ConversionParams
            {
                // Get the entire image
                inputRect = new RectInt(0, 0, image.width, image.height),

                // Downsample by 2
                outputDimensions = new Vector2Int(image.width / 2, image.height / 2),

                // Choose RGBA format
                outputFormat = TextureFormat.RGBA32,

                // Flip across the vertical axis (mirror image)
                transformation = XRCpuImage.Transformation.MirrorY
            };

            // See how many bytes we need to store the final image.
            int size = image.GetConvertedDataSize(conversionParams);

            // Allocate a buffer to store the image
            var buffer = new NativeArray<byte>(size, Allocator.Temp);

            // Extract the image data
            image.Convert(conversionParams, new IntPtr(buffer.GetUnsafePtr()), buffer.Length);

            // The image was converted to RGBA32 format and written into the provided buffer
            // so we can dispose of the CameraImage. We must do this or it will leak resources.
            image.Dispose();

            // At this point, we could process the image, pass it to a computer vision algorithm, etc.
            // In this example, we'll just apply it to a texture to visualize it.

            // We've got the data; let's put it into a texture so we can visualize it.
            m_Texture = new Texture2D(
                conversionParams.outputDimensions.x,
                conversionParams.outputDimensions.y,
                conversionParams.outputFormat,
                false);

            m_Texture.LoadRawTextureData(buffer);
            m_Texture.Apply();

            // Done with our temporary data
            buffer.Dispose();

            Array.Copy(m_Texture.GetRawTextureData(), NextImageDatas[cameraId], ImageDataSizes[cameraId]);

            return true;
        }

        // Methods

        /// <summary>
        /// Configures <see cref="ArucoCamera.Textures"/> and calls <see cref="ArucoCamera.OnStarted"/>.
        /// </summary>
        protected virtual void WebcamController_Started(WebcamController webcamController)
        {
            base.OnStarted();
        }
    }
}