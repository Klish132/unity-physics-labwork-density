using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrometerInputController : MonoBehaviour
{
    public GameObject micrometer = null;
    public GameObject reel = null;

    public float maxValue = -20f;

    private Vector3 micrometerOriginalPosition;
    private bool micrometerIsUp;

    void Start()
    {
        micrometerOriginalPosition = micrometer.transform.localPosition;
    }

    private void MoveMicrometerUp()
    {
        if (!micrometerIsUp)
        {
            micrometerIsUp = true;
            micrometer.transform.localPosition = new Vector3(micrometerOriginalPosition.x, micrometerOriginalPosition.y + 0.5f, micrometerOriginalPosition.z);
        }
    }
    private void MoveMicrometeDown()
    {
        if (micrometerIsUp)
        {
            micrometerIsUp = false;
            micrometer.transform.localPosition = micrometerOriginalPosition;
        }
    }

    private float CalculateDistanceMultiplier(float bounds)
    {
        double sum = System.Math.Round(bounds - reel.transform.localPosition.x, 3);
        double mult = System.Math.Truncate(System.Math.Abs(sum) / 0.001d);
        float result = (float)mult;
        return result;
    }
    private void MoveReel(int dirModifier, float distMultiplier = 20)
    {
        float diameter = -GSC.currentDiameter;

        float distMoved = dirModifier * 0.001f * distMultiplier;
        if (reel.transform.localPosition.x + distMoved > diameter)
        {
            distMultiplier = CalculateDistanceMultiplier(diameter);
        }
        else if (reel.transform.localPosition.x + distMoved < maxValue)
        {
            distMultiplier = CalculateDistanceMultiplier(maxValue);
        }
        distMoved = dirModifier * 0.001f * distMultiplier;
        float newX = (float)System.Math.Round(reel.transform.localPosition.x + distMoved, 3);
        Vector3 newPos = new Vector3(newX, 0, 0);
        reel.transform.localRotation *= Quaternion.Euler(0.72f * dirModifier * distMultiplier, 0, 0);
        reel.transform.localPosition = newPos;
    }

    private void AdjustMicrometer()
    {
        if (reel.transform.localPosition.x > -GSC.currentDiameter)
        {
            reel.transform.localPosition = new Vector3(maxValue, 0, 0);
            reel.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }
    }

    private void Update()
    {
        if (GSC.micrometerHolder.heldObject != null)
        {
            MoveMicrometerUp();

            if (GSC.focusTarget == GSC.micrometer || GSC.focusTarget == GSC.reel)
            {
                float reelX = reel.transform.localPosition.x;
                int currentState = GSC.micrometerHolder.state;

                if (Input.GetKeyDown("up")
                    && currentState != 0
                    && reelX < -GSC.micrometerHolder.heldObjectScript.GetStateDiameter(currentState - 1)
                    && reelX != -GSC.currentDiameter)
                {
                    GSC.micrometerHolder.MoveHeldObject(-1, GSC.avgHeight);
                    AdjustMicrometer();
                }
                if (Input.GetKeyDown("down")
                    && currentState != GSC.sampleStateCount - 1
                    && reelX < -GSC.micrometerHolder.heldObjectScript.GetStateDiameter(currentState + 1)
                    && reelX != -GSC.currentDiameter)
                {
                    GSC.micrometerHolder.MoveHeldObject(1, GSC.avgHeight);
                    AdjustMicrometer();
                }
            }
        }
        else
            MoveMicrometeDown();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GSC.micrometerHolder.heldObject != null)
        {
            if (GSC.focusTarget == GSC.micrometer || GSC.focusTarget == GSC.reel)
            {
                if (Input.GetKey("left"))
                {
                    if (reel.transform.localPosition.x != -GSC.currentDiameter)
                    {
                        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                            MoveReel(1, 60);
                        else
                            MoveReel(1);
                    }
                }
                if (Input.GetKey("right"))
                {
                    if (reel.transform.localPosition.x != maxValue)
                    {
                        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                            MoveReel(-1, 60);
                        else
                            MoveReel(-1);
                    }
                }
            }
        }
        else if (reel.transform.localPosition.x != maxValue)
        {
            reel.transform.localPosition = new Vector3(maxValue, 0, 0);
        }
    }
}
