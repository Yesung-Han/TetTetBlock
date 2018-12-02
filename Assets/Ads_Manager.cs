using UnityEngine;
using UnityEngine.Advertisements;


public class Ads_Manager : MonoBehaviour {

    public string id = "1299272";
    ShowOptions showopt = new ShowOptions();


    //save관련 인스탄스
    public Game currentGameManager;
    public XMLManager currentXMLManager;
    public SavePanelBoxDisplay SaveContents;
    public UserDataManager UDM;

    //save인지 saveAppend인지 구분
    public bool Is_SaveButton = false;
    public bool Is_SaveAppendButton = false;

    public bool ADsReady;

    // Use this for initialization
    void Start () {
        if (Advertisement.isInitialized == false)
        {
            Advertisement.Initialize(id,true);
            
        }
        //ADsReady = Advertisement.IsReady();
        showopt.resultCallback = OnAdsShowResultCallBack;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void appendClick()
    {
        Is_SaveAppendButton = true;
    }
    public void saveClick()
    {
        Is_SaveButton = true;
    }

    public void Save_Append_Rutine()
    {
        UDM.AppendSaveThisMap();
        SaveContents.DeactivateAppendSavePopupWindow();
        currentXMLManager.SavePlayerDataXML();
        currentGameManager.DispNowMapName();
        currentGameManager.Open_Save_complete_Popup();
        Is_SaveAppendButton = false;
    }

    public void Save_Rutine()
    {
        UDM.SaveThisMap();
        SaveContents.DeactivateSavePopupWindow();
        currentXMLManager.SavePlayerDataXML();
        currentGameManager.DispNowMapName();
        currentGameManager.Open_Save_complete_Popup();
        Is_SaveButton = false;
    }


    void OnAdsShowResultCallBack(ShowResult result)
    {
   
    }
    
    public void Adsplay()
    {
        if(Advertisement.IsReady())
        {
            Advertisement.Show(null, showopt);
        }

    }


}
