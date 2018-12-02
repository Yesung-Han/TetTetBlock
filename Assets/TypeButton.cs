using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TypeButton : MonoBehaviour {
    public int typeNum;
    public TypeButton MainTypeButton;
    public Image mainSprite;
    public Image thisSprite;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void tossTypeNum()
    {
        MainTypeButton.typeNum = this.typeNum;
        mainSprite.sprite = thisSprite.sprite;
    }

}
