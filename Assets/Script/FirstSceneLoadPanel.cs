using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FirstSceneLoadPanel : MonoBehaviour {
    public GameObject blockPrefabs;
   
    private GameObject CurrentFirstSceneManager;
    // Use this for initialization
    void Start () {
      
        CurrentFirstSceneManager = GameObject.FindGameObjectWithTag("FirstSceneManager");
        //indicater = GameObject.FindGameObjectWithTag("indicater");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
   
    

    public void Display_Item()
    {
        

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

       
        for (int i = 0; i < playerDataBase.StaticPlayerDB().aa.MapList.Count; i++)
        {
            GameObject newBlock = Instantiate(blockPrefabs) as GameObject;
            newBlock.transform.SetParent(transform, false);

            /*
            newBlock.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => { newBlock.GetComponent<ItemBlock>().FirstSceneTossNum(); });
            entry.callback.AddListener((data) => { CurrentFirstSceneManager.GetComponent<firstSceneManager>().GotoMainSceneByContinue(); });

            newBlock.GetComponent<EventTrigger>().triggers.Add(entry);
            */

            
            //newBlock.GetComponent<Button>().onClick.AddListener(() => newBlock.GetComponent<ItemBlock>().TossNum());
           
            // newBlock.GetComponent<Button>().onClick.AddListener(() => CurrentFirstSceneManager.GetComponent<firstSceneManager>().GotoMainSceneByContinue());



            newBlock.GetComponent<ItemBlock>().Display_Text(playerDataBase.StaticPlayerDB().aa.MapList[i]);
            newBlock.GetComponent<ItemBlock>().ItemNumber = i;
        }


    }
}
