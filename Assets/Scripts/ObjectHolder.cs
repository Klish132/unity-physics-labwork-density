using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    public GameObject heldObject = null;
    public Sample heldObjectScript = null;
    
    public Vector3 rotation = new Vector3(0, 0, 0);
    public GameObject holdingParent = null;

    public Vector3 originalHoldingPosition = Vector3.zero;
    public Vector3 currHoldingPosition = Vector3.zero;

    // 0 - normal, 1 - horizontal, 2 - vertical
    public int holdingOrientantion = 0;

    public virtual void SetObject(GameObject newObject)
    {
        heldObject = newObject;
        heldObjectScript = heldObject.GetComponent<Sample>();
        if (holdingParent == null)
        {
            holdingParent = gameObject;
        }
        heldObject.transform.parent = holdingParent.transform;
        switch (holdingOrientantion)
        {
            case 0:
                currHoldingPosition = originalHoldingPosition;
                break;
            case 1:
                currHoldingPosition = new Vector3(originalHoldingPosition.x, heldObject.transform.localScale.y / 100f, originalHoldingPosition.z);
                break;
            case 2:
                currHoldingPosition = new Vector3(-heldObject.transform.localScale.x / 100f, originalHoldingPosition.y, originalHoldingPosition.z);
                break;
        }
        heldObject.transform.localPosition = currHoldingPosition;
        heldObject.transform.eulerAngles = rotation;
    }
    public virtual void ClearObject()
    {
        heldObject = null;
    }

}
