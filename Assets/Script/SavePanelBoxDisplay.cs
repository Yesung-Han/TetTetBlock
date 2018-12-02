using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SavePanelBoxDisplay : MonoBehaviour {

    public GameObject blockPrefabs;
    public UserDataManager NowDataManager;
    public Game GameManager;
    public TetrominoSet nowTetSet;
    public Button plusBlockPrefabs;
    public GameObject popw;
    public GameObject Apopw;
    public bool SavePopup=false;
    public InputField saveText;
    public InputField saveAppedText;
    //playerDataBase CurrentUserDB;
    // Use this for initialization




    void Start () {
       // Display_Item();
        popw.SetActive(false);
        Apopw.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        
        


    }

 

    public void Display_Item()
    {
        GameManager.IndicateDeactive();
        //CurrentUserDB = GameObject.FindGameObjectWithTag("CurrentPlayerDB").GetComponent<playerDataBase>();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

       
        for (int i = 0; i< playerDataBase.StaticPlayerDB().aa.MapList.Count; i++)
        {
            GameObject newBlock = Instantiate(blockPrefabs) as GameObject;
            newBlock.transform.SetParent(transform, false);
            if(i==GameManager.nowMapNum)
            {
                newBlock.GetComponent<Image>().color=Color.gray;
            }
            /*
            newBlock.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { ActivateAppendSavePopupWindow(); });
            entry.callback.AddListener((data) => { newBlock.GetComponent<ItemBlock>().TossNum(); });
           
            newBlock.GetComponent<EventTrigger>().triggers.Add(entry);
            */
            // newBlock.GetComponent<Button>().onClick.AddListener(()=>ActivateAppendSavePopupWindow());
            // newBlock.GetComponent<Button>().onClick.AddListener(()=>newBlock.GetComponent<ItemBlock>().TossNum());

            newBlock.GetComponent<ItemBlock>().Display_Text(playerDataBase.StaticPlayerDB().aa.MapList[i]);
            newBlock.GetComponent<ItemBlock>().ItemNumber = i;



        }


        /* 포이치 구문 
        foreach (TetrominoDB item in FindObjectOfType<UserDataManager>().userData1.MapList)
        {
            ItemBlock newBlock = Instantiate(blockPrefabs) as ItemBlock;
            newBlock.transform.SetParent(transform, false);
            newBlock.Display_Text(item);
            
        }*/
        Button plusBlock = Instantiate(plusBlockPrefabs) as Button;
        plusBlock.transform.SetParent(transform, false);
        //plusBlock.onClick.AddListener(ActivateSavePopupWindow);

    }

    public void SaveButtonClick()
    {
      if(NowDataManager.indicaterOn)
        {
            if(NowDataManager.PlusButtonCheck)
            {
                ActivateSavePopupWindow();
            }
            else
            {
                ActivateAppendSavePopupWindow();
            }
        }
      else
        {
            GameManager.Open_CantChoose_Popup();
        }
    }

    public void ActivateSavePopupWindow()
    {
        saveText.text = nowTetSet.TetDB.mapName;
        SavePopup = true;
        popw.SetActive(true);
       
    }
    public void DeactivateSavePopupWindow()
    {

       // GameManager.indicater.transform.parent = null;
        SavePopup = false;
        popw.SetActive(false);
    }
    public void ActivateAppendSavePopupWindow()
    {
        saveAppedText.text = nowTetSet.TetDB.mapName;
        Apopw.SetActive(true);
        
    }
    public void DeactivateAppendSavePopupWindow()
    {
        //GameManager.indicater.transform.parent = null;
        Apopw.SetActive(false);
    }
   

}
