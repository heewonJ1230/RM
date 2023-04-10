using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SimpleMssgMan : MonoBehaviour
{
    public Text simpleTitleTxt;
    public Text simpleMssgTxt;
    public Text simpleBttnTxt;

    // Start is called before the first frame update

    private void Start()
    {
        
    }
    public void MakeSimpleMssg(string titleTxt, string mssgTxt, string bttnTxt)
    {
        simpleTitleTxt.text = titleTxt;
        simpleMssgTxt.text = mssgTxt;
        simpleBttnTxt.text = bttnTxt;
    }

    public void SMM_OK()//초기화
    {
        simpleBttnTxt.text = "";
        simpleMssgTxt.text = "";
        simpleTitleTxt.text = "";
      //  Debug.Log("심플메세지초기화 완료오옹.");
    }
}
