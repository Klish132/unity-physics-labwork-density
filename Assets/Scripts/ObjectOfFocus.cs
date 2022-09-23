using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOfFocus : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject childOrbit;
    public GameObject focusingPoint = null;
    public bool orthographicOnFocus = false;
    public GameObject[] allowedFocuses;

    MainCameraController cameraControl = null;
    MainInputController inputControl = null;

    private Vector3 lastMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        inputControl = GameObject.Find("MainInputController").GetComponent<MainInputController>();
        cameraControl = GameObject.Find("MainCamera").GetComponent<MainCameraController>();
    }

    public bool CheckIfContainsObject(GameObject go)
    {
        foreach (GameObject obj in allowedFocuses)
        {
            if (obj == go)
                return true;
        }
        return false;
    }

    public void Focus()
    {
        GSC.focusTarget = gameObject;
        mainCamera.transform.parent = childOrbit.transform;
        mainCamera.orthographic = orthographicOnFocus;
        if (focusingPoint == null)
            focusingPoint = gameObject;
        cameraControl.UpdateTransforms(childOrbit, focusingPoint.transform);
        inputControl.UpdateOrbit(childOrbit);
    }

    private void OnMouseDown()
    {
        lastMousePosition = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        Vector3 delta = Input.mousePosition - lastMousePosition;
        if (!GSC.isSelectingSamples && 
            GSC.focusTarget != gameObject &&
            GSC.focusTarget.GetComponent<ObjectOfFocus>().CheckIfContainsObject(gameObject) &&
            delta == Vector3.zero)
        {
            Focus();
        }
    }
}
