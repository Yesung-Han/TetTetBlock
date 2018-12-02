using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColorButton : MonoBehaviour {

    public int colorNum;
    public ColorButton MainColorButton;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void tossColorNum()
    {
        MainColorButton.colorNum = this.colorNum;
        MainColorButton.gameObject.GetComponent<Image>().color = this.gameObject.GetComponent<Image>().color;
    }

}
