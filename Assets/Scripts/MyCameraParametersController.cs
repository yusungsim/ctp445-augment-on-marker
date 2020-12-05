using System;
using System.IO;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ArucoUnity.Cameras.Parameters
{
    /// <summary>
    /// Editor controller for <see cref="CameraParameters"/>.
    /// </summary>
    public class MyCameraParametersController : MonoBehaviour, IHasArucoCameraParameters
    {
        [SerializeField]
        private ARCameraManager cameraManager;

        // IHasCameraParameters properties

        /// <summary>
        /// Gets or sets the camera parameters.
        /// </summary>
        public ArucoCameraParameters CameraParameters { get; set; }
        // Properties


        // Variables
        

        // MonoBehaviour methods

        /// <summary>
        /// Calls <see cref="SetCameraParametersFilePath"/> then <see cref="Load"/> if <see cref="CameraParametersFilePath"/> is set.
        /// </summary>
        protected virtual void Awake()
        {
            cameraManager.frameReceived += OnFrameRecieved;
        }


        // Methods

        /// <summary>
        /// Initializes <see cref="CameraParameters"/> with <see cref="ArucoCameraParameters.ArucoCameraParameters(int)"/>
        /// </summary>
        /// <param name="cameraNumber">The number of cameras in the calibrated camera system.</param>
        public virtual void Initialize(int cameraNumber)
        {
            CameraParameters = new ArucoCameraParameters(cameraNumber);
        }

        void OnFrameRecieved(ARCameraFrameEventArgs eventArgs)
        {
            if (cameraManager.TryGetIntrinsics(out var intrinsics))
            {
                CameraParameters = MyCameraParameters.CreateFromXRCameraIntrinsics(intrinsics);
            }
        }
    }
}