using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrateButton : MonoBehaviour
{
    private Vector3 lastMousePosition;
    //private Vector3 homePosition;

    private void OnMouseDown()
    {
        transform.localPosition += new Vector3(0, -0.01f, -0.01f);
        lastMousePosition = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        Vector3 delta = Input.mousePosition - lastMousePosition;
        if (delta == Vector3.zero)
        {
            //transform.localPosition = homePosition;
            GameObject.Find("ScaleInputController").GetComponent<ScaleInputController>().Calibration();
        }
    }

    //private void Start()
    //{
    //    homePosition = transform.localPosition;
    //}

    //private void OnMouseDrag()
    //{
    //    Vector3 delta = Input.mousePosition - lastMousePosition;
    //    if (delta != Vector3.zero)
    //    {
    //        transform.localPosition = homePosition;
    //    }
    //}
}
