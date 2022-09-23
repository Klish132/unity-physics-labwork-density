using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitSphere : MonoBehaviour
{
    public Transform orbitTarget;
    public float default_scale = 2.5f;

    public bool isActive = false;
    public bool restrictHorizontalRotation = true;

    public float minV;
    public float maxV;
    public float minH;
    public float maxH;

    // Start is called before the first frame update
    void Start()
    {
        //transform.localPosition = orbitTarget.transform.localPosition;
        transform.localScale = new Vector3(default_scale, default_scale, default_scale);

    }
}
