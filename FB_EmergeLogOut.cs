using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FB_EmergeLogOut : MonoBehaviour
{
    public Text elo_mssg;
    public GameObject elo_go;

    public void Start()
    {
        Eml_ok();
    }
    public void EmergeLogout(string email)
    {
        elo_mssg.text = email + "로 메일 인증 링크가 발송되었습니다."
            + "\n 인증링크를 클릭해 주세요"
            + "\n <size=35> 구글에서 제공하는 연동 서비스이므로 안심하세요! 그치만... 스팸메일로 처리될 수 있습니다 ㅠㅠ </size>";
        elo_go.SetActive(true);
    }

    public void Eml_ok()//초기화
    {
        elo_go.SetActive(false);
        elo_mssg.text = "";

    }
}
