using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ArucoUnity.Cameras
{
    
    public class MyCamController : MonoBehaviour
    {
        [SerializeField]
        private ARCameraManager cameraManager;

        // Events

        /// <summary>
        /// Called when the webcams started.
        /// </summary>
        public Action<MyCamController> Started = delegate { };

        // Properties

        public List<XRCpuImage> Planes { get; private set; }

        /// <summary>
        /// Gets <see cref="Textures"/> converted in Texture2D.
        /// </summary>
        public List<Texture2D> Textures2D
        {
            get;
        }


    }       
}