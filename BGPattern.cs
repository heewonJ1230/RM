using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGPattern : MonoBehaviour
{
    public Image bgIcon;
    public string path1, path2;
    float patternSpeed = 1.0f;
    void Start()
    {
       InvokeRepeating("ImageChangePath", 0.5f, 1);
       InvokeRepeating("Imagemove", 1.0f, 1);

    }
    void Update()
    {
        Vector3 nextPos = transform.position;
        nextPos.x -= patternSpeed * Time.deltaTime;
   
        transform.position = nextPos;
    }
    void Imagemove()
    {
        Vector3 nextPos = transform.position;
        nextPos.x += 1;
        transform.position = nextPos;
    }
    void ImageChange(Sprite sprite)
    {
        bgIcon.sprite = sprite;
    }
    void ImageChangePath()
    {
        if (bgIcon.sprite == Resources.Load<Sprite>(path1))
        ImageChange(Resources.Load<Sprite>(path2));
        else if (bgIcon.sprite == Resources.Load<Sprite>(path2))
            ImageChange(Resources.Load<Sprite>(path1));
    }
}
