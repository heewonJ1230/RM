using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarimoWork : MonoBehaviour
{
    private Animator anim;
    GameManager gm;
    CartoonManager cm;
 
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();
        anim = GetComponent<Animator>();

        CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        if (cm.doneCartoon[3] && !cm.doneCartoon[4])
        {
            gameObject.SetActive(false);
        }
        else if (cm.doneCartoon[4] && cdc.marimoIsSick)
        {
            gameObject.SetActive(true);
            gameObject.GetComponent<Animator>().Play("Sick");
        }
        else
        {
            gameObject.SetActive(true);
            gameObject.GetComponent<Animator>().Play("marimo_Idle");

        }
         if (cm.doneCartoon[8] && !cm.doneCartoon[9])
        {
            anim.SetTrigger("marimoAngry");
        }
    
        if (cm.doneCartoon[9])
        {
            gameObject.GetComponent<Animator>().Play("marimo_Idle");
            if (gm.isKilled)
            {
                gameObject.SetActive(false);
            }
            if (cdc.isTurnBack)
            {
                gameObject.SetActive(true);
                gameObject.GetComponent<Animator>().Play("marimo_Idle");
            }
        }

        if (gm.isKilled)
        {
            gameObject.SetActive(false);
        }

    }
    void Update()
    {
       // bool nomoreSick = false;

        CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (Input.GetMouseButtonDown(0))
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
            {
                if (gm.isKilled)
                {
                    ;
                }
                else if (cm.doneCartoon[8] && !cm.doneCartoon[9])
                {
                    anim.SetTrigger("marimoAngry");
                }
                else if (cm.doneCartoon[1])
                {
                    if (cdc.marimoIsSick)
                    {
                        gameObject.GetComponent<Animator>().Play("Inrefr");
                        //nomoreSick = true;
                    }
                    else
                    {
                        anim.SetTrigger("marimoClick");
                    }
                }
    
            }

        }
       if (cm.doneCartoon[9])
        {
            if (gm.isKilled)
            {
                gameObject.SetActive(false);
            }
            if (cdc.isTurnBack)
            {
                gameObject.SetActive(true);
            }
        }
    }

}
