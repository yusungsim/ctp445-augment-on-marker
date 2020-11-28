using System;
using System.IO;
using UnityEngine;

namespace ArucoUnity.Cameras.Parameters
{
    /// <summary>
    /// Editor controller for <see cref="CameraParameters"/>.
    /// </summary>
    public class MyCameraParametersController : MonoBehaviour, IHasArucoCameraParameters
    {
        // Editor fields
        /*
        [SerializeField]
        [Tooltip("Automatically load the camera parameters file at start.")]
        private bool autoLoadFile = true;

        [SerializeField]
        [Tooltip("The folder of the camera parameters file, relative to the Application.persistentDataPath folder.")]
        private string cameraParametersFolderPath = "ArucoUnity/CameraParameters/";

        [SerializeField]
        [Tooltip("The xml file where to load and save the camera parameters.")]
        private string cameraParametersFilename;
        */

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
            
        }

        // MonoBehaviour methods


        // Methods

        /// <summary>
        /// Initializes <see cref="CameraParameters"/> with <see cref="ArucoCameraParameters.ArucoCameraParameters(int)"/>
        /// </summary>
        /// <param name="cameraNumber">The number of cameras in the calibrated camera system.</param>
        public virtual void Initialize(int cameraNumber)
        {
            CameraParameters = new ArucoCameraParameters(cameraNumber);
        }

    }
}