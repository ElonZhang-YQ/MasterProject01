using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * mouse control
         * when right button push on and hold
         * Holding down the right mouse button for dragging preview
         */
        if (Input.GetMouseButton(1))
        {
            float MouseX = Input.GetAxis("Mouse X");
            float MouseY = Input.GetAxis("Mouse Y");
            transform.RotateAround(new Vector3(0, 0, 0), new Vector3(MouseX, -MouseY, 0), Time.deltaTime*100f);
        }
    }
}
