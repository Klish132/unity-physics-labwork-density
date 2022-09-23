using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaliperInputController : MonoBehaviour
{
    public GameObject caliper = null;
    public GameObject noniusBody = null;

    public float maxValue = -120f;

    private Vector3 caliperOriginalPosition;
    private bool caliperIsUp;

    void Start()
    {
        caliperOriginalPosition = caliper.transform.localPosition;
    }

    private void MoveCaliperUp()
    {
        if (!caliperIsUp)
        {
            caliperIsUp = true;
            caliper.transform.localPosition = new Vector3(caliperOriginalPosition.x, caliperOriginalPosition.y + 0.5f, caliperOriginalPosition.z);
        }
    }
    private void MoveCaliperDown()
    {
        if (caliperIsUp)
        {
            caliperIsUp = false;
            caliper.transform.localPosition = caliperOriginalPosition;
        }
    }

    private float CalculateDistanceMultiplier(float bounds)
    {
        double sum = System.Math.Round(bounds - noniusBody.transform.localPosition.x, 3);
        double mult = System.Math.Truncate(System.Math.Abs(sum) / 0.001d);
        float result = (float)mult;
        return result;
    }

    private void MoveNonius(int dirModifier, float distMultiplier = 80)
    {
        float height = -GSC.currentHeight;

        float distMoved = dirModifier * 0.001f * distMultiplier;
        if (dirModifier > 0 && noniusBody.transform.localPosition.x + distMoved > height)
        {
            distMultiplier = CalculateDistanceMultiplier(height);
        }
        else if (dirModifier < 0 && noniusBody.transform.localPosition.x + distMoved < maxValue)
        {
            distMultiplier = CalculateDistanceMultiplier(maxValue);
        }
        distMoved = dirModifier * 0.001f * distMultiplier;
        float newX = (float)System.Math.Round(noniusBody.transform.localPosition.x + distMoved, 3);
        Vector3 newPos = new Vector3(newX, 0, 0);
        noniusBody.transform.localPosition = newPos;
    }

    private void AdjustCaliper()
    {
        if (noniusBody.transform.localPosition.x > -GSC.currentHeight)
        {
            noniusBody.transform.localPosition = new Vector3(-GSC.caliperHolder.heldObjectScript.currentHeight - 5f, 0, 0);
        }
    }

    private void Update()
    {
        if (GSC.caliperHolder.heldObject != null)
        {
            MoveCaliperUp();

            if (GSC.focusTarget == GSC.caliper || GSC.focusTarget == GSC.nonius)
            {
                float noniusX = noniusBody.transform.localPosition.x;
                int currentState = GSC.caliperHolder.state;

                if (Input.GetKeyDown("up") 
                    && currentState != 0
                    && noniusX < -GSC.caliperHolder.heldObjectScript.GetStateHeight(currentState - 1)
                    && noniusX != -GSC.currentHeight)
                {
                    GSC.caliperHolder.MoveHeldObject(-1, GSC.avgDiameter);
                    AdjustCaliper();
                }
                if (Input.GetKeyDown("down")
                    && currentState != GSC.sampleStateCount - 1
                    && noniusX < -GSC.caliperHolder.heldObjectScript.GetStateHeight(currentState + 1)
                    && noniusX != -GSC.currentHeight)
                {
                    GSC.caliperHolder.MoveHeldObject(1, GSC.avgDiameter);
                    AdjustCaliper();
                }
            }
        }
        else
            MoveCaliperDown();
    }

    void FixedUpdate()
    {
        if (GSC.caliperHolder.heldObject != null)
        {
            if (GSC.focusTarget == GSC.caliper || GSC.focusTarget == GSC.nonius)
            {
                if (Input.GetKey("left"))
                {
                    if (noniusBody.transform.localPosition.x != -GSC.currentHeight && noniusBody.transform.localPosition.x != 0)
                    {
                        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                            MoveNonius(1, 240);
                        else
                            MoveNonius(1);
                    } 
                }
                if (Input.GetKey("right"))
                {
                    if (noniusBody.transform.localPosition.x != maxValue)
                    {
                        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                            MoveNonius(-1, 240);
                        else
                            MoveNonius(-1);
                    }
                }
            }
        }
        else if (noniusBody.transform.localPosition.x != maxValue)
        {
            noniusBody.transform.localPosition = new Vector3(maxValue, 0, 0);
        }
    }
}
