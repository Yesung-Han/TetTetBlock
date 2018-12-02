using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class ShareImageCanvas : MonoBehaviour
{
    // public
    // private
    private bool isProcessing = false;
    public Image buttonShare1;
    public Image buttonShare2;

    public GameObject GUI;
    public GameObject GUI2;

    public GameObject CurrentGhostTet;

    public GameObject FlashPannel;
    public GameObject GameLogo;

    public string mensaje;



    public void hideGUI()
    {
        CurrentGhostTet = GameObject.FindGameObjectWithTag("currentGhostTetromino");

        GUI.SetActive(false);
        CurrentGhostTet.SetActive(false);
        FlashPannel.SetActive(true);
        StartCoroutine(Fade());
        GUI2.SetActive(true);
        //ShareButton.SetActive(true);
    }
    public void DispGUI()
    {
        GUI2.SetActive(false);
        GUI.SetActive(true);
        CurrentGhostTet.SetActive(true);
        CurrentGhostTet.GetComponent<GhostTetromino>().Start();
        //ShareButton.SetActive(true);
    }


    IEnumerator Fade()
    {
        Color c = FlashPannel.GetComponent<Image>().color;
        c.a = 1;
        FlashPannel.GetComponent<Image>().color = c;

        for (float f = 1f; f >=0 ;f-=0.1f)
        {
            
            c.a = f;
            FlashPannel.GetComponent<Image>().color = c;

            yield return null;
        }
        FlashPannel.SetActive(false);
    }

    //function called from a button
    public void ButtonShare()
    {
        buttonShare1.enabled = false;
        buttonShare2.enabled = false;
        GameLogo.SetActive(true);

        if (!isProcessing)
        {
            StartCoroutine(ShareScreenshot());
            Debug.Log("Share OK!");
        }
    }
    public IEnumerator ShareScreenshot()
    {
        
        isProcessing = true;
        // wait for graphics to render
        yield return new WaitForEndOfFrame();
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
        // create the texture
        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        // put buffer into texture
        screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
        // apply
        screenTexture.Apply();

        
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
        byte[] dataToSave = screenTexture.EncodeToPNG();
        string destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
        File.WriteAllBytes(destination, dataToSave);
        if (!Application.isEditor)
        {
            // block to open the file and share it ------------START
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
           
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);

            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "" + mensaje);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "SUBJECT");

            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");
            currentActivity.Call("startActivity", jChooser);
        }
        isProcessing = false;
        buttonShare1.enabled = true;
        buttonShare2.enabled = true;
        GameLogo.SetActive(false);
        DispGUI();
    }
}