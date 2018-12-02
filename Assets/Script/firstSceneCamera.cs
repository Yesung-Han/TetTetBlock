using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstSceneCamera : MonoBehaviour {
    private Transform camTransform;
    // Use this for initialization
    void Start () {
        camTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {

        camTransform.Rotate(new Vector3(0.0f,0.5f,0.0f));


    }


}
