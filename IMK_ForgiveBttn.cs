using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IMK_ForgiveBttn : MonoBehaviour
{
    [Header("용서버튼")]
    public Button forgiveBttn;
    public float resetSec;
    public bool isForgiveBttnClicked;

    IMKSpeakBbbMaker imksbb;
    IMK_FXController imkFx;
    MKmanager mkm;

    private void Start()
    {
        imksbb = GameObject.Find("IMKSpeakBbbMaker").GetComponent<IMKSpeakBbbMaker>();
        imkFx = GameObject.Find("IMK_FXController").GetComponent<IMK_FXController>();
        mkm = GameObject.Find("marimoInMarimoKill").GetComponent<MKmanager>();
    }
    private void Update()
    {
        if (isForgiveBttnClicked)
        {
            forgiveBttn.interactable = false;
        }
    }

    public void OnClickForgiveBttn()
    {
        isForgiveBttnClicked = true;
        mkm.ForgiveAnim();
        Invoke("ResetForgive", resetSec);
    }

    void ResetForgive()
    {
        isForgiveBttnClicked = false;
    }
}
