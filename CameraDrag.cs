using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraDrag : MonoBehaviour
{
    private Transform tr;
    private Vector2 firstTouch;
    public float limitMinY;
    public float limitMaxY;
    public float dragSpeed;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()== false && SuchoBttnClick_Lud.isclicked_Lud == false && SuchoBttnClick_Vall.isclicked_Vall == false)
        {
            Move();
        }
    }
    void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 currentTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector2 currentTouch = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if (Vector2.Distance(firstTouch, currentTouch) > 1.0f) 
            {
                if (firstTouch.y < currentTouch.y) 
                {
                    if (tr.position.y > limitMinY)
                        tr.Translate(Vector2.down * dragSpeed);
                }
                else if(firstTouch.y > currentTouch.y)
                {
                    if (tr.position.y < limitMaxY) 
                        tr.Translate(Vector2.up * dragSpeed); 
                }
            }
        }
    }
}
