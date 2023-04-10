using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCheckonce : MonoBehaviour
{

    public string goName;
    public GameObject openck_go;
    private int openCheckOnce;

    private void Start()
    {
        openck_go.SetActive(false);
        openCheckOnce = PlayerPrefs.GetInt(goName, 0);
        ShowitSelfOnce();

   //     Debug.Log(openCheckOnce);
   //     Debug.Log(PlayerPrefs.GetInt(goName, 0));
    }
   
    void ShowitSelfOnce()
    {
        if (openCheckOnce == 0)
        {
            openck_go.SetActive(true);
            if (openck_go.activeSelf == true)
            {
                openCheckOnce = 1;
                PlayerPrefs.SetInt(goName, openCheckOnce);
            }
        }
        else { openck_go.SetActive(false); }
       
    }
}
