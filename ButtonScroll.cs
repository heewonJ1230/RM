using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScroll : MonoBehaviour
{
    public RectTransform List;
    public int count;
    private float pos;
    private float movepos;
    private bool isScroll = false;

    void Start()
    {
        pos = List.localPosition.x;
        movepos = List.rect.xMax - List.rect.xMax / count;
    }

    public void Right()
    {
        if(List.rect.xMin +List.rect.xMax/count == movepos)
        {

        }
        else
        {
            isScroll = true;
            movepos = pos - List.rect.width / count;
            pos = movepos;
            StartCoroutine(Scroll());
        }
    }
    public void Left()
    {
        if(List.rect.xMax - List.rect.xMax/count == movepos)
        {

        }
        else
        {
            isScroll = true;
            movepos = pos + List.rect.width / count;
            pos = movepos;
            StartCoroutine(Scroll());
        }
    }

    IEnumerator Scroll()
    {
        while (isScroll)
        {
            List.localPosition = Vector2.Lerp(List.localPosition, new Vector2(movepos, 0), Time.deltaTime * 5);
            if(Vector2.Distance(List.localPosition, new Vector2(movepos, 0)) < 0.1f)
            {
                isScroll = false;
            }
            yield return null;
        }
    }
}
