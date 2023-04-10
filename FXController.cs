using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXController : MonoBehaviour
{
    private AudioSource FXaudioSource;
    [SerializeField] private AudioClip bttn_waterDrops_Start;
    [SerializeField] private AudioClip bttn_WaterDrops;
    [SerializeField] private AudioClip pageTurn;
    [SerializeField] private AudioClip saiyanMarimoClick;

    [SerializeField] private AudioClip closeSFX;
    [SerializeField] private AudioClip endSFX;
    [SerializeField] private AudioClip saiyanReady;
    [SerializeField] private AudioClip openExtraSFX;
    [SerializeField] private AudioClip savedSccssFX;
    [SerializeField] private AudioClip tab;
    [SerializeField] private AudioClip close_Dir_Y;
    [SerializeField] private AudioClip nextCard;
    [SerializeField] private AudioClip hchoSuccess;
    [SerializeField] private AudioClip hchoafterfx;

    void OnEnable()
    {
        FXaudioSource = GetComponent<AudioSource>();
    }
    public void HchoAferFx()
    {
        FXaudioSource.clip = hchoafterfx;
        FXaudioSource.Play();
    }
    public void HchoSuccessFX()
    {
        FXaudioSource.clip = hchoSuccess;
        FXaudioSource.Play();
    }
    public void Tab()
    {
        FXaudioSource.clip = tab;
        FXaudioSource.Play();
    }
    public void CloseDirY()
    {
        FXaudioSource.clip = close_Dir_Y;
        FXaudioSource.Play();
    }
    public void NextCard()
    {
        FXaudioSource.clip = nextCard;
        FXaudioSource.Play();
    }
    public void PageTurnFX()
    {
        FXaudioSource.clip = pageTurn;
        FXaudioSource.Play();
    }

    public void InvokeWaterDrops_Start()//스플래쉬에 쓰임 건들면 안됨
    {
        Invoke("WaterDrops_Start", 0.04f);
    }
    public void WaterDrops_Start()
    {
        FXaudioSource.clip = bttn_waterDrops_Start;
        FXaudioSource.Play();
    }
    public void InvokeWaterDrops()//스플래쉬에 쓰임 건들면 안됨
    {
        Invoke("WaterDrops", 1.8f);
    }
    public void WaterDrops()
    {
        FXaudioSource.clip = bttn_WaterDrops;
        FXaudioSource.Play();
    }
    public void SaiyanMarimoClickFX()
    {
        FXaudioSource.clip = saiyanMarimoClick;
        FXaudioSource.Play();
    }
    public void CloseSFX ()
    {
        FXaudioSource.clip = closeSFX;
        FXaudioSource.Play();
    }
    public void EndSFX()
    {
        FXaudioSource.clip = endSFX;
        FXaudioSource.Play();
    }
    public void SaiyanReadySFX()
    {
        FXaudioSource.clip = saiyanReady;
        FXaudioSource.Play();
    }
    public void OpenExtraSFX()
    {
        FXaudioSource.clip = openExtraSFX;
        FXaudioSource.Play();
    }
    public void SavedSccssFX()
    {
        FXaudioSource.clip = savedSccssFX;
        FXaudioSource.Play();
    }
}