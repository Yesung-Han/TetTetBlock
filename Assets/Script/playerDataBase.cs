using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using System;

public class playerDataBase : MonoBehaviour {
    // public static playerDataBase PlayerDataBase1;


    private static playerDataBase _StaticPlayerDB;

    // private static object m_pLock = new object();
    public int SingletonInstance = 0;
    //public UserDataBase UserData1;

    public static playerDataBase StaticPlayerDB()
    {


        // if (!_StaticPlayerDB)
        // {
        //_StaticPlayerDB = (playerDataBase)FindObjectOfType(typeof(playerDataBase));
        //if (FindObjectsOfType(typeof(playerDataBase)).Length > 1)
        // {
        //    return _StaticPlayerDB;
        // }

        if (!_StaticPlayerDB)
        {
            GameObject singleton = new GameObject();
            _StaticPlayerDB = singleton.AddComponent<playerDataBase>();
            singleton.name = typeof(playerDataBase).ToString();

            DontDestroyOnLoad(singleton);
        }
        return _StaticPlayerDB;

    }

    //public string PlayerName = "My name";
    //[XmlArray("Maps")]
    //public List<TetrominoDB> MapList = new List<TetrominoDB>();

    public AA aa = new AA();
    public bool BGM_Switch = true;
    public bool IsBeginer = true;
    public int LangCode = 23;

    void start()
    {
      
    }
    void Awake()
    {
        
    }

}

[System.Serializable]
public class AA 
{
    public string PlayerName = "Your name here";
    public List<TetrominoDB> MapList=new List<TetrominoDB>();
}