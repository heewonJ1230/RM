using UnityEngine.UI;
using UnityEngine;

public class SoundMan2 : MonoBehaviour
{
  // public Slider bgmVolume;
  // public Slider asmrVolume;
  // public Slider sfxVolume;

    public AudioSource bgm; public AudioSource asmr; public AudioSource sfx;
    private float bgmVol = 0.5f;
    private float asmrVol= 0.5f;
    private float sfxVol = 0.5f;

    void Start()
    {
        asmrVol = PlayerPrefs.GetFloat("asmrVol", 0.5f);
        asmr.volume = asmrVol;

        sfxVol = PlayerPrefs.GetFloat("sfxVol", 0.5f);
        sfx.volume = sfxVol;

        bgmVol = PlayerPrefs.GetFloat("bgmVol", 0.5f);

        bgm.volume = bgmVol;
    }
    void Update()
    {
        
        BGMSlider();
        ASMRSlider();
        SFXSlider();
    }
    public void BGMSlider()
    {
       bgm.volume = bgmVol;
        PlayerPrefs.SetFloat("bgm", asmrVol);
    }
    public void ASMRSlider()
    {
        asmr.volume = asmrVol;
        PlayerPrefs.SetFloat("asmrVol", asmrVol);
    }
    public void SFXSlider()
    {
        sfx.volume = sfxVol;

        PlayerPrefs.SetFloat("sfxVol", sfxVol);
    }
}
