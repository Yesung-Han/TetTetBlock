using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadPanelBoxDisplay : MonoBehaviour
{
    public GameObject blockPrefabs;
    //playerDataBase CurrentUserDB;
    public UserDataManager CurrentUserDBMangaer;
    public Game CurrentGameMangaer;
    // Use this for initialization



    void Start()
    {
        
        Display_Item();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Display_Item()
    {
        CurrentGameMangaer.IndicateDeactive();
        //CurrentUserDB = GameObject.FindGameObjectWithTag("CurrentPlayerDB").GetComponent<playerDataBase>();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }


        for (int i = 0; i < playerDataBase.StaticPlayerDB().aa.MapList.Count; i++)
        {
            GameObject newBlock = Instantiate(blockPrefabs) as GameObject;
            newBlock.transform.SetParent(transform, false);

            if (i == CurrentGameMangaer.nowMapNum)
            {
                newBlock.GetComponent<Image>().color = Color.gray;
            }

            /*
            newBlock.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { newBlock.GetComponent<ItemBlock>().TossNum(); });
            entry.callback.AddListener((data) => { CurrentUserDBMangaer.CopyLoadtoCurrentTetrominoSet(); });
            entry.callback.AddListener((data) => { CurrentGameMangaer.LoadItemButtonClick(); });
            newBlock.GetComponent<EventTrigger>().triggers.Add(entry);
            */

            //newBlock.GetComponent<Button>().onClick.AddListener(() => newBlock.GetComponent<ItemBlock>().TossNum());
            // newBlock.GetComponent<Button>().onClick.AddListener(() => CurrentUserDBMangaer.CopyLoadtoCurrentTetrominoSet());
            // newBlock.GetComponent<Button>().onClick.AddListener(() => CurrentGameMangaer.LoadItemButtonClick());

            newBlock.GetComponent<ItemBlock>().Display_Text(playerDataBase.StaticPlayerDB().aa.MapList[i]);
            newBlock.GetComponent<ItemBlock>().ItemNumber = i;
        }


    }
}