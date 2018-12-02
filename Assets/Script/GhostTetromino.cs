using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTetromino : MonoBehaviour {

    private float timer = 0.0f;
    private Vector3 prePos;
    private Vector3 currPos;
    public bool Activate =false;
    Vector2 tempColor;

    GameObject GameManager;
    GameObject currentActiveTet;
    // Use this for initialization
    public void Start () {

        GameManager = GameObject.FindGameObjectWithTag("GameController");
        currentActiveTet = GameObject.FindGameObjectWithTag("currentActiveTetromino");

        tag = "currentGhostTetromino";

        iniGhostTet();
        StartCoroutine(GhostTet());
        
        tempColor = gameObject.GetComponentInChildren<Set_UVs>().tilePos;
    }

    public void ChangeGhostTetColor(Vector2 TileColor)
    {
        foreach (Transform mino in transform)
        {
            mino.gameObject.GetComponent<Set_UVs>().tilePos = TileColor;
            mino.gameObject.GetComponent<Set_UVs>().SetUVsNow();
        }
    }


    public void iniGhostTet()
    {
        FollowActiveTetromino();
        MoveDown();
        WritePos();
    }


    public void FollowActiveTetromino()
    {

        transform.position = currentActiveTet.transform.position;
        transform.rotation = currentActiveTet.transform.rotation;

    }

    IEnumerator GhostTet()
    {
        for (;;)
        {
            if (Activate)
           {
            
                FollowActiveTetromino();
                MoveDown();
                WritePos();
               
            }
            yield return null;
        }
  }


    public void MoveDown()
    {
        while (CheckIsValidPosition())
        {
            transform.position += new Vector3(0, -1, 0);
        }
        if (!CheckIsValidPosition())
        {
            transform.position += new Vector3(0, 1, 0);
        }
    }

    public void GhostDownToUp()
    {
       
        
        do
        {
            transform.position += new Vector3(0, -1, 0);
        }
        while (!CheckIsValidPosition() && transform.position.y >= -1);

        if (CheckIsValidPosition())
        {
            transform.position += new Vector3(0, +1, 0);
            if (!CheckIsValidPosition())
            {
                transform.position += new Vector3(0, -1, 0);
                GameManager.GetComponent<Game>().moveAudio.Play();
            }
            else
            {
                GameManager.GetComponent<Game>().moveAudio.Play();
                MoveDown();
            }
                    
            //GameManager.GetComponent<Game>().UpdateGrid(this);

            //GameManager.GetComponent<Game>().AddTetrominoToList();

            //GameManager.GetComponent<Game>().IsSpawnComplite();
            //GameManager.GetComponent<Game>().SpawnNextTetromino();
            //enabled = false;
        }
        else
        {
            GameManager.GetComponent<Game>().backAudio.Play();
            Debug.Log("invalid pos!");
            //GameManager.GetComponent<Game>().AddTetrominoToList();

            GameManager.GetComponent<Game>().IsSpawnCompliteAndDestory();
            GameManager.GetComponent<Game>().SpawnNextTetromino();
           

        }

    }







    bool CheckIsValidPosition()
    {
        foreach(Transform mino in transform)
        {
            Vector3 pos = GameManager.GetComponent<Game>().Round(mino.position);
            if (GameManager.GetComponent<Game>().CheckIsInsideGrid(pos) == false)
                return false;
            if (GameManager.GetComponent<Game>().GetTransformAtGridPosition(pos) != null && GameManager.GetComponent<Game>().GetTransformAtGridPosition(pos).parent.tag == "currentActiveTetromino")
                return true;
            if (GameManager.GetComponent<Game>().GetTransformAtGridPosition(pos) != null && GameManager.GetComponent<Game>().GetTransformAtGridPosition(pos).parent != transform)
                return false;
        }
        return true;
    }

    public void WritePos()
    {
        Vector3 currPos = transform.position;
        GameManager.GetComponent<Game>().spawnPos = currPos;
    }

}
