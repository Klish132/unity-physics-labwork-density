using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInputController : MonoBehaviour
{
    public GameObject cameraOrbit;
    OrbitSphere orbitScript = null;

    public float rotateSpeed = 8f;

    private bool restrictHorizontalRotation;

    private float default_scale;
    private float minV;
    private float maxV;
    private float minH;
    private float maxH;

    // Start is called before the first frame update
    void Start()
    {
        orbitScript = cameraOrbit.GetComponent<OrbitSphere>();
        orbitScript.isActive = true;

        restrictHorizontalRotation = orbitScript.restrictHorizontalRotation;

        default_scale = orbitScript.default_scale;
        minV = orbitScript.minV;
        maxV = orbitScript.maxV;
        minH = orbitScript.minH;
        maxH = orbitScript.maxH;
    }

    public void UpdateOrbit(GameObject newOrbit)
    {
        cameraOrbit = newOrbit;
        orbitScript.isActive = false;
        orbitScript = cameraOrbit.GetComponent<OrbitSphere>();
        orbitScript.isActive = true;

        restrictHorizontalRotation = orbitScript.restrictHorizontalRotation;

        default_scale = orbitScript.default_scale;
        minV = orbitScript.minV;
        maxV = orbitScript.maxV;
        minH = orbitScript.minH;
        maxH = orbitScript.maxH;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GSC.isSelectingSamples && !GSC.isDragging){
            if (Input.GetMouseButton(1))
            {
                float h = rotateSpeed * Input.GetAxis("Mouse X");
                float v = rotateSpeed * Input.GetAxis("Mouse Y");

                if (cameraOrbit.transform.eulerAngles.z + v <= minV || cameraOrbit.transform.eulerAngles.z + v >= maxV)
                    v = 0;
                if (restrictHorizontalRotation)
                {
                    if (cameraOrbit.transform.eulerAngles.y + h <= minH || cameraOrbit.transform.eulerAngles.y + h >= maxH)
                        h = 0;
                }


                cameraOrbit.transform.eulerAngles = new Vector3(cameraOrbit.transform.eulerAngles.x, cameraOrbit.transform.eulerAngles.y + h, cameraOrbit.transform.eulerAngles.z + v);
            }

            float scrollFactor = Input.GetAxis("Mouse ScrollWheel");

            if (scrollFactor > 0)
            {
                if (cameraOrbit.transform.localScale.y * (1f - scrollFactor) > default_scale)
                {
                    cameraOrbit.transform.localScale = cameraOrbit.transform.localScale * (1f - scrollFactor);
                }
                else
                {
                    cameraOrbit.transform.localScale = Vector3.one * default_scale;
                }
            }
            if (scrollFactor < 0)
            {
                if (cameraOrbit.transform.localScale.y * (1f - scrollFactor) < default_scale * 2f)
                {
                    cameraOrbit.transform.localScale = cameraOrbit.transform.localScale * (1f - scrollFactor);
                }
                else
                {
                    cameraOrbit.transform.localScale = Vector3.one * default_scale * 2f;
                }
            }
        }
    }
}
