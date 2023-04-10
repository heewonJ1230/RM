using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuchoWork : MonoBehaviour
{
    public SpriteRenderer suchoSp;
    public Sprite sprite1, sprite2;
    private bool imageChange;
    public float ranTimeMin, ranTimeMax;
    private float repeatFloat;

    public static long sucho_autoMoneyIncreaseAmount = 1;
    public static long sucho_autoIncreasePrice_Vall = 191;
    public static long sucho_autoIncreasePrice_Lud = 191;
    GameManager gm;

 
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        repeatFloat = Random.Range(ranTimeMin, ranTimeMax);
        InvokeRepeating("ImageChangeSuchoPath", 1.0f, repeatFloat);
        StartCoroutine(WorkSucho());
    }

    void ImageChangeSucho(Sprite sprite)
    {
        suchoSp.sprite = sprite;
    }
    void ImageChangeSuchoPath()
    {
        if (imageChange)
        {
            ImageChangeSucho(sprite2);
            imageChange = false;
        }
        else
        {
            ImageChangeSucho(sprite1);
            imageChange = true;
        }
    }
    IEnumerator WorkSucho()
    {
        while (true)
        {
            CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
            if (!cdc.marimoIsSick /*&& !gm.isKilled*/)
            {
                gm.money += sucho_autoMoneyIncreaseAmount;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
