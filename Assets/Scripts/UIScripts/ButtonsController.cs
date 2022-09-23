using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    public GameObject scalesMainBody = null;

    public GameObject zoomoutButton = null;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void FocusOnScalesBody()
    {
        scalesMainBody.GetComponent<ObjectOfFocus>().Focus();
    }

    // Update is called once per frame
    void Update()
    {
        if (GSC.focusTarget != GSC.scalesBody)
        {
            zoomoutButton.SetActive(true);
        } else
        {
            zoomoutButton.SetActive(false);
        }
    }
}
