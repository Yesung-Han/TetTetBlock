using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour {

    GameObject GameManager;
    UIButtonManager ButtonManager;
    GhostTetromino Ghost;

    float fall = 0;
    public float fallSpeed = 1;

    public bool allowRotation = true;
    public bool limitRotation = false;

    public bool inputLeft = false;
    public bool inputRight = false;
    public bool inputUp = false;
    public bool inputDown = false;
    public bool inputJump = false;
    public bool inputDownUp = false;
    public bool inputRotate = false;

    public bool onAndonUp = false;
    public bool onAndonDown = false;
    public bool onAndonRight = false;
    public bool onAndonLeft = false;

    public float timer = 0.0f;

    void Awake()
    {
        
    }


    // Use this for initialization
    void Start () {
        Ghost = FindObjectOfType<GhostTetromino>();
        GameManager = GameObject.FindGameObjectWithTag("GameController");
        ButtonManager = FindObjectOfType<UIButtonManager>();
        ButtonManager.Init();

       
        
    }
	
	// Update is called once per frame
	void Update () {
        
        CheckUserUnput();
       
   
    }







    public void MoveUp()
    {

        transform.position += new Vector3(0, 0, 1);
        GameManager.GetComponent<Game>().moveAudio.Play();
        if (CheckIsValidPosition())
        {
            GameManager.GetComponent<Game>().UpdateGrid(this);
        }
        else
        {
            transform.position += new Vector3(0, 0, -1);
        }
    }
    public void MoveDown()
    {
        transform.position += new Vector3(0, 0, -1);
        GameManager.GetComponent<Game>().moveAudio.Play();
        if (CheckIsValidPosition())
        {
            GameManager.GetComponent<Game>().UpdateGrid(this);
        }
        else
        {
            transform.position += new Vector3(0, 0, 1);
        }

    }
    public void MoveLeft()
    {
        transform.position += new Vector3(-1, 0, 0);
        GameManager.GetComponent<Game>().moveAudio.Play();
        if (CheckIsValidPosition())
        {
            GameManager.GetComponent<Game>().UpdateGrid(this);
        }
        else
        {
            transform.position += new Vector3(1, 0, 0);
        }

    }
    public void MoveRight()
    {
        transform.position += new Vector3(1, 0, 0);
        GameManager.GetComponent<Game>().moveAudio.Play();
        if (CheckIsValidPosition())
        {
            GameManager.GetComponent<Game>().UpdateGrid(this);
        }
        else
        {
            transform.position += new Vector3(-1, 0, 0);
        }

    }

    public void RotateTet()
    {
        GameManager.GetComponent<Game>().moveAudio.Play();
        if (allowRotation)
        {
            if (limitRotation)
            {
                if (transform.rotation.eulerAngles.y >= 90)
                {
                    transform.Rotate(0, -90, 0);
                }
                else
                {
                    transform.Rotate(0, 90, 0);
                }
            }
            else
            {
                transform.Rotate(0, 90, 0);
            }

            if (CheckIsValidPosition())
            {
                GameManager.GetComponent<Game>().UpdateGrid(this);
            }
            else
            {
                if (limitRotation)
                {
                    if (transform.rotation.eulerAngles.y >= 90)
                    {
                        transform.Rotate(0, -90, 0);
                    }
                    else
                    {
                        transform.Rotate(0, 90, 0);
                    }
                }
                else
                {
                    transform.Rotate(0, -90, 0);
                }
            }
        }

    }





    void CheckUserUnput()
    {
        if(inputRight)
        {
            timer += Time.deltaTime;
            if (timer>=0.5)
            {
                onAndonRight = true;
                timer = 0.0f;
            }
        }
      
        //------------------------
       else if (inputLeft)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5)
            {
                onAndonLeft = true;
                timer = 0.0f;
            }
        }
     
        //------------------------
        else if (inputUp)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5)
            {
                onAndonUp = true;
                timer = 0.0f;
            }
        }
       
        //------------------------
        else if (inputDown)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5)
            {
                onAndonDown = true;
                timer = 0.0f;
            }
        }
     
        //------------------------


        if (onAndonDown)
        {
            MoveDown();
        }
        else if (onAndonRight)
        {
            MoveRight();
        }
        else if (onAndonUp)
        {
            MoveUp();
        }
        else if (onAndonLeft)
        {
            MoveLeft();
        }


    }

    public void SlamDown()
    {
        GameManager.GetComponent<Game>().slamAudio.Play();

        this.transform.position = Ghost.transform.position;
        


            GameManager.GetComponent<Game>().UpdateGrid(this);

            GameManager.GetComponent<Game>().AddTetrominoToList();

            GameManager.GetComponent<Game>().IsSpawnComplite();
            GameManager.GetComponent<Game>().SpawnNextTetromino();
            enabled = false;
        
    }

    bool CheckIsValidPosition()
    {
        foreach (Transform mino in transform)
        {
            Vector3 pos = GameManager.GetComponent<Game>().Round(mino.position);
            if(GameManager.GetComponent<Game>().CheckIsInsideGrid(pos)==false)
            {
                return false;
            }
            if(GameManager.GetComponent<Game>().GetTransformAtGridPosition(pos) != null && GameManager.GetComponent<Game>().GetTransformAtGridPosition(pos).parent != transform)
            {
                return false;
            }
        }

        return true;
    }

}
 