using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartoonGuide : MonoBehaviour
{
    [SerializeField]  GameObject thisBttn;
    public float pressCount;
    public float maxCount;
    public GameObject guidshow;
    public bool ismouseDown;

    void Start()
    {
        pressCount = 0;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ismouseDown = true; 
        }
        else if (Input.GetMouseButtonUp(0) && ismouseDown)
        {
            ismouseDown = false;
            pressCount = 0;
            guidshow.SetActive(false);
        }
        else if (ismouseDown)
        {
            pressCount += Time.smoothDeltaTime;
            if (pressCount >= maxCount)
            {
                guidshow.SetActive(true);
            }
        }
    }
   
}
