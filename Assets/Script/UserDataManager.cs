using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class UserDataManager : MonoBehaviour {

  

    public Game GameManager;
    public Text MapNameText;
    public Text AppendMapNameText;
    public TetrominoSet TetSet;
    public int WantToSaveNumb;
    public bool indicaterOn = false;
    public bool PlusButtonCheck = false;

    // Use this for initialization

    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
  

    public void SaveThisMap()
    {
        //PlayerData1 = GameObject.FindGameObjectWithTag("CurrentPlayerDB").GetComponent<playerDataBase>();
        TetrominoDB tempTetrominoDB = new TetrominoDB();
        TetSet.TetDB.mapName = MapNameText.text;
        tempTetrominoDB.mapName = MapNameText.text;
        tempTetrominoDB.mapSize = TetSet.TetDB.mapSize;
        tempTetrominoDB.TetList.AddRange(TetSet.TetDB.TetList);
        playerDataBase.StaticPlayerDB().aa.MapList.Add(tempTetrominoDB);
        GameManager.nowMapNum = playerDataBase.StaticPlayerDB().aa.MapList.Count - 1;
        FindObjectOfType<SavePanelBoxDisplay>().Display_Item();
        
    }
    public void AppendSaveThisMap()
    {
        //PlayerData1 = GameObject.FindGameObjectWithTag("CurrentPlayerDB").GetComponent<playerDataBase>();
        TetrominoDB tempTetrominoDB = new TetrominoDB();
        TetSet.TetDB.mapName = AppendMapNameText.text;
        tempTetrominoDB.mapName = AppendMapNameText.text;
        tempTetrominoDB.mapSize = TetSet.TetDB.mapSize;
        tempTetrominoDB.TetList.AddRange(TetSet.TetDB.TetList);
        playerDataBase.StaticPlayerDB().aa.MapList[WantToSaveNumb] = tempTetrominoDB;
        GameManager.nowMapNum = WantToSaveNumb;
        FindObjectOfType<SavePanelBoxDisplay>().Display_Item();
        
    }

    public void CopyLoadtoCurrentTetrominoSet()//for Button prefabs;
    {
        //PlayerData1 = GameObject.FindGameObjectWithTag("CurrentPlayerDB").GetComponent<playerDataBase>();
        TetSet.TetDB.mapName = playerDataBase.StaticPlayerDB().aa.MapList[WantToSaveNumb].mapName;
        TetSet.TetDB.mapSize = playerDataBase.StaticPlayerDB().aa.MapList[WantToSaveNumb].mapSize;
        TetSet.TetDB.TetList.Clear();
        TetSet.TetDB.TetList.AddRange(playerDataBase.StaticPlayerDB().aa.MapList[WantToSaveNumb].TetList);
        GameManager.nowMapNum = WantToSaveNumb;
    }


}


