using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSampleController : MonoBehaviour
{
    public Camera mainCamera = null;
    public Camera selectSampleCamera = null;

    public int sampleStateCount = 5;

    public GameObject selectSampleCanvas = null;
    public GameObject mainScene = null;
    public GameObject sampleHolder = null;

    public GameObject graySample = null;
    public GameObject orangeSample = null;
    public GameObject yellowSample = null;

    private Sample graySampleScript = null;
    private Sample orangeSampleScript = null;
    private Sample yellowSampleScript = null;

    private float g_diameter;

    private void Start()
    {
        graySampleScript = graySample.GetComponent<Sample>();
        orangeSampleScript = orangeSample.GetComponent<Sample>();
        yellowSampleScript = yellowSample.GetComponent<Sample>();
    }

    private void GenerateSampleValues(int density, Sample script)
    {
        System.Random rnd = new System.Random();

        double randomDiameter = System.Math.Round(rnd.Next(500, 2001) / 100d, 3); 
        double randomHeight = System.Math.Round(rnd.Next(3000, 10000) / 100d, 3);
        script.SetDensity(density);

        GSC.sampleStateCount = sampleStateCount;
        script.CreateArrays(GSC.sampleStateCount);
        for (int i = 0; i < sampleStateCount; i++)
        {
            double perc = rnd.Next(-5, 6) / 1000d;
            double newDiameter = randomDiameter + System.Math.Round(randomDiameter * perc, 3);
            perc = rnd.Next(-5, 6) / 1000d;
            double newHeight = randomHeight + System.Math.Round(randomHeight * perc, 3);
            script.AddValuesToArrays(i, newDiameter, newHeight);
        }
        script.SetAverages();
        script.SetMass();
    }

    private void RepositionSelectedSample(GameObject sample, Sample sampleScript)
    {
        sample.transform.parent = mainScene.transform;
        sampleScript.InitializeSample();
        DraggableObject sampleDragScript = sample.GetComponent<DraggableObject>();
        sampleDragScript.SetHolders(sampleHolder);
    }

    private void switchToMainCamera()
    {
        selectSampleCamera.enabled = false;
        mainCamera.enabled = true;
        selectSampleCanvas.SetActive(false);
        GSC.isOnMainCamera = true;
        GSC.isSelectingSamples = false;
    }

    public void GraySampleSelected()
    {
        int density = 0;

        System.Random rnd = new System.Random();
        // 0 = Алюминий, 1 = Железо, 2 = Титан, 3 = Сталь
        int material_id = rnd.Next(0, 4);
        switch(material_id)
        {
            case 0:
                density = 2700;
                break;
            case 1:
                density = 7870;
                break;
            case 2:
                density = 4500;
                break;
            case 3:
                density = rnd.Next(7500, 7901);
                break;
        }

        GenerateSampleValues(density, graySampleScript);
        RepositionSelectedSample(graySample, graySampleScript);
        switchToMainCamera();
        GSC.sample = graySample;
    }

    public void OrangeSampleSelected()
    {
        int density = 8930;
        GenerateSampleValues(density, orangeSampleScript);
        RepositionSelectedSample(orangeSample, orangeSampleScript);
        switchToMainCamera();
        GSC.sample = orangeSample;
    }

    public void YellowSampleSelected()
    {
        int density = 0;

        System.Random rnd = new System.Random();
        // 0 = Золото, 1 = Латунь
        int material_id = rnd.Next(0, 2);
        switch (material_id)
        {
            case 0:
                density = 19300;
                break;
            case 1:
                density = rnd.Next(8400, 8701);
                break;
        }

        GenerateSampleValues(density, yellowSampleScript);
        RepositionSelectedSample(yellowSample, yellowSampleScript);
        switchToMainCamera();
        GSC.sample = yellowSample;
    }
}
