using ArucoUnity.Plugin;
using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ArucoUnity.Cameras.Parameters
{
    /// <summary>
    /// Manage the camera parameters from a calibration.
    /// </summary>
    [Serializable]
    public class MyCameraParameters : ArucoCameraParameters
    {
        // Constructors

        /// <summary>
        /// Create an empty CameraParameters and set <see cref="CalibrationDateTime"/> to now.
        /// </summary>
        /// <remarks>This constructor is needed for the serialization.</remarks>
        public MyCameraParameters()
        {
        }

        /// <summary>
        /// Initialize the properties.
        /// </summary>
        /// <param name="camerasNumber">The number of camera in the camera system. Must be equal to the number of cameras of the related
        /// <see cref="ArucoCamera"/>.</param>
        public MyCameraParameters(int camerasNumber) : base(camerasNumber)
        {
        }

        // Properties


        // Methods

        /// <summary>
        /// Create a new CameraParameters object from ARCameraManager
        /// </summary>
        public static ArucoCameraParameters CreateFromXRCameraIntrinsics(XRCameraIntrinsics intrinsics)
        {
            ArucoCameraParameters cameraParameters = new ArucoCameraParameters(1);

            cameraParameters.ImageWidths[0] = intrinsics.resolution.x;
            cameraParameters.ImageHeights[0] = intrinsics.resolution.y;

            cameraParameters.ReprojectionErrors[0] = 1.0;

            cameraParameters.CameraMatricesType = Cv.Type.CV_64F;

            double fx = intrinsics.focalLength.x;
            double fy = intrinsics.focalLength.y;
            double cx = intrinsics.principalPoint.x;
            double cy = intrinsics.principalPoint.y;

            cameraParameters.CameraMatricesValues[0] = new double[][]
            {
                new double[] { fx, 0, cx },
                new double[] { 0, fy, cy },
                new double[] { 0, 0, 1 }
            };

            cameraParameters.DistCoeffsType = Cv.Type.CV_64F;

            cameraParameters.DistCoeffsValues[0] = new double[][]
            {
                new double[] { 0, 0, 0, 0, 0 }
            };

            cameraParameters.CameraMatrices = CreateProperty(cameraParameters.CameraMatricesType, cameraParameters.CameraMatricesValues);
            cameraParameters.DistCoeffs = CreateProperty(cameraParameters.DistCoeffsType, cameraParameters.DistCoeffsValues);

            return cameraParameters;
        }

       
    }
}