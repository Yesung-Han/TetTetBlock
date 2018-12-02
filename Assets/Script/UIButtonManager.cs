using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonManager : MonoBehaviour {

    GameObject PlayerManager;
    Tetromino PlayerMovement;
    GhostTetromino Ghost;
    
    public void Init()
    {
        PlayerManager = GameObject.FindGameObjectWithTag("currentActiveTetromino");
        PlayerMovement = PlayerManager.GetComponent<Tetromino>();
        Ghost = FindObjectOfType<GhostTetromino>();
    }


    public void UpClickDown()
    {
        PlayerMovement.MoveUp();
        PlayerMovement.timer = 0.0f;
        PlayerMovement.onAndonUp = false;
        PlayerMovement.inputUp = true;
        Ghost.Activate = true;
    }
    public void UpClickup()
    {
        PlayerMovement.inputUp = false;
        PlayerMovement.timer = 0.0f;
        PlayerMovement.onAndonUp = false;
        Ghost.Activate = false;
    }
    public void DownClickDown()
    {
        PlayerMovement.MoveDown();
        PlayerMovement.timer = 0.0f;
        PlayerMovement.onAndonDown = false;
        PlayerMovement.inputDown = true;
        Ghost.Activate = true;
    }
    public void DownClickUp()
    {
        PlayerMovement.inputDown = false;
        PlayerMovement.timer = 0.0f;
        PlayerMovement.onAndonDown = false;
        Ghost.Activate = false;
    }
    public void LeftClickDown()
    {
        PlayerMovement.MoveLeft();
        PlayerMovement.timer = 0.0f;
        PlayerMovement.onAndonLeft = false;
        PlayerMovement.inputLeft = true;
        Ghost.Activate = true;
    }
    public void LeftClickUp()
    {
        PlayerMovement.inputLeft = false;
        PlayerMovement.timer = 0.0f;
        PlayerMovement.onAndonLeft = false;
        Ghost.Activate = false;
    }
    public void RightClickDown()
    {
        PlayerMovement.MoveRight();
        PlayerMovement.timer = 0.0f;
        PlayerMovement.onAndonRight = false;
        PlayerMovement.inputRight = true;
        Ghost.Activate = true;
    }
    public void RightClickUP()
    {
        PlayerMovement.inputRight = false;
        PlayerMovement.timer = 0.0f;
        PlayerMovement.onAndonRight = false;
        Ghost.Activate = false;
    }

    public void JumpCickDown()
    {
        PlayerMovement.SlamDown();
        PlayerMovement.inputJump = true;
        //Ghost.Activate = true;
    }

    public void JumpCickUp()
    {
        PlayerMovement.inputJump = false;
        //Ghost.Activate = false;
    }

    public void DownUpKeyDown()
    {
        //PlayerMovement.DownToUp();
        Ghost.GhostDownToUp();
        PlayerMovement.inputDownUp = true;
       
    }

    public void RotateCickDown()
    {
        PlayerMovement.RotateTet();
        PlayerMovement.inputRotate = true;
        Ghost.Activate = true;
    }

    public void RotateCickUp()
    {
        PlayerMovement.inputRotate = false;
        Ghost.Activate = false;
    }

 
}
