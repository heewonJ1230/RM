using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMK_FXController : MonoBehaviour
{
    private AudioSource IMK_FXaudioSource;
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private AudioClip[] cutSounds;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip forgiveSound;
    [SerializeField] private AudioClip missSound;
    void Awake()
    {
        IMK_FXaudioSource = GetComponent<AudioSource>();
    }
    public void HitFX()
    {
        IMK_FXaudioSource.clip = hitSounds[Random.Range(0, hitSounds.Length)];
        IMK_FXaudioSource.Play();
    }
    public void CutFX()
    {
        IMK_FXaudioSource.clip = cutSounds[Random.Range(0, hitSounds.Length)];
        IMK_FXaudioSource.Play();
    }

    public void DeathFX()
    {
        IMK_FXaudioSource.clip = deathSound;
        IMK_FXaudioSource.Play();
    }

    public void ForgiveBttnFX()
    {
        IMK_FXaudioSource.clip = forgiveSound;
        IMK_FXaudioSource.Play();
    }

    public void MissFX()
    {
        IMK_FXaudioSource.clip = missSound;
        IMK_FXaudioSource.Play();
    }

    public void InvokeWaterDrops_Start()//스플래쉬에 쓰임 건들면 안됨
    {
        Invoke("WaterDrops_Start", 0.04f);
    }

}