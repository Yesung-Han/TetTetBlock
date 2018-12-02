using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusButton : MonoBehaviour {

    
    public MainCameraScript MainCamera;

	// Use this for initialization
	void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void TrackingModeSwitch()
    {
        if(!MainCamera.TrackingMode)
        {
            MainCamera.TrackingMode = true;
            gameObject.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            MainCamera.TrackingMode = false;
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }

}


