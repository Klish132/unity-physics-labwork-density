using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleInputController : MonoBehaviour
{
    public Text mainText = null;
    public Text statusText = null;

    // 0 = Не отклабированы, 1 = Калибруются, 2 = Откалиброваны
    private int calibrationStatus;
    private bool waiting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Calibration()
    {
        if (!waiting)
        {
            if (calibrationStatus == 0 || calibrationStatus == 2)
            {
                if (GSC.scaleHolder.heldObject != null)
                {
                    mainText.text = "ERROR";
                    Wait();
                }
                else
                {
                    calibrationStatus = 1;
                    Wait();
                }
            }
            if (calibrationStatus == 1)
            {
                if (GSC.scaleHolder.heldObject != null)
                {
                    if (GSC.scaleHolder.heldObject == GSC.weight)
                    {
                        Wait();
                        calibrationStatus = 2;
                    }
                    else
                    {
                        mainText.text = "ERROR";
                    }
                }
            }
        }
    }

    private void Wait()
    {
        if (waiting == false)
        {
            GSC.preventDrag = true;
            waiting = true;
            Invoke("Wait", 2);
        }
        else
        {
            GSC.preventDrag = false;
            waiting = false;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting)
        {
            statusText.text = "~";
        }
        else
        {
            statusText.text = "";
            if (calibrationStatus == 2)
            {
                if (GSC.scaleHolder.heldObject != null)
                {
                    if (GSC.scaleHolder.heldObject == GSC.weight)
                        mainText.text = "500,000";
                    else
                        mainText.text = GSC.scaleHolder.heldObject.GetComponent<Sample>().mass.ToString();
                }
                else
                    mainText.text = ",000";
            }
            if (calibrationStatus == 1)
            {
                mainText.text = ",000";
                Calibration();
            }
            if (calibrationStatus == 0)
            {
                statusText.text = "";
                mainText.text = "";
            }
        }
    }
}
