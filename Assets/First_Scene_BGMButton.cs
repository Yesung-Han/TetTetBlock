using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class First_Scene_BGMButton : MonoBehaviour {

    public firstSceneManager GameM;

    void Start()
    {
        if (playerDataBase.StaticPlayerDB().BGM_Switch)
        {
            gameObject.GetComponent<Image>().color = Color.green;
        }
    }


    public void BGM_ModeSwitch()
    {

        if (!playerDataBase.StaticPlayerDB().BGM_Switch)
        {
            playerDataBase.StaticPlayerDB().BGM_Switch = true;
            GameM.turn_on_BGM();
            gameObject.GetComponent<Image>().color = Color.green;
        }
        else
        {
            playerDataBase.StaticPlayerDB().BGM_Switch = false;
            GameM.turn_off_BGM();
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }
}
