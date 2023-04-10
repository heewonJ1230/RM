using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuchoBttnClick_Lud : MonoBehaviour
{

    public static bool isclicked_Lud = false;

    public Button buyBttn_Lud;

    public GameObject graySucho_Lud = null;
    private GameObject createGray_Lud = null;
    public GameObject realSucho_Lud = null;

    GameManager gm;
    SuchoManager suchom;

    void Start()
    {
        isclicked_Lud = false;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        suchom = GameObject.Find("SuchoManager").GetComponent<SuchoManager>();
    }

    private void Update()
    {
        Lud_ButtonCheck();
        if (isclicked_Lud)
        {
            CreatSucho();
        }
    }

    private void OnMouseDown()
    {
        if(gm.money >= SuchoWork.sucho_autoIncreasePrice_Lud)
        {
            isclicked_Lud = true;
            createGray_Lud = Instantiate(graySucho_Lud, transform.position, Quaternion.identity);
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z = 0.0f;
            createGray_Lud.transform.position = mousepos;
        }
    }

    private void OnMouseDrag()
    {
        if (isclicked_Lud == true)
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z = 0.0f;
            createGray_Lud.transform.position = mousepos;
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
                isclicked_Lud = false;
                fXController_ep1.HchoAferFx();

                Instantiate(realSucho_Lud, createGray_Lud.transform.position, Quaternion.identity);
                suchom.suchoPositions_Luds.Add(createGray_Lud.transform.position);
                //Debug.Log("루드위지드 구매");
                gm.money -= SuchoWork.sucho_autoIncreasePrice_Lud;
                suchom.ludCount += 1;
                SuchoWork.sucho_autoMoneyIncreaseAmount += 1;
                SuchoWork.sucho_autoIncreasePrice_Lud += gm.moneyIncreaseLevel * suchom.ludCount * 191;

                createGray_Lud.SetActive(false);
                Destroy(createGray_Lud);
           
            }
            else
            {
                fXController_ep1.EndSFX();
                createGray_Lud.SetActive(false);
                Destroy(createGray_Lud);
               // Debug.Log("범위 밖!!");
            }
        }
    }

    void Lud_ButtonCheck()
    {
        if (gm.money >= SuchoWork.sucho_autoIncreasePrice_Lud)
        {
            buyBttn_Lud.interactable = true;
        }
        else
        {
            buyBttn_Lud.interactable = false;
        }
    }
}
