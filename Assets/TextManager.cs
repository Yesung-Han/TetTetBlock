using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine.UI;


public class TextManager : MonoBehaviour {

    public Dropdown LangDrop;

    public enum Enum_Language { kor, eng, chi };
    public Enum_Language Language = Enum_Language.kor;

    public Text NewText;
    public Text New_MapNameText;
    public Text New_SizeNameText;
    public Text New_Size_warn_Text;

    public Text LoadText;
    public Text Load_SelectWarn_Text;
    public Text Load_DeletWarn_Text;

    public Text Menu_BGM_Text;
    public Text Menu_Recommend_Text;
    public Text Menu_Reset_Text;
    public Text Menu_GameOff_Text;

    public Text Menu_Reset_Warn_Text;
    public Text Menu_Reset_confirm_Text;

    public Text Menu_GameOff_Warn_Text;

    public Text Lang;


    void Awake()
    {
        LoadingText();
        ChangeLang();
    }

    void Start()
    {

        if (playerDataBase.StaticPlayerDB().LangCode == 23)
        {
            LangDrop.value = 0;
        }
        else if (playerDataBase.StaticPlayerDB().LangCode == 10)
        {
            LangDrop.value = 1;
        }

    }

    public void ChangeLangCode(int langNum)
    {
        switch (langNum)
        {
            case 0:
                {
                    playerDataBase.StaticPlayerDB().LangCode = 23;
                    break;
                }
            case 1:
                {
                    playerDataBase.StaticPlayerDB().LangCode = 10;
                    break;
                }
        }
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
       NewText.text = GetLocalizingText(0);
       New_MapNameText.text = GetLocalizingText(1);
       New_SizeNameText.text = GetLocalizingText(2);
       New_Size_warn_Text.text = GetLocalizingText(3);

       LoadText.text = GetLocalizingText(4);
       Load_SelectWarn_Text.text = GetLocalizingText(5);
       Load_DeletWarn_Text.text = GetLocalizingText(6);

        Menu_BGM_Text.text = GetLocalizingText(7);
       Menu_Recommend_Text.text = GetLocalizingText(8);
       Menu_Reset_Text.text = GetLocalizingText(9);
       Menu_GameOff_Text.text = GetLocalizingText(10);

       Menu_Reset_Warn_Text.text = GetLocalizingText(11);
       Menu_Reset_confirm_Text.text = GetLocalizingText(12);

       Menu_GameOff_Warn_Text.text = GetLocalizingText(13);
       Lang.text = GetLocalizingText(31);
    }


    List<LocalWord> AllText = new List< LocalWord >();

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
        for(int i=0;i<Index_Table.Count;i++)
        {
            LocalWord mWord = new LocalWord();
            mWord.Index = System.Convert.ToInt32(Index_Table[i].InnerText);
            mWord.kor = kor_Table[i].InnerText;
            mWord.eng = eng_Table[i].InnerText;
            //mWord.chi = chi_Table[i].InnerText;
            AllText.Add(mWord);
        }
    }

    

    public string GetLocalizingText(int Index)
    {
        if(AllText[Index].Index == Index)
        {
            switch(Language)
            {
                case Enum_Language.kor :
                    return AllText[Index].kor;

                case Enum_Language.eng:
                    return AllText[Index].eng;

                case Enum_Language.chi:
                    return AllText[Index].chi;
            }
        }
        for(int i=0; i < AllText.Count ; i++)
        {
            if(AllText[i].Index == Index)
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

public class LocalWord
{
    public int Index;
    public string kor;
    public string eng;
    public string chi;
}