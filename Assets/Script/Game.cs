using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Game : MonoBehaviour {
    public int ii; //first씬으로 부터 받은 배열 넘버
    public int nowMapNum;
    public int gridWidth ;
    public int gridHeight ;
    public int gridz ;

    public Text NowMapName;
    public int NowTetNameNum;

    public Transform[,,] grid;

    public UserDataManager currentUDM;
    private GameObject ghostTetromino;
    private GameObject nextTetromino;
    private GameObject gridCube;
    public GameObject TetrominoSet;
    public GameObject mainCamera;
    public GameObject SavePanel;
    public GameObject LoadPanel;
    public GameObject Menu;
    public GameObject CameraSetting;
    public GameObject ColorPannel;
    public GameObject TypePannel;

    public GameObject popup_Window1;
    public GameObject popup_Window2;
    public GameObject popup_Window3;
    public GameObject HelpPannel;
    public HelpButton HelpButton;

    public Ads_Manager UnityAds;

    public GameObject indicater;
    public GameObject mapRemovePopup;
    public Text RemoveMapname;
    

    public GameObject QuitMenu;
    public bool AudioSwitch = true;

    public Vector3 spawnPos;
    public Quaternion spawnRot;

    public TypeButton TetType;
    public ColorButton TetColor;

    public Material Sky1;
    public Material Sky2;
    public Material Sky3;
    public Material Sky4;
    public Material Sky5;

    public AudioSource BGMAudio;
    public AudioSource moveAudio;
    public AudioSource slamAudio;
    public AudioSource backAudio;
    public AudioSource frontAudio;
    private float timer = 0.0f;

    // Use this for initialization

    void Awake()
    {
       
      // QualitySettings.vSyncCount = 0;
      // Application.targetFrameRate = 30;
        
        ChangeSky();
        GameObject CurrentTetromino = GameObject.FindGameObjectWithTag("currentActiveTetromino");
        spawnPos = new Vector3(Mathf.RoundToInt(gridWidth / 2), 100.0f, Mathf.RoundToInt(gridz / 2));


        

    }
    void Start ()
    {

        ii = PlayerPrefs.GetInt("Initial_Number");
        nowMapNum = ii;
        TetrominoSet.GetComponent<TetrominoSet>().TetDB.mapName = playerDataBase.StaticPlayerDB().aa.MapList[ii].mapName;
        TetrominoSet.GetComponent<TetrominoSet>().TetDB.mapSize = playerDataBase.StaticPlayerDB().aa.MapList[ii].mapSize;
        TetrominoSet.GetComponent<TetrominoSet>().TetDB.TetList.Clear();
        TetrominoSet.GetComponent<TetrominoSet>().TetDB.TetList.AddRange(playerDataBase.StaticPlayerDB().aa.MapList[ii].TetList);

        gridWidth = (int)TetrominoSet.GetComponent<TetrominoSet>().TetDB.mapSize.x;
        gridz = (int)TetrominoSet.GetComponent<TetrominoSet>().TetDB.mapSize.y;
        grid = new Transform[gridWidth, gridHeight, gridz];

        DisplayTetrominoSet();
        SpawnNextTetromino();
       
        DisplayGridCube();
        TetrominoSet.GetComponent<TetrominoSet>().HistorySystemInit();
        DispNowMapName();

        if (playerDataBase.StaticPlayerDB().BGM_Switch)
        {
            BGMAudio.Play(104100);
        }

        if(playerDataBase.StaticPlayerDB().IsBeginer)
        {
            Open_HelpPannel();
        }
    }

// Update is called once per frame
    void Update () {
        
      
            if(Input.GetKeyDown(KeyCode.Escape))
            {
               if (!HelpPannel.activeInHierarchy)
               {
                  QuitMenu.SetActive(true);
               }
               else if(HelpPannel.activeInHierarchy)
               {
                  HelpButton.closeHelpPannel();
               }   
            }
        


    }



    public void DispNowMapName()
    {
        NowMapName.text = TetrominoSet.GetComponent<TetrominoSet>().TetDB.mapName;
    }


    public void LoadItemButtonClick()//각종 초기화 + 보여줌 
    {
        ChangeSky();
        DispNowMapName();
        gridWidth = (int)TetrominoSet.GetComponent<TetrominoSet>().TetDB.mapSize.x;
        gridz = (int)TetrominoSet.GetComponent<TetrominoSet>().TetDB.mapSize.y;
        grid = new Transform[gridWidth, gridHeight, gridz];
        DisplayTetrominoSet();
        DisplayGridCube();
        nextTetromino.transform.position = new Vector3(Mathf.RoundToInt(gridWidth / 2), 100.0f, Mathf.RoundToInt(gridz / 2));
        ghostTetromino.GetComponent<GhostTetromino>().iniGhostTet();


        mainCamera.GetComponent<MainCameraScript>().CameraInitialization();

        mainCamera.GetComponent<MainCameraScript>().TranslateCamera();
        TetrominoSet.GetComponent<TetrominoSet>().HistorySystemInit();
       
    }

    public void FrontBackLoadTet()
    {
        DisplayTetrominoSet();
        DisplayGridCube();
    }

    public void UpdateGrid (Tetromino tetromino)
    {
        for(int y=0; y<gridHeight; ++y )
            for(int x=0;x<gridWidth;++x)
                for (int z = 0; z < gridz; ++z)
                {
                  if (grid[x, y, z] != null)
                  {
                    if(grid[x,y, z].parent==tetromino.transform)
                    {
                        grid[x, y, z] = null;
                    }
                  }
                }
        foreach(Transform mino in tetromino.transform)
        {
            Vector3 pos = Round(mino.position);
            if(pos.y<gridHeight)
            {
                grid[(int)pos.x, (int)pos.y, (int)pos.z] = mino;
            }
        }
    }
    public Transform GetTransformAtGridPosition (Vector3 pos)
    {
        if(pos.y>gridHeight-1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y, (int)pos.z];
        }
    }

    public void SpawnNextTetromino()
    {
        NowTetNameNum = GetRandomTetrominoNum(); 

        nextTetromino = (GameObject)Instantiate(Resources.Load(NumtoString(NowTetNameNum), typeof(GameObject)), new Vector3(spawnPos.x, 100, spawnPos.z) , Quaternion.identity);
        nextTetromino.transform.rotation = spawnRot;

        nextTetromino.tag = "currentActiveTetromino";
        SpawnGhostTetromino();
        changeTetColor();
       
    }

    public bool CheckIsInsideGrid(Vector3 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= 0 && (int)pos.z >= 0 && (int)pos.z < gridz);
    }

    public Vector3 Round(Vector3 pos)
    {
        return new Vector3 (Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
    }

    public void DisplayGridCube()
    {
        float gridx=TetrominoSet.GetComponent<TetrominoSet>().TetDB.mapSize.x;
        float gridz=TetrominoSet.GetComponent<TetrominoSet>().TetDB.mapSize.y;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }


        gridCube = (GameObject)Instantiate(Resources.Load("Prefabs/mino_Grid 1", typeof(GameObject)), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        gridCube.transform.localScale = new Vector3(gridx, 1.0f, gridz);
        gridCube.transform.position = new Vector3((gridx/2)-0.5f, -1.0f, (gridz/2)-0.5f);
        gridCube.transform.parent = transform;
            
    }

    public void SpawnGhostTetromino()
    {

        if (GameObject.FindGameObjectsWithTag("currentGhostTetromino") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("currentGhostTetromino"));
        }

        ghostTetromino = (GameObject)Instantiate(Resources.Load(GhostNumtoString(NowTetNameNum)), nextTetromino.transform.position,Quaternion.identity);

     // Destroy(ghostTetromino.GetComponent<Tetromino>());

     // ghostTetromino.AddComponent<GhostTetromino>();
        
    }


    public void DisplayTetrominoSet()
    {
        foreach (Transform child in TetrominoSet.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (TetrominoInfo tetinfo in TetrominoSet.GetComponent<TetrominoSet>().TetDB.TetList)
        {
            GameObject newTet = Instantiate(Resources.Load(NumtoString(tetinfo.TetrominoNameNum)), new Vector3(tetinfo.tetromino_position.x, tetinfo.tetromino_position.y, tetinfo.tetromino_position.z), Quaternion.identity) as GameObject;
            newTet.transform.Rotate(tetinfo.tetromino_rotation);
            

            newTet.transform.parent=TetrominoSet.transform;

            Set_UVs[] minos2 = newTet.GetComponentsInChildren<Set_UVs>();
            //Debug.Log(minos2.Length);
            for (int i = 0; i < minos2.Length; i++)
            {

                minos2[i].gameObject.GetComponent<Set_UVs>().tilePos = tetinfo.TetColor;
                minos2[i].gameObject.GetComponent<Set_UVs>().SetUVsNow();
               // minos2[i].transform.parent = transform.parent;

            }
            
            UpdateGrid(newTet.GetComponent<Tetromino>());
            //this.gameObject.SetActive(false);
            newTet.GetComponent<Tetromino>().enabled = false;
        }


    }

    public void Spown_Front_TET(int pointer)
    {
        TetrominoInfo tetinfo = TetrominoSet.GetComponent<TetrominoSet>().TetDB.TetList[pointer];
        GameObject newTet = Instantiate(Resources.Load(NumtoString(tetinfo.TetrominoNameNum)), new Vector3(tetinfo.tetromino_position.x, tetinfo.tetromino_position.y, tetinfo.tetromino_position.z), Quaternion.identity) as GameObject;
        newTet.transform.Rotate(tetinfo.tetromino_rotation);


        newTet.transform.parent = TetrominoSet.transform;

        Set_UVs[] minos2 = newTet.GetComponentsInChildren<Set_UVs>();
        //Debug.Log(minos2.Length);
        for (int i = 0; i < minos2.Length; i++)
        {

            minos2[i].gameObject.GetComponent<Set_UVs>().tilePos = tetinfo.TetColor;
            minos2[i].gameObject.GetComponent<Set_UVs>().SetUVsNow();
            // minos2[i].transform.parent = transform.parent;

        }

        UpdateGrid(newTet.GetComponent<Tetromino>());
        //this.gameObject.SetActive(false);
        newTet.GetComponent<Tetromino>().enabled = false;
    }


    public void IsSpawnComplite()
    {

        spawnRot = nextTetromino.transform.rotation;
        nextTetromino.tag= "Untagged";
        nextTetromino.transform.parent = TetrominoSet.transform;
        //nextTetromino.GetComponent<OverEat>().OverEatMinos();
        // TetrominoSet.GetComponent<TetrominoSet>().combine();


        // Debug.Log(CurrentTetromino.transform.position.x + "," + CurrentTetromino.transform.position.z);

        //save tetinfo to list
    }
    public void IsSpawnCompliteAndDestory()
    {

        spawnRot = nextTetromino.transform.rotation;
        nextTetromino.tag = "Untagged";
        //nextTetromino.transform.parent = TetrominoSet.transform;
        Destroy(nextTetromino.gameObject);
        //nextTetromino.GetComponent<OverEat>().OverEatMinos();
        // TetrominoSet.GetComponent<TetrominoSet>().combine();


        // Debug.Log(CurrentTetromino.transform.position.x + "," + CurrentTetromino.transform.position.z);

        //save tetinfo to list
    }



    public void AddTetrominoToList()
    {
        

        TetrominoInfo Tet1 = new TetrominoInfo();
        Tet1.TetrominoNameNum = NowTetNameNum;
        Tet1.tetromino_position.x = nextTetromino.transform.position.x;
        Tet1.tetromino_position.y = nextTetromino.transform.position.y;
        Tet1.tetromino_position.z = nextTetromino.transform.position.z;
        Tet1.tetromino_rotation = nextTetromino.transform.rotation.eulerAngles;
        Tet1.TetColor = nextTetromino.GetComponentInChildren<Set_UVs>().tilePos;

        TetrominoSet.GetComponent<TetrominoSet>().TetDB.TetList.Add(Tet1);
    }

    public string NumtoString(int num)
    {

        string randomTetrominoName = "Prefabs/1X1_Tet";
        switch (num)
        {
            case 0:
                randomTetrominoName = "Prefabs/1X1_Tet";
                break;
            case 1:
                randomTetrominoName = "Prefabs/1X2_Tet";
                break;
            case 2:
                randomTetrominoName = "Prefabs/1X3_Tet";
                break;
            case 3:
                randomTetrominoName = "Prefabs/2X2_Tet";
                break;
            case 4:
                randomTetrominoName = "Prefabs/2X2L_Tet";
                break;
            case 5:
                randomTetrominoName = "Prefabs/2X3_Tet";
                break;

        }
        return randomTetrominoName;


    }
    
    public string GhostNumtoString(int num)
    {

        string randomTetrominoName = "Prefabs/1X1_GhostTet";
        switch (num)
        {
            case 0:
                randomTetrominoName = "Prefabs/1X1_GhostTet";
                break;
            case 1:
                randomTetrominoName = "Prefabs/1X2_GhostTet";
                break;
            case 2:
                randomTetrominoName = "Prefabs/1X3_GhostTet";
                break;
            case 3:
                randomTetrominoName = "Prefabs/2X2_GhostTet";
                break;
            case 4:
                randomTetrominoName = "Prefabs/2X2L_GhostTet";
                break;
            case 5:
                randomTetrominoName = "Prefabs/2X3_GhostTet";
                break;

        }
        return randomTetrominoName;


    }


    public Material GetSkyBoxName()
    {
        int randomnum = Random.Range(1, 5);
        Material randomSkyBox = Sky1;
        switch (randomnum)
        {
            case 1:
                randomSkyBox = Sky1;
                break;
            case 2:
                randomSkyBox = Sky2;
                break;
            case 3:
                randomSkyBox = Sky3;
                break;
            case 4:
                randomSkyBox = Sky4;
                break;
            case 5:
                randomSkyBox = Sky5;
                break;

        }
        return randomSkyBox;

    }

    public void ChangeSky()
    {
        Material Mat = GetSkyBoxName();
        RenderSettings.skybox = Mat;
    }



    public int GetRandomTetrominoNum()
    {
        int TetrominoNum = TetType.typeNum;
        return TetrominoNum;
    }

    public void changeTetType()
    {

        nextTetromino.GetComponent<Tetromino>().enabled = false;
        nextTetromino.tag = "Untagged";
        Destroy(nextTetromino.gameObject);

        NowTetNameNum = GetRandomTetrominoNum();

        nextTetromino = (GameObject)Instantiate(Resources.Load(NumtoString(NowTetNameNum), typeof(GameObject)), new Vector3(Mathf.RoundToInt(gridWidth / 2), 100.0f, Mathf.RoundToInt(gridz / 2)), Quaternion.identity);
        nextTetromino.tag = "currentActiveTetromino";
        SpawnGhostTetromino();
        changeTetColor();

    }

    public Vector2 colorChoose(int num)
    {
        switch (num)
        {
            case 0: //white
                {
                    return new Vector2(0.5f, 9.5f);
                    break;
                }
            case 1: //Black
                {
                    return new Vector2(0.5f ,6.5f);
                    break;
                }
            case 2: //Red
                {
                    return new Vector2(0.5f, 5.5f);
                    break;
                }
            case 3: //Green
                {
                    return new Vector2(0.5f, 2.5f);
                    break;
                }
            case 4: //Blue
                {
                    return new Vector2(0.5f, 1.5f);
                    break;
                }
            case 5: //Yellow
                {
                    return new Vector2(0.5f, 3.5f);
                    break;
                }
        }
        return new Vector2(0.5f, 9.5f);
    }


    public void changeTetColor()
    {
        int num = TetColor.colorNum;

        foreach (Transform mino in nextTetromino.transform)
        {

            mino.gameObject.GetComponent<Set_UVs>().tilePos = colorChoose(num);
            mino.gameObject.GetComponent<Set_UVs>().SetUVsNow();
        }
        foreach (Transform mino in ghostTetromino.transform)
        {
            mino.gameObject.GetComponent<Set_UVs>().tilePos = colorChoose(num);
            mino.gameObject.GetComponent<Set_UVs>().SetUVsNow();
        }

    }
    public void IndicateDeactive()
    {
        indicater.transform.parent = null;
        currentUDM.indicaterOn = false;
    }

    public void OpenSavepanel()
    {
        SavePanel.SetActive(true);
    }
    public void CloseSavepanel()
    {
        IndicateDeactive();
        SavePanel.SetActive(false);
    }

    public void OpenLoadpanel()
    {
        LoadPanel.SetActive(true);
    }
    public void CloseLoadpanel()
    {
      
        IndicateDeactive();
        LoadPanel.SetActive(false);
    }
    public void BacktoFirstScene()
    {
        Application.LoadLevel("First");
    }
    public void Open_Menu()
    {
        Menu.SetActive(true);
    }
    public void Close_Menu()
    {
        Menu.SetActive(false);
    }

    public void Open_Quit_Game_Menu()
    {
        QuitMenu.SetActive(true);
    }
    public void Close_Quit_Game_Menu()
    {
        QuitMenu.SetActive(false);
    }

    public void Open_CameraSetting_Menu()
    {
        CameraSetting.SetActive(true);
    }
    public void Close_CameraSetting_Menu()
    {
        CameraSetting.SetActive(false);
    }



    public void Application_Quit()
    {
        Application.Quit();
    }

    public void turn_on_BGM()
    {
        BGMAudio.Play();
    }
    public void turn_off_BGM()
    {
        BGMAudio.Stop();
    }

    public void Open_Remove_Popup()
    {  
        if (currentUDM.indicaterOn && !currentUDM.PlusButtonCheck && nowMapNum != currentUDM.WantToSaveNumb)
        {
            if(currentUDM.WantToSaveNumb<nowMapNum)//앞에있는거 삭제
            {
                nowMapNum--;
            }
            RemoveMapname.text = playerDataBase.StaticPlayerDB().aa.MapList[currentUDM.WantToSaveNumb].mapName;
            mapRemovePopup.SetActive(true);
           
        }
        
        else if(currentUDM.indicaterOn && !currentUDM.PlusButtonCheck && nowMapNum == currentUDM.WantToSaveNumb)
        {
            Open_CantRemove_Popup();
        }
        else if(currentUDM.indicaterOn && currentUDM.PlusButtonCheck || !currentUDM.indicaterOn)
        {
            Open_CantChoose_Popup();
        }
    }
    public void Close_Remove_Popup()
    {
        mapRemovePopup.SetActive(false);
    }
    public void RemoveMapData()
    {
        indicater.transform.parent = null;
        playerDataBase.StaticPlayerDB().aa.MapList.RemoveAt(currentUDM.WantToSaveNumb);

    }

    public void LoadCheckButtonClick()
    {
        if (currentUDM.indicaterOn)
        {
            currentUDM.CopyLoadtoCurrentTetrominoSet();
            LoadItemButtonClick();
            CloseLoadpanel();
        }
        else
        {
            Open_CantChoose_Popup();
        }
    }

    public void Open_Save_complete_Popup()
    {
        StartCoroutine(ResetPopup());
    }

    IEnumerator ResetPopup()
    {
        
        popup_Window1.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        popup_Window1.SetActive(false);
        UnityAds.Adsplay();
    }

    public void Open_CantRemove_Popup()
    {
        StartCoroutine(ResetPopup2());
    }

    IEnumerator ResetPopup2()
    {
        popup_Window2.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        popup_Window2.SetActive(false);
    }
    public void Open_CantChoose_Popup()
    {
        StartCoroutine(ResetPopup3());
    }

    IEnumerator ResetPopup3()
    {
        popup_Window3.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        popup_Window3.SetActive(false);
    }

    public void Open_HelpPannel()
    {
        HelpPannel.SetActive(true);
    }

    public void Open_ColorPannel()
    {
        ColorPannel.SetActive(true);
    }

    public void Close_ColorPannel()
    {
        ColorPannel.SetActive(false);
    }

    public void Open_TypePannel()
    {
        TypePannel.SetActive(true);
    }

    public void Close_TypePannel()
    {
        TypePannel.SetActive(false);
    }

    public void ColorPannelClick()
    {
        if (!ColorPannel.activeInHierarchy)
        {
            ColorPannel.SetActive(true);
        }
        else
        {
            ColorPannel.SetActive(false);
        }
    }

    public void TypePannelClick()
    {
        if (!TypePannel.activeInHierarchy)
        {
            TypePannel.SetActive(true);
        }
        else
        {
            TypePannel.SetActive(false);
        }
    }

}
