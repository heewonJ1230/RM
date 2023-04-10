using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuchoManager : MonoBehaviour
{
    public GameObject suchoBttn_go;
    public GameObject sucho_LockedTxt;

    public Button suchoBttn;
    public Text valTextBuy; //발로스베리아?카드 표시
    public Text ludTextBuy; //룯위지아 카드 표

    public int valCount;
    public int ludCount;

    public List<Vector3> suchoPositions_Vals;
    public List<Vector3> suchoPositions_Luds;

    public GameObject prefabVal;
    public GameObject prefabLud;

    public GameObject suchoGroup;

    CartoonManager cm;

    void Start()
    {
        cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();
        if (cm.doneCartoon[5])
        {
            suchoBttn_go.SetActive(true);
            sucho_LockedTxt.SetActive(false);
        }
        else
        {
            suchoBttn_go.SetActive(false);
            sucho_LockedTxt.SetActive(true);
        }

        if(ludCount > 0 || valCount > 0)
        {
            FillSucho();
        }
        else
        {
            suchoPositions_Vals = new List<Vector3>();
            suchoPositions_Luds = new List<Vector3>();
        }
    }
    void Update()
    {
        if (cm.doneCartoon[5])
        {
            suchoBttn_go.SetActive(true);
            sucho_LockedTxt.SetActive(false);
        }
        else
        {
            suchoBttn_go.SetActive(false);
            sucho_LockedTxt.SetActive(true);
        }
        if (valCount < suchoPositions_Vals.Count)
        {
            for (int i = 0; i < suchoPositions_Vals.Count - valCount; i++)
            {
                suchoPositions_Vals.RemoveAt(i);
            }
        }
        if (ludCount < suchoPositions_Luds.Count)
        {
            for (int i = 0; i < suchoPositions_Luds.Count - ludCount; i++)
            {
                suchoPositions_Luds.RemoveAt(i);
            }
        }
        if (valCount > suchoPositions_Vals.Count)
        {
            valCount = suchoPositions_Vals.Count;
        }
        if(ludCount> suchoPositions_Luds.Count)
        {
            ludCount = suchoPositions_Luds.Count;
        }

        UpdateSuchoAll();

    }
    public void UpdateSuchoAll()
    {
        UpdatePanelBuyVal();
        UpdatePanelBuyLud();
    }

    //수초 수 불러오기 해야하는데
    public void FillSucho()
    {
        GameObject[] vals = GameObject.FindGameObjectsWithTag("Vall");
        if (valCount != vals.Length)
        {
         //   Debug.Log("발ㄹ개수카운트" + valCount +"발스랭스" + vals.Length);
            StartCoroutine(SpwanVals());
        }
        GameObject[] luds = GameObject.FindGameObjectsWithTag("Ludwigia");
        if (ludCount != luds.Length)
        {
            //Debug.Log("루드개수 카운트 " + ludCount + "루드 랭싀" + luds.Length);
            StartCoroutine(SpwanLuds());
        }
    }

    IEnumerator SpwanVals()
    {
        float randTime = (float)Random.Range(0.001f, 0.02f);
        GameObject[] vals = GameObject.FindGameObjectsWithTag("Vall");
       //저장해온 위치값도 불러와야함 여기서!
        for(int i = vals.Length; i < valCount; i++)
        {
            yield return new WaitForSeconds(randTime);
            GameObject obj = Instantiate(prefabVal, suchoPositions_Vals[i], Quaternion.identity);
            obj.transform.parent = suchoGroup.transform;
        }

    }
    IEnumerator SpwanLuds()
    {
        float randTime = (float)Random.Range(0.001f, 0.02f);
        GameObject[] luds = GameObject.FindGameObjectsWithTag("Ludwigia");
        //저장해온 위치값도 불러와야함 여기서!
        for (int i = luds.Length; i < ludCount; i++)
        {
            yield return new WaitForSeconds(randTime);
            GameObject ludstart = Instantiate(prefabLud, suchoPositions_Luds[i], Quaternion.identity);
            ludstart.transform.parent = suchoGroup.transform;
        }

    }


    void UpdatePanelBuyVal()
    {
        valTextBuy.text = "1 뿌리 당 생산 물력 -------" + "  \n";
        valTextBuy.text += "    "+SuchoWork.sucho_autoMoneyIncreaseAmount.ToString("###,###") + " 물력 \n";
        valTextBuy.text += "번식 비용 -------------------" + "  \n";
        valTextBuy.text += SuchoWork.sucho_autoIncreasePrice_Vall.ToString("###,###") + " 물력";
    }
    void UpdatePanelBuyLud()
    {
        ludTextBuy.text = "1 뿌리 당 생산 물력 -------" + "  \n";
        ludTextBuy.text += "    " + SuchoWork.sucho_autoMoneyIncreaseAmount.ToString("###,###") + " 물력 \n";
        ludTextBuy.text += "번식 비용 -------------------" + "  \n";
        ludTextBuy.text += SuchoWork.sucho_autoIncreasePrice_Lud.ToString("###,###") + " 물력";
    }

}
