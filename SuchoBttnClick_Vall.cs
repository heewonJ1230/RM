using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuchoBttnClick_Vall : MonoBehaviour
{

    public static bool isclicked_Vall = false;

    public Button buyBttn_Vall;

    public GameObject graySucho_Vall = null;
    private GameObject createGray_Vall = null;
    public GameObject realSucho_Vall = null;

    GameManager gm;
    SuchoManager suchom;

    void Start()
    {
        isclicked_Vall = false;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        suchom = GameObject.Find("SuchoManager").GetComponent<SuchoManager>();
    }

    private void Update()
    {
        Vall_ButtonCheck();
        if (isclicked_Vall)
        {
            CreatSucho();
        }
    }

    private void OnMouseDown()
    {
        if (gm.money >= SuchoWork.sucho_autoIncreasePrice_Vall)
        {
            isclicked_Vall = true;
            createGray_Vall = Instantiate(graySucho_Vall, transform.position, Quaternion.identity);
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z = 0.0f;
            createGray_Vall.transform.position = mousepos;
        }
    }

    private void OnMouseDrag()
    {
        if (isclicked_Vall == true)
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z = 0.0f;
            createGray_Vall.transform.position = mousepos;
        }
    }

    void CreatSucho()
    {

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            FXController fXController_ep1 = GameObject.Find("FXManager_Game").GetComponent<FXController>();

            if (mousePosition.y < - 0.1f)
            {

                fXController_ep1.HchoAferFx();
                isclicked_Vall = false;
                Instantiate(realSucho_Vall, createGray_Vall.transform.position, Quaternion.identity);
                suchom.suchoPositions_Vals.Add(createGray_Vall.transform.position);

                //Debug.Log("발리스네리아 구매");
                gm.money -= SuchoWork.sucho_autoIncreasePrice_Vall;
                suchom.valCount += 1;
                SuchoWork.sucho_autoMoneyIncreaseAmount += 1;
                SuchoWork.sucho_autoIncreasePrice_Vall += gm.moneyIncreaseLevel * suchom.valCount * 191;

                createGray_Vall.SetActive(false);
                Destroy(createGray_Vall);
           
            }
            else
            {
                fXController_ep1.EndSFX();
                createGray_Vall.SetActive(false);
                Destroy(createGray_Vall);
               // Debug.Log("범위 밖!!");
            }
        }

    }
    void Vall_ButtonCheck()
    {
        if (gm.money >= SuchoWork.sucho_autoIncreasePrice_Vall)
        {
            buyBttn_Vall.interactable = true;
        }
        else
        {
            buyBttn_Vall.interactable = false;
        }
    }
}
