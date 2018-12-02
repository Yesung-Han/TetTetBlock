using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine.UI;


public class TextManager2 : MonoBehaviour
{

    public Text Menu_GotoMain_Text;
    public Text Menu_BGM_Text;
    public Text Menu_Help_Text;
    public Text Menu_Save_Text;
    public Text Menu_Load_Text;
    public Text Menu_GameOff_Text;

    public Text SaveLoad_SelectWarn_Text;
    public Text SaveLoad_NowMapSelectWarn_Text;
    public Text SaveLoad_DeletWarn_Text;

    public Text Save_SaveNew_Text;
    public Text Save_SaveAppend_Text;
    public Text Save_SaveConfirm_Text;

    public Text CamerSetting_TrackingMode_Text;
    public Text CamerSetting_ScreenShot_Text;
    public Text CamerSetting_FocusHeight_Text;
    public Text CamerSetting_Zoom_Text;


    public Text GameOff_Warn_Text;



    void Awake()
    {
        LoadingText();
        ChangeLang();
    }

    public void ChangeLang()
    {
        switch (playerDataBase.StaticPlayerDB().LangCode)
        {
            case 23:
                {
                    Language = Enum_Language.kor;
                    ApplyText();
                    break;
                }
            case 10:
                {
                    Language = Enum_Language.eng;
                    ApplyText();
                    break;
                }
        }
    }
    public void ApplyText()
    {
        Menu_GotoMain_Text.text = GetLocalizingText(14);
        Menu_BGM_Text.text = GetLocalizingText(15);
        Menu_Help_Text.text = GetLocalizingText(16);
        Menu_Save_Text.text = GetLocalizingText(17);
        Menu_Load_Text.text = GetLocalizingText(18);
        Menu_GameOff_Text.text = GetLocalizingText(19);

        SaveLoad_SelectWarn_Text.text = GetLocalizingText(20);
        SaveLoad_NowMapSelectWarn_Text.text = GetLocalizingText(21);
        SaveLoad_DeletWarn_Text.text = GetLocalizingText(22);

        Save_SaveNew_Text.text = GetLocalizingText(23);
        Save_SaveAppend_Text.text = GetLocalizingText(24);
        Save_SaveConfirm_Text.text = GetLocalizingText(25);

        CamerSetting_TrackingMode_Text.text = GetLocalizingText(26);
        CamerSetting_ScreenShot_Text.text = GetLocalizingText(27);
        CamerSetting_FocusHeight_Text.text = GetLocalizingText(28);
        CamerSetting_Zoom_Text.text = GetLocalizingText(29);
        GameOff_Warn_Text.text = GetLocalizingText(30);
    }

    List<LocalWord> AllText = new List<LocalWord>();

    void LoadingText()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Texts");
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);

        XmlNodeList Index_Table = xmldoc.GetElementsByTagName("index");
        XmlNodeList menu_Table = xmldoc.GetElementsByTagName("menu");
        XmlNodeList kor_Table = xmldoc.GetElementsByTagName("kor");
        XmlNodeList eng_Table = xmldoc.GetElementsByTagName("eng");
        //XmlNodeList chi_Table = xmldoc.GetElementsByTagName("chi");
        for (int i = 0; i < Index_Table.Count; i++)
        {
            LocalWord mWord = new LocalWord();
            mWord.Index = System.Convert.ToInt32(Index_Table[i].InnerText);
            mWord.kor = kor_Table[i].InnerText;
            mWord.eng = eng_Table[i].InnerText;
            //mWord.chi = chi_Table[i].InnerText;
            AllText.Add(mWord);
        }
    }

    public enum Enum_Language { kor, eng, chi };
    public Enum_Language Language;

    public string GetLocalizingText(int Index)
    {
        if (AllText[Index].Index == Index)
        {
            switch (Language)
            {
                case Enum_Language.kor:
                    return AllText[Index].kor;

                case Enum_Language.eng:
                    return AllText[Index].eng;

                case Enum_Language.chi:
                    return AllText[Index].chi;
            }
        }
        for (int i = 0; i < AllText.Count; i++)
        {
            if (AllText[i].Index == Index)
            {
                switch (Language)
                {
                    case Enum_Language.kor:
                        return AllText[Index].kor;

                    case Enum_Language.eng:
                        return AllText[Index].eng;

                    case Enum_Language.chi:
                        return AllText[Index].chi;
                }
            }
        }

        Debug.Log("반환된 Text가 없습니다.");
        return "반환된 Text가 없습니다.";
    }

}