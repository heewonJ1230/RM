using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMK_missControl : MonoBehaviour
{
    CutSystem cs;

    private void Awake()
    {
        cs = GameObject.Find("CutSystem").GetComponent<CutSystem>();
    }

    private void OnMouseDown()
    {
        cs.MissShowup(); //제대로 하려면 마우클리으로 해야겠다인풋 마우스클릭말이야.
    }
}
