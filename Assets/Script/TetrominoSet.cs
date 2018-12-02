using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


public class TetrominoSet : MonoBehaviour {
     
    public TetrominoDB TetDB; //아이템엔트리형 배열 
    public List<TetrominoInfo> SaveTetDB = new List<TetrominoInfo>();
    public int TetDBPointer;
   
    Game GameManager;
    public Button BackButton;
    public Button FrontButton;


    void Start()
    {
        
        GameManager = FindObjectOfType<Game>();
        //HistorySystemInit();

    }


    public void SaveItems()//XML
    {
        XmlSerializer serializer = new XmlSerializer(typeof(TetrominoDB));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/item_data.xml", FileMode.Create);
        serializer.Serialize(stream, TetDB);
        stream.Close();
        Debug.Log("Save Successed! : " + Application.dataPath + "/StreamingAssets/XML/item_data.xml");
    }

    public void LoadItems()//XML
    {
        var serializer = new XmlSerializer(typeof(TetrominoDB));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/item_data.xml", FileMode.Open);
        var streamReader = new StreamReader(stream, System.Text.Encoding.UTF8);
        TetDB = serializer.Deserialize(streamReader) as TetrominoDB;
        stream.Close();

        Debug.Log("Load Successed! : " + Application.dataPath + "/StreamingAssets/XML/item_data.xml");

    }

    public void TetPonterInit()
    {
        TetDBPointer = TetDB.TetList.Count;

    }

   
    public void backButtonClick()
    {
        
        TetDBPointer = TetDB.TetList.Count;//0.포인터 초기화 : 포인터의 위치 = 배열의 크기

        if (TetDBPointer >= 1)
        {
            GameManager.moveAudio.Play();
            if (SaveTetDB.Count==0)
            {
                SaveTetDB.AddRange(TetDB.TetList);//1.현상태 저장
                
            }

            FrontButton.interactable = true;
            TetDBPointer--;   //2.포인터 한칸뭄고 , 포인터가 가리키는 데이터 삭제
            TetDB.TetList.RemoveAt(TetDBPointer);
            Destroy(this.transform.GetChild(TetDBPointer).gameObject);
            
            //GameManager.FrontBackLoadTet();//3.디스플레이
            if(TetDBPointer==0)
            {
                BackButton.interactable=false;
            }

        }
        else
        {
            //Debug.Log("Cant Back;;");
        }
    }


    public void FrontButtonClick()
    {
        int SDCount = SaveTetDB.Count; 
        if (SDCount != 0)
        {
            if (SDCount > TetDBPointer)
            {
                GameManager.moveAudio.Play();
                BackButton.interactable = true;
                TetDB.TetList.Add(SaveTetDB[TetDBPointer]);
                GameManager.Spown_Front_TET(TetDBPointer);

                TetDBPointer++;

                //GameManager.FrontBackLoadTet();
                if(SDCount== TetDBPointer)
                {
                   // Debug.Log("This is end;;");
                    FrontButton.interactable = false;
                }
            }
           
        }
        else
        {
           // Debug.Log("There is no Save Data;;");
        }
    }

    public void HistoryDataRemove()
    {
        BackButton.interactable = true;
        SaveTetDB.Clear();
        FrontButton.interactable = false;
    }

   public void HistorySystemInit()
    {
        SaveTetDB.Clear();
        TetDBPointer = TetDB.TetList.Count;
        FrontButton.interactable = false;
        if (TetDBPointer == 0)
        {
            BackButton.interactable = false;
        }
        else
        {
            BackButton.interactable = true;
        }

    }


}

[System.Serializable]
public struct TetrominoInfo
{
    public int TetrominoNameNum;
    public Vector3 tetromino_position;
    public Vector3 tetromino_rotation;
    public Vector2 TetColor;
}
[System.Serializable]
public class TetrominoDB
{
    public string mapName;
    public Vector2 mapSize;

    [XmlArray("Tetrominos")]
    public List<TetrominoInfo> TetList = new List<TetrominoInfo>();

}