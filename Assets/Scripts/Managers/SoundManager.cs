using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private GameSettingsSO GameSettings;

    [Header("Sources")]
    [SerializeField] private AudioSource AsBg;
    [SerializeField] private AudioSource AsUiTap;

    [Header("Clips")]
    [SerializeField] private AudioClip ClipBgMenu;
    [SerializeField] private AudioClip ClipBgGame;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        Destroy(this.gameObject);
        AsBg.clip = ClipBgGame;
    }

    public void TapSound()
    {
        AsUiTap.Play();
    }

    public void ChangeBgMusic(bool isMenu)
    {
        if (isMenu)
        {
            AsBg.clip = ClipBgMenu;
        }
        else
        {
            AsBg.clip = ClipBgGame;
        }
        AsBg.Play();
    }

    public void MusicSwitcher(bool isMusic)
    {
        AsBg.mute = !isMusic;
    }

    public void SfxSwitcher(bool isSFX)
    {
        AsUiTap.mute = !isSFX;
    }

    private void OnEnable()
    {
        GameSettings.OnIsSFX += SfxSwitcher;
        GameSettings.OnIsMusic += MusicSwitcher;
    }

    private void OnDisable()
    {
        GameSettings.OnIsSFX -= SfxSwitcher;
        GameSettings.OnIsMusic -= MusicSwitcher;
    }
}
