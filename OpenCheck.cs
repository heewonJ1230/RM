using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCheck : MonoBehaviour
{
    public bool isGongSaJung;

    public Button bttn;
    public Text text;

    private void Start()
    {       
        GongSa();
    }
   

   void GongSa()
    {
        if (isGongSaJung)
        {
            bttn.interactable = false;
            text.fontStyle = FontStyle.Bold;
            text.text = "공사 중...";
        }
    }
  
}
