using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DimensionCheckController : MonoBehaviour
{
    public InputField heightCheckInput = null;

    public InputField diameterCheckInput = null;

    private string prevHeight = "";
    private int prevHeightState = 0;

    private string prevDiameter = "";
    private int prevDiameterState = 0;

    // Update is called once per frame
    void Update()
    {
        if (GSC.caliperHolder.heldObject != null & (GSC.focusTarget == GSC.caliper || GSC.focusTarget == GSC.nonius))
            heightCheckInput.gameObject.SetActive(true);
        else
            heightCheckInput.gameObject.SetActive(false);

        if (GSC.micrometerHolder.heldObject != null & (GSC.focusTarget == GSC.micrometer || GSC.focusTarget == GSC.reel))
            diameterCheckInput.gameObject.SetActive(true);
        else
            diameterCheckInput.gameObject.SetActive(false);

        if (heightCheckInput.text != prevHeight && heightCheckInput.text != "")
        {
            float inputHeight = float.Parse(heightCheckInput.text.Replace(".", ","));

            float correctHeight = (float)System.Math.Round(GSC.caliperHolder.heldObjectScript.currentHeight * 20) / 20;

            if (Mathf.Approximately(inputHeight, correctHeight - 0.05f) || Mathf.Approximately(inputHeight, correctHeight) || Mathf.Approximately(inputHeight, correctHeight + 0.05f))
                heightCheckInput.textComponent.color = new Color32(55, 200, 55, 255);
            else
                heightCheckInput.textComponent.color = new Color32(200, 55, 55, 255);
            prevHeight = heightCheckInput.text;
        }
        else
        {
            int state = GSC.caliperHolder.state;
            if (prevHeightState != state)
            {
                heightCheckInput.text = "";
                prevHeight = "";
                prevHeightState = state;
            }
        }

        if (diameterCheckInput.text != prevDiameter && diameterCheckInput.text != "")
        {
            double inputHeight = double.Parse(diameterCheckInput.text.Replace(".", ","));

            float correctDiameter = GSC.micrometerHolder.heldObjectScript.currentDiameter;

            if (inputHeight >= correctDiameter - 0.015d && inputHeight <= correctDiameter + 0.015d)
                diameterCheckInput.textComponent.color = new Color32(55, 200, 55, 255);
            else
                diameterCheckInput.textComponent.color = new Color32(200, 55, 55, 255);
            prevDiameter = diameterCheckInput.text;
        }
        else
        {
            int state = GSC.micrometerHolder.state;
            if (prevDiameterState != state)
            {
                diameterCheckInput.text = "";
                prevDiameter = "";
                prevDiameterState = state;
            }
        }
    }
}
