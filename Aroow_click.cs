using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aroow_click : MonoBehaviour
{
    public GameObject topArrw;
    public GameObject downArrw;
    public GameObject scollob;
    ScrollRect sr;
    float srP;

    void Start()
    {
        sr = scollob.GetComponent<ScrollRect>();
        sr.verticalNormalizedPosition = 1;
        srP = sr.verticalNormalizedPosition;
        topArrw.SetActive(false);
        downArrw.SetActive(true);
    }
    void Update()
    {
        srP = sr.verticalNormalizedPosition;

        SrDefault();
    }
    void SrDefault()
    {
        if (srP >= 1)
        {
            topArrw.SetActive(false);
            downArrw.SetActive(true);
        }
        else if (srP <= 1 && srP >= 0)
        {
            topArrw.SetActive(true);
            downArrw.SetActive(true);
        }
        else
        {
            topArrw.SetActive(true);
            downArrw.SetActive(false);
        }
    }

    public void GoTop()
    {
        topArrw.SetActive(false);
        downArrw.SetActive(true);
        sr.verticalNormalizedPosition = 1;
    }
    public void GoDown()
    {
        topArrw.SetActive(true);
        downArrw.SetActive(false);
        sr.verticalNormalizedPosition = 0;
    }
}
