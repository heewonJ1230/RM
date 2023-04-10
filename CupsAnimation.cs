using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CupsAnimation : MonoBehaviour
{
    public Image cupImg;
    public string path0, path1;
    void Start()
    {
        
        InvokeRepeating("ImageChangePath", 0.5f, 0.5f);
    }

    void Update()
    {
        
    }
    void ImageChange(Sprite sprite)
    {
        cupImg.sprite = sprite;
    }
    void ImageChangePath()
    {
        if (cupImg.sprite == Resources.Load<Sprite>(path0))
            ImageChange(Resources.Load<Sprite>(path1));
        else if (cupImg.sprite == Resources.Load<Sprite>(path1))
            ImageChange(Resources.Load<Sprite>(path0));
    }
}
