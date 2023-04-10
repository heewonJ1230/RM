using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyMove : MonoBehaviour
{
    public Vector2 point;
    private GameObject pointGo;

    public Text txt;

    void Start()
    {
        txt = transform.GetComponentInChildren <Text>();
        SaiyanMarimo sm = GameObject.Find("Icon_SaiyanMC").GetComponent<SaiyanMarimo>();
        pointGo = GameObject.Find("Icon_money");
        point = new Vector2(pointGo.transform.position.x -0.65f,pointGo.transform.position.y+1.5f);
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (sm.isSaiyanTime)
        {
            txt.text = "+" + (gm.moneyIncreaseAmount * 3).ToString("###,###");
        }
        else
        {
            txt.text = "+" + gm.moneyIncreaseAmount.ToString("###,###");
        }
        

        Destroy(this.gameObject, 3.0f);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, point, Time.deltaTime * 5f);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color. a - 0.015f);
       

        txt = transform.GetComponentInChildren<Text>();
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a - 0.012f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(point, 0.2f);
    }
}
