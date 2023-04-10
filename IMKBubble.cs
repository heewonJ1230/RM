using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IMKBubble : MonoBehaviour
{
    [SerializeField] private GameObject imgbubble_IMK;
    [SerializeField] Text speakText;
    public float destroyTime;
    public Image bbbimg;//없어질때 투명도 조절
    KillUISystem kiUI;
    private void Awake()
    {
        //Debug.Log("나오는거?- 확인");
        UpdateSpeakBbb_IML();
        Destroy(this.gameObject, destroyTime);
    }
    private void Start()
    {
        kiUI = GameObject.Find("KillUISystem").GetComponent<KillUISystem>();
    }

    void Update()
    {
        if (kiUI.hpFill.fillAmount <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void UpdateSpeakBbb_IML()
    {
        StartCoroutine(AppearSpkbb(0.03f));
    }
    IEnumerator AppearSpkbb(float interval)
    {
        string speakbb_txt = IMKSpeakBbbMaker.newSpeakTxt;
        int index = 0;
        speakbb_txt = speakbb_txt.Replace("'", ",");
        while(index <= speakbb_txt.Length)
        {
            speakText.text = speakbb_txt.Substring(0, index);
            yield return new WaitForSeconds(interval);
            index++;
        }
    }
    private void OnDestroy()
    {
        IMKSpeakBbbMaker imkSbbMaker = GameObject.Find("IMKSpeakBbbMaker").GetComponent<IMKSpeakBbbMaker>();
        imkSbbMaker.isSpeaking = false;
    }

}
