using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    public int density;

    public double avgDiameter;
    public double avgHeight;

    public float currentDiameter;
    public float currentHeight;

    public double mass;

    public float[] heights;
    public float[] diameters;

    public void SetDensity(int den)
    {
        density = den;
    }

    public void CreateArrays(int length)
    {
        heights = new float[length];
        diameters = new float[length];
    }

    public void AddValuesToArrays(int i, double newD, double newH)
    {
        diameters[i] = (float)newD;
        heights[i] = (float)newH;
    }

    public void SetMass()
    {
        mass = System.Math.Round((System.Math.PI * avgDiameter * avgDiameter / 4) * avgHeight * density / 1000000d, 3);
    }

    public void SetAverages()
    {
        double dSum = 0;
        foreach (double d in diameters)
            dSum += d;
        avgDiameter = GSC.avgDiameter = System.Math.Round(dSum / GSC.sampleStateCount, 3);
        double hSum = 0;
        foreach (double h in heights)
            hSum += h;
        avgHeight = GSC.avgHeight = System.Math.Round(hSum / GSC.sampleStateCount, 3);
    }

    public void InitializeSample()
    {
        currentDiameter = GSC.currentDiameter = diameters[0];
        currentHeight = GSC.currentHeight = heights[0];
        transform.localScale = new Vector3((float)avgDiameter / 2f, (float)avgDiameter / 2f, (float)avgHeight / 2f);

        Debug.Log("Average diameter: " + avgDiameter);
        Debug.Log("Average height: " + avgHeight);
        Debug.Log("Density: " + density);
        Debug.Log("Mass: " + mass);
        string debugHeights = "Heights: ";
        foreach (float i in heights)
            debugHeights += i + ", ";
        string debugDiameters = "Diameters: ";
        foreach (float i in diameters)
            debugDiameters += i + ", ";
        Debug.Log(debugHeights);
        Debug.Log(debugDiameters);
    }

    public void ChangeState(int state)
    {
        currentDiameter = GSC.currentDiameter = diameters[state];
        currentHeight = GSC.currentHeight = heights[state];
    }

    public float GetStateHeight(int state)
    {
        return heights[state];
    }

    public float GetStateDiameter(int state)
    {
        return diameters[state];
    }
}
