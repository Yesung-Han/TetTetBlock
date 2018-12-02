using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainCameraScript : MonoBehaviour {

    public Game GameManager;
    private Transform camTransform;
    public Transform KeyPad;
    //public GameObject GhostTetromino;
    public FocusButton currFocusButton;
    float timer=0.0f;
    float dist = 0.0f;

    float deltaX = 0.0f;
    float deltaY = 0.0f;
    public float Focus_height = 2.0f;
    public bool TrackingMode = false;

    private bool touchOn;
    private bool TrackingOn;
    public Slider ZoomSlider;
    public Slider Camera_UpDown_Slider;
    private float distance = 20.0f;
    private float currentX = 45.0f;
    private float currentY = 15.0f;
    private float sensivityX = 4.0f;
    private float sensivityY = 1.0f;
    public Vector3 LookAt;
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 80.0f;
    public TouchPanel nowTouchPanel;

    bool once = false;
    // Use this for initialization
    void Start () {
        CameraInitialization();
        TranslateCamera();
        currFocusButton = FindObjectOfType<FocusButton>();
    }


   
    void LateUpdate() {

        if (nowTouchPanel.IsTouchPanelActive)
        {
            if (touchOn)
            {
                RotateCamera();

            }
        }
        else
        {
            touchOn = false;
        }
       if (TrackingMode)
        {
            once = true;
            Camera_UpDown_Slider.interactable = false;
            TranslateCamera();
        }
        else
        {
            if (once)
            {
                Camera_UpDown_Slider.interactable = true;
                TranslateCamera();
                once = false;
            }
        }
        

        
       
    }

    public void CameraInitialization()
    {
        camTransform = transform;
        //camTransform.Rotate(new Vector3(-180, 0, 0));
        distance = (GameManager.gridWidth + GameManager.gridz);
        ZoomSlider.value = distance;
        Focus_height = 2.0f;
        Camera_UpDown_Slider.value = Focus_height;
        LookAt.x = GameManager.gridWidth / 2;
        LookAt.y = 5.0f;
        LookAt.z = GameManager.gridz / 2;

    }
    

    public void touchOnFunc()
    {
        touchOn=true;
     
    }

    public void touchOutFunc()
    {
        touchOn = false;
    }

    public void trackingOnFunc()
    {
        TrackingOn = false;
    }
    public void trackingOffFunc()
    {
        TrackingOn = false;
    }



    public void Zoom()
    {
      
        distance = ZoomSlider.value;
        TranslateCamera();
    }
    public void CameraUpDown()
    {

        Focus_height = Camera_UpDown_Slider.value;
        TranslateCamera();
    }


    public void RotateCamera()
    {
       
            deltaX = Input.GetAxis("Mouse X");
            deltaY = Input.GetAxis("Mouse Y");



                currentX += deltaX * 2.0f;
                currentY -= deltaY * 2.0f;
                currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

            TranslateCamera();
       
    }
    public void TranslateCamera()
    {
        timer = 0.0f;
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        timer += Time.deltaTime;
        LookAt = TrackingModeFunc();
        camTransform.position = LookAt + rotation * dir;
        camTransform.LookAt(LookAt);
        KeyPad.rotation= Quaternion.Euler(0, 0, currentX); //키패드 회전
    }

   private Vector3 TrackingModeFunc()
    {
        if(TrackingMode)
        {

            return Vector3.Lerp(LookAt, GameManager.spawnPos, timer);

        }
        else
        {
            return new Vector3(GameManager.gridWidth / 2, Focus_height, GameManager.gridz / 2);
            
        }

    }


}
