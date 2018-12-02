using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XMLManager : MonoBehaviour {




    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void FirstLoad()
    {
        if (Application.systemLanguage.GetHashCode() == 23)
        {
            playerDataBase.StaticPlayerDB().LangCode = 23;
        }
        else
        {
            playerDataBase.StaticPlayerDB().LangCode = 10;
        }
                

        if (System.IO.File.Exists(Application.persistentDataPath + "/MyFile.xml"))
        {
         playerDataBase.StaticPlayerDB().IsBeginer = false;
         var serializer = new XmlSerializer(typeof(AA));
         FileStream stream = new FileStream(Application.persistentDataPath + "/MyFile.xml", FileMode.Open);
         var streamReader = new StreamReader(stream, System.Text.Encoding.UTF8);
         playerDataBase.StaticPlayerDB().aa = serializer.Deserialize(streamReader) as AA;
         stream.Close();
        }
        else
        {
            playerDataBase.StaticPlayerDB().IsBeginer = true;
            
        }

    }
   
    

    public void SavePlayerDataXML()//XML
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AA));
        FileStream stream = new FileStream(Application.persistentDataPath + "/MyFile.xml", FileMode.Create);
        serializer.Serialize(stream, playerDataBase.StaticPlayerDB().aa);
        stream.Close();
        //Debug.Log("Save Successed! : " + Application.dataPath + "/StreamingAssets/XML/Player_Data.xml");
    }

    public void LoadPlayerDataXML()//XML
    {
        var serializer = new XmlSerializer(typeof(AA));
        FileStream stream = new FileStream(Application.persistentDataPath + "/MyFile.xml", FileMode.Open);
        var streamReader = new StreamReader(stream, System.Text.Encoding.UTF8);
        playerDataBase.StaticPlayerDB().aa = serializer.Deserialize(streamReader) as AA;
        stream.Close();

       // Debug.Log("Load Successed! : " + Application.dataPath + "/StreamingAssets/XML/Player_Data.xml");

    }

    public void ResetData()
    {
        playerDataBase.StaticPlayerDB().aa.PlayerName = "New Player";
        playerDataBase.StaticPlayerDB().aa.MapList.Clear();
        SavePlayerDataXML();
        System.IO.File.Delete(Application.persistentDataPath + "/MyFile.xml");
    }


    //Application.persistentDataPath -안드로이드
    //PC : /StreamingAssets/XML/



}
