using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlusButtonScript : MonoBehaviour, IPointerDownHandler
{
    public GameObject indicater;
    public GameObject CurrentUserDataManager;
    public void OnPointerDown(PointerEventData eventData)
    {
       
        CurrentUserDataManager.GetComponent<UserDataManager>().PlusButtonCheck = true;
        CurrentUserDataManager.GetComponent<UserDataManager>().indicaterOn = true;
        indicater.transform.SetParent(transform);
        indicater.transform.position = transform.position;
    }
    // Use this for initialization
    void Start () {
        indicater = GameObject.FindGameObjectWithTag("indicater");
        CurrentUserDataManager = GameObject.FindGameObjectWithTag("CurrentUserDBManager");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
