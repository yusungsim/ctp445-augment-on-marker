using ArucoUnity.Cameras.Parameters;
using UnityEngine;

public class PrintArucoCameraParameters : MonoBehaviour
{
    public ArucoCameraParametersController m_controller;

    // Start is called before the first frame update
    void Start()
    {
        ArucoCameraParameters camParams = m_controller.CameraParameters;
        Debug.Log($"ImageHeights[0] = {camParams.ImageHeights[0]}");
        Debug.Log($"ImageWidths[0] = {camParams.ImageWidths[0]}");

        Debug.Log("CameraMatricesValues : [");
        foreach ( double[] row in camParams.CameraMatricesValues[0])
        {
            Debug.Log("[");
            foreach (double v in row)
            {
                Debug.Log(v);
            }
            Debug.Log("]");
        }
        Debug.Log("]"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
