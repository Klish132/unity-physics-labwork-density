using UnityEngine;
using System.Collections;

public class DraggableObject : MonoBehaviour
{
    public Camera mainCamera;

    public LayerMask orbitLayer;
    public LayerMask draggableLayer;

    public bool isDragging = false;

    public GameObject originalHolder = null;
    private GameObject collidingWith = null;
    private GameObject currentHolder = null;

    private void OnTriggerEnter(Collider other)
    {
        collidingWith = other.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        collidingWith = null;
    }

    public void SetHolders(GameObject newHolder)
    {
        if (currentHolder != null)
            currentHolder.GetComponent<ObjectHolder>().ClearObject();
        ObjectHolder holderScript = newHolder.GetComponent<ObjectHolder>();
        holderScript.SetObject(gameObject);
        currentHolder = newHolder;
    }

    void Update()
    {
        if (!GSC.isSelectingSamples && GSC.focusTarget == GSC.scalesBody && !GSC.preventDrag)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 10000f, draggableLayer))
                {
                    if (hit.transform == transform)
                    {
                        isDragging = true;
                        GSC.isDragging = true;
                        transform.eulerAngles = new Vector3(-90, 0, 0);
                    }
                }
            }
        }

        if (isDragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10000f, ((1 << orbitLayer) | (1 << draggableLayer))))
            {
                Vector3 hitPoint = hit.point;
                transform.position = hitPoint;
            } else
            {
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
            }
        }

        if (isDragging)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (gameObject == GSC.weight)
                {
                    if (collidingWith == GSC.scale)
                    {
                        SetHolders(collidingWith);
                    }
                    else
                    {
                        SetHolders(originalHolder);
                    }
                }
                else
                {
                    if (collidingWith == GSC.scale || collidingWith == GSC.caliper || collidingWith == GSC.micrometer)
                    {
                        SetHolders(collidingWith);
                    }
                    else
                    {
                        SetHolders(originalHolder);
                    }
                }
                isDragging = false;
                GSC.isDragging = false;
            }
        }
        
    }
}