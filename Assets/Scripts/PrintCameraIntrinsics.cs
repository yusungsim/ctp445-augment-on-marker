using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARCameraManager))]
public class PrintCameraIntrinsics : MonoBehaviour
{
    XRCameraIntrinsics? m_CameraIntrinsics;

    void Start()
    {
        GetComponent<ARCameraManager>().frameReceived += OnFrameReceived;
    }

    void OnFrameReceived(ARCameraFrameEventArgs eventArgs)
    {
        if (GetComponent<ARCameraManager>().TryGetIntrinsics(out var intrinsics))
        {
            m_CameraIntrinsics = intrinsics;
        }
        else
        {
            m_CameraIntrinsics = null;
        }
    }

    // Update is called once per frame
    void OnGUI()
    {
        if (m_CameraIntrinsics.HasValue)
        {
            GUI.skin.label.fontSize = 100;
            GUILayout.Label($"Camera intrinsics: {m_CameraIntrinsics.Value.ToString()}");
        }
    }
}