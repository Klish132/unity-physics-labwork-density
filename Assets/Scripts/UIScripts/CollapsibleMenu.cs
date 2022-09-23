using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollapsibleMenu : MonoBehaviour
{
    public GameObject collapseButton;
    public GameObject[] toggles;

    private bool menuStatus = false;
    public void ToggleMenu()
    {
        menuStatus = !menuStatus;
        string newText;
        newText = menuStatus is true ? "Закрыть инструкции" : "Открыть инструкции";
        collapseButton.GetComponentInChildren<Text>().text = newText;
        foreach (GameObject toggle in toggles)
        {
            toggle.SetActive(menuStatus);
        }
    }
}
