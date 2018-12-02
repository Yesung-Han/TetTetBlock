using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverEat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OverEatMinos()
    {
        Transform[] minos = transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < minos.Length; i++)
        {
            minos[i].parent = transform.parent;
        }
        this.gameObject.SetActive(false);

    }

}
