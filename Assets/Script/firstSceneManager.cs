using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class firstSceneManager : MonoBehaviour {

    public GameObject SelectPanel;
    public GameObject FirstLoadPanel;
    public GameObject ConfirmPannel;
    public GameObject Menu_window;
    public GameObject Quit_Game_Window;
    public GameObject popup_Window1;
    public GameObject popup_Window3;
    public GameObject popup_Window4;
    public GameObject indicater;
    public GameObject mapRemovePopup;

    public Text RemoveMapname;

    public bool indicaterOn = false;

    public AudioSource first_BGM;

    public InputField NewInputMapName;
    public InputField NewInputMapSize_X;
    public InputField NewInputMapSize_Z;
    public Text NowConfirmText;
    public string NewMapName;
    public int NewMapSize_X;
    public int NewMapSize_Z;
    public int WanttoGoNum;
    // Use this for initialization

    





    void Awake()
    {
           CheckXMLexist();
        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 30;
    }

    void Start () {
        SelectPanel.SetActive(false);
        ConfirmPannel.SetActive(false);

        if (playerDataBase.StaticPlayerDB().BGM_Switch)
        {
            first_BGM.Play();
        }

    }
	
	// Update is called once per frame
	void Update () {
       if(SelectPanel.activeInHierarchy)
        {
            MapNameSelect();
            MapSizeSelect();      
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
                Quit_Game_Window.SetActive(true);
            
        }
    }

    public void CheckXMLexist()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/MyFile.xml"))
        {
            playerDataBase.StaticPlayerDB().IsBeginer = false;
        }
        else
        {
            playerDataBase.StaticPlayerDB().IsBeginer = true;
        }
    }

    public void ActiveResetConfirm()
    {
        ConfirmPannel.SetActive(true);
    }
    public void DeactiveResetConfirm()
    {
        ConfirmPannel.SetActive(false);
    }

    public void PopUpSelectPanel()
    {

        NewInputMapName.text = "new map";
        NewInputMapSize_X.text = "20";
        NewInputMapSize_Z.text = "20";
        SelectPanel.SetActive(true);
        
    }

    public void DeactivateSelectPanel()
    {
        NowConfirmText.gameObject.SetActive(false);
        SelectPanel.SetActive(false);
    }

    public void PopUpFirstLoadPanel()
    {
        FirstLoadPanel.SetActive(true);

    }

    public void DeactivateFirstLoadPanel()
    {
        indicater.transform.parent = null;
        indicaterOn = false;
        FirstLoadPanel.SetActive(false);
    }



    public void MapNameSelect()
    {
        NewMapName = string.IsNullOrEmpty(NewInputMapName.text) ? "New Map" : NewInputMapName.text;    
    }
    public void MapSizeSelect()
    {
        NewMapSize_X = string.IsNullOrEmpty(NewInputMapSize_X.text) ? 0 : Convert.ToInt32(NewInputMapSize_X.text);
        NewMapSize_Z = string.IsNullOrEmpty(NewInputMapSize_Z.text) ? 0 : Convert.ToInt32(NewInputMapSize_Z.text);    
    }
    public void TossDatatoNextScene()
    {
         
        PlayerPrefs.SetInt("Initial_Number", playerDataBase.StaticPlayerDB().aa.MapList.Count-1);
        
    }
    public void CreateNewMaptoDB()
    {
        TetrominoDB tempTetrominoDB = new TetrominoDB();
        tempTetrominoDB.mapName = NewMapName;
        tempTetrominoDB.mapSize = new Vector2(NewMapSize_X, NewMapSize_Z);
        playerDataBase.StaticPlayerDB().aa.MapList.Add(tempTetrominoDB);
    }

    public void GotoMainSceneByNew()
    {
        if (NewMapSize_X >= 10 && NewMapSize_X <= 50 && NewMapSize_Z >= 10 && NewMapSize_Z <= 50)
        {

            CreateNewMaptoDB();
            TossDatatoNextScene();
            Application.LoadLevel("Main");

        }
        else
        {
            NowConfirmText.gameObject.SetActive(true);
        }   
    }

    public void GotoMainSceneByContinue()
    {
        if (indicaterOn)
        { 
         PlayerPrefs.SetInt("Initial_Number", WanttoGoNum);
         Application.LoadLevel("Main");
        }
        else
        {
            Open_ChooseError_Popup3();
        }
    }

    public void Open_Menu()
    {
        Menu_window.SetActive(true);
    }
    public void Close_Menu()
    {
        Menu_window.SetActive(false);
    }
    public void Open_Quit_Game_Menu()
    {
        Quit_Game_Window.SetActive(true);
    }
    public void Close_Quit_Game_Menu()
    {
        Quit_Game_Window.SetActive(false);
    }

    public void Application_Quit()
    {
        Application.Quit();
    }

    public void turn_on_BGM()
    {
        first_BGM.Play();
    }
    public void turn_off_BGM()
    {
        first_BGM.Stop();
    }


    public void Open_Reset_Popup()
    {
        StartCoroutine(ResetPopup());
    }

    public void Open_Remove_Popup()
    {
        if (indicaterOn)
        {
            RemoveMapname.text = playerDataBase.StaticPlayerDB().aa.MapList[WanttoGoNum].mapName;
            mapRemovePopup.SetActive(true);
        }
        else
        {
            Open_ChooseError_Popup3();
        }
    }

    public void Close_Remove_Popup()
    {
        mapRemovePopup.SetActive(false);
    }

    public void RemoveMapData()
    {
        indicater.transform.parent = null;
        indicaterOn = false;
        playerDataBase.StaticPlayerDB().aa.MapList.RemoveAt(WanttoGoNum);

    }

    IEnumerator ResetPopup()
    {
        popup_Window1.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        popup_Window1.SetActive(false);
        CheckXMLexist();
    }

    public void Open_ChooseError_Popup3()
    {
        StartCoroutine(ResetPopup3());
    }

    IEnumerator ResetPopup3()
    {
        popup_Window3.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        popup_Window3.SetActive(false);
    }


    public void Open_Recomend_Popup3()
    {
        StartCoroutine(ResetPopup4());
    }

    IEnumerator ResetPopup4()
    {
        popup_Window4.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        popup_Window4.SetActive(false);
        Application.OpenURL("https://www.facebook.com/TetTetBlock/?ref=bookmarks");
    }


}
