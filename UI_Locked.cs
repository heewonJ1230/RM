using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Locked : MonoBehaviour
{
    public Button bettabuyBttn;
    public Text bettabttnText;
    public int bettalev;

    GameManager gm;


    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        LockORUnLock();
    }
    private void Update()
    {
        LockORUnLock();
    }

    void LockORUnLock()
    {
        if (gm.moneyIncreaseLevel >= bettalev)
        {
            bettabuyBttn.interactable = true;
            bettabttnText.text = "구매!";

        }
        else
        {
            bettabuyBttn.interactable = false;
            bettabttnText.text = bettalev + "렙 도달시 구매가능";
        }
    }
}
