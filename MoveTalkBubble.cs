using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTalkBubble : MonoBehaviour
{
    public float patternSpeed ;
    public float movetime ;
    public float repeattime ;
    //public GameObject parent;

    Vector2 originPosition;

    void Start()
    {
        originPosition = transform.localPosition;
        //transform.parent = parent.transform; //--ㅋㅏ메라 따라가려하는데 힘드네.

        InvokeRepeating("Move4", repeattime, movetime);
        
    }

    void Move4()
    {
        MoveXLeft();
        Invoke("MoveYUp", 0.25f);
        Invoke("MoveXRight", 0.5f);
        Invoke("MoveYDown", 0.75f);
        Invoke("MoveReset", 2.00f);
    }
    void MoveXLeft()
    {
        Vector2 movebubble = transform.localPosition;
        movebubble.x -= patternSpeed * Time.deltaTime;

        transform.localPosition = movebubble;
        //Debug.Log(transform.localPosition);
    }
    void MoveXRight()
    {
        Vector2 movebubble = transform.localPosition;
        movebubble.x += patternSpeed * Time.deltaTime;

        transform.localPosition = movebubble;
        //Debug.Log(transform.localPosition);
    }
    void MoveYUp()
    {
        Vector2 movebubble = transform.localPosition;
        movebubble.y += patternSpeed * Time.deltaTime/2;

        transform.localPosition = movebubble;
        //Debug.Log(transform.localPosition);
    }
    void MoveYDown()
    {
        Vector2 movebubble = transform.localPosition;
        movebubble.y -= patternSpeed * Time.deltaTime/2;

        transform.localPosition = movebubble;
        //Debug.Log(transform.localPosition);
    }
    void MoveReset()
    {
        //Vector3 movebubble = transform.localPosition;
     
        transform.localPosition = originPosition;
        //Debug.Log(transform.localPosition);
    }
}
