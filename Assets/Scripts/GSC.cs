using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSC : MonoBehaviour
{
    public static GSC instance = null;

    public static GameObject scalesBody = null;
    public static GameObject scale = null;
    public static GameObject caliper = null;
    public static GameObject nonius = null;
    public static GameObject micrometer = null;
    public static GameObject reel = null;
    public static GameObject sample = null;
    public static GameObject weight = null;

    public static GameObject focusTarget = null;

    public static ObjectHolder scaleHolder = null;
    public static ChangableObjectHolder caliperHolder = null;
    public static ChangableObjectHolder micrometerHolder = null;

    public static int sampleStateCount = 0;

    public static double avgDiameter = 0;
    public static double avgHeight = 0;
    public static float currentDiameter = 0;
    public static float currentHeight = 0;

    public static bool isSelectingSamples = true;
    public static bool isOnMainCamera = false;
    public static bool isDragging = false;
    public static bool preventDrag = false;

    void Start()
    {
        if (instance == null)
        { // Ёкземпл€р менеджера был найден
            instance = this; // «адаем ссылку на экземпл€р объекта
        }
        else if (instance == this)
        { // Ёкземпл€р объекта уже существует на сцене
            Destroy(gameObject); // ”дал€ем объект
        }
        Initialize();
    }

    private void Initialize()
    {
        focusTarget = scalesBody = GameObject.Find("ScalesBody");
        scale = GameObject.Find("Scale");
        caliper = GameObject.Find("CaliperBody");
        nonius = GameObject.Find("Nonius");
        micrometer = GameObject.Find("Ruler");
        reel = GameObject.Find("Reel");
        weight = GameObject.Find("Weight");

        scaleHolder = GameObject.Find("Scale").GetComponent<ObjectHolder>();
        caliperHolder = GameObject.Find("CaliperBody").GetComponent<ChangableObjectHolder>();
        micrometerHolder = GameObject.Find("Ruler").GetComponent<ChangableObjectHolder>();
    }
}
