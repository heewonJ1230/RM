using UnityEngine.UI;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public Slider bgmVolume;
   public Slider asmrVolume;
   public Slider sfxVolume;

    public AudioSource bgm; public AudioSource asmr; public AudioSource sfx;
    private float bgmVol = 0.5f;
    private float asmrVol= 0.5f;
    private float sfxVol = 0.5f;

    CommonDataController cdc;
    CartoonManager cm;
    GameManager gm;

    void Start()
    {
        cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        asmrVol = PlayerPrefs.GetFloat("asmrVol", 0.5f);
        asmrVolume.value = asmrVol;
        asmr.volume = asmrVolume.value;

        sfxVol = PlayerPrefs.GetFloat("sfxVol", 0.5f);
        sfxVolume.value = sfxVol;
        sfx.volume = sfxVolume.value;


        if (gm.isKilled ||cdc.marimoIsSick || cm.doneCartoon[8]&& !cm.doneCartoon[9])
        {
            bgm.volume = 0;
        }
        else
        {
            bgmVol = PlayerPrefs.GetFloat("bgmVol", 0.5f);
            bgmVolume.value = bgmVol;
            bgm.volume = bgmVolume.value;
        }
    }
    void Update()
    {   
        BGMSlider();
        ASMRSlider();
        SFXSlider();
    }
    public void BGMSlider()
    {
        if (gm.isKilled || cdc.marimoIsSick || cm.doneCartoon[8] && !cm.doneCartoon[9])
        {
            bgm.volume = 0;
        }
        else
        {
            bgm.volume = bgmVolume.value;
            bgmVol = bgmVolume.value;
        }
        //Debug.Log("지금씨디시마리모킬은? " + cdc.marimoIsSick +"볼룸은 ?  "+bgm.volume);

        PlayerPrefs.SetFloat("bgmVol", bgmVol);
    }
    public void ASMRSlider()
    {
        asmr.volume = asmrVolume.value;
        asmrVol = asmrVolume.value;
        
        PlayerPrefs.SetFloat("asmrVol", asmrVol);
    }
    public void SFXSlider()
    {
        sfx.volume = sfxVolume.value;
        sfxVol = sfxVolume.value;

        PlayerPrefs.SetFloat("sfxVol", sfxVol);
    }
}
