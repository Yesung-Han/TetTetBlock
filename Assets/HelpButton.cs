using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HelpButton : MonoBehaviour {
    public Sprite Help1;
    public Sprite Help2;
    public Sprite Help3;

    public Sprite HelpEng1;
    public Sprite HelpEng2;
    public Sprite HelpEng3;

    public int state=0;

    public GameObject HelpPannel;

    // Use this for initialization
    void Awake ()
    {
        if (playerDataBase.StaticPlayerDB().LangCode == 23)
        {
            this.gameObject.GetComponent<Image>().sprite = Help1;
        }
        else
        {
            this.gameObject.GetComponent<Image>().sprite = HelpEng1;
        }
        
    }

    public void closeHelpPannel()
    {
        state = 0;
        if (playerDataBase.StaticPlayerDB().LangCode == 23)
        {
          this.gameObject.GetComponent<Image>().sprite = Help1;
        }
        else
        {
          this.gameObject.GetComponent<Image>().sprite = HelpEng1;
        }


        HelpPannel.SetActive(false);
    }


    public void ChangeImg()
    {

        if (playerDataBase.StaticPlayerDB().LangCode == 23)
        {

            switch (state)
            {

                case 0:
                    {
                        this.gameObject.GetComponent<Image>().sprite = Help2;
                        state++;
                        break;
                    }
                case 1:
                    {
                        this.gameObject.GetComponent<Image>().sprite = Help3;
                        state++;
                        break;
                    }
                case 2:
                    {
                        closeHelpPannel();
                        break;
                    }

            }
        }
        else
        {
            switch (state)
            {

                case 0:
                    {
                        this.gameObject.GetComponent<Image>().sprite = HelpEng2;
                        state++;
                        break;
                    }
                case 1:
                    {
                        this.gameObject.GetComponent<Image>().sprite = HelpEng3;
                        state++;
                        break;
                    }
                case 2:
                    {
                        closeHelpPannel();
                        break;
                    }

            }
        }
    }
}
