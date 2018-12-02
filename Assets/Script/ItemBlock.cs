using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemBlock : MonoBehaviour, IPointerDownHandler
{
    
    public Text name, size, number;
    public int ItemNumber;
    // Use this for initialization
    public GameObject indicater;
    public GameObject CurrentUserDataManager;
    public void OnPointerDown(PointerEventData eventData)
    {
        // Do action
        //this.GetComponent<Image>().color = Color.green;
        TossNum();
    }


    void Start () {
        indicater = GameObject.FindGameObjectWithTag("indicater");
        if (Application.loadedLevel == 1)
        {
           CurrentUserDataManager = GameObject.FindGameObjectWithTag("CurrentUserDBManager");
        }

        else if (Application.loadedLevel == 0)
        {
           CurrentUserDataManager = GameObject.FindGameObjectWithTag("FirstSceneManager");
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Display_Text(TetrominoDB tetDB)
    {
        name.text = tetDB.mapName;
        size.text=tetDB.mapSize.x+" X "+tetDB.mapSize.y;
        number.text = tetDB.TetList.Count+" pieces";
        //ItemNumber=tetDB.
    }
    public void TossNum()
    {
        
        if (Application.loadedLevel == 1)
        {   
            CurrentUserDataManager.GetComponent<UserDataManager>().WantToSaveNumb = ItemNumber;
            CurrentUserDataManager.GetComponent<UserDataManager>().indicaterOn = true;
            CurrentUserDataManager.GetComponent<UserDataManager>().PlusButtonCheck = false;
            indicater.transform.SetParent(transform);
            indicater.transform.position = transform.position;

        }
        else if (Application.loadedLevel == 0)
        {
            CurrentUserDataManager.GetComponent<firstSceneManager>().WanttoGoNum = ItemNumber;
            CurrentUserDataManager.GetComponent<firstSceneManager>().indicaterOn = true;
            indicater.transform.SetParent(transform);
            indicater.transform.position = transform.position;

        }

        
    }
    /*
    public void FirstSceneTossNum()
    {
        GameObject FirstSceneManager = GameObject.FindGameObjectWithTag("FirstSceneManager");
        FirstSceneManager.GetComponent<firstSceneManager>().WanttoGoNum = ItemNumber;
    }
    */
  
        
    

}
