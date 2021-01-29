using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextData : MonoBehaviour
{
    public List<string> data = new List<string>();
    string display = "";
    public Text showText;
    public Text searchText;
    public Text showReseach;

    void Start()
    {
        data = new List<string>();
        
        data.Add("Thyme");
        data.Add("Por");
        data.Add("Namtan");
        data.Add("Yok");
        data.Add("Bright");
 
        ShowText();
    }

    public void ShowText()
    {
        foreach (string dataList in data)
        {
            display = display.ToString() + dataList.ToString() + "\n";
        }

        showText.text = display;
    }

    public void checkData()
    {
        if (data.Contains(searchText.text))
        {
            showReseach.text = string.Format("Found <color=green>{0}</color> in data", searchText.text);
        }
        
        else
        {
            showReseach.text = string.Format("Not Found <color=red>{0}</color> in data", searchText.text);
        }

    }

}
