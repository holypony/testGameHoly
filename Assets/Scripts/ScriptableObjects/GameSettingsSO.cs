using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Scriptables/GameSettings")]

public class GameSettingsSO : ScriptableObject
{
    [SerializeField] private bool isMusic;
    [SerializeField] private bool isSFX;
    [SerializeField] private bool noInternet;
    [SerializeField] private bool isLoading;
    [SerializeField] private bool isMenu;
    [SerializeField] private bool noInternetPopUp;
    public string BundleUrl;
    public string ActualVersion;


    public void Init()
    {
        IsMusic = true;
        IsSFX = true;
        IsLoading = false;
        NoInternet = false;
        isMenu = true;
        noInternetPopUp = false;
    }
    public bool IsMusic
    {
        get => isMusic;
        set
        {
            isMusic = value;
            OnIsMusic?.Invoke(isMusic);
        }
    }

    public bool NoInternetPopUp
    {
        get => noInternetPopUp;
        set
        {
            noInternetPopUp = value;
            OnInternetPopUp?.Invoke(noInternetPopUp);
        }
    }

    public bool IsSFX
    {
        get => isSFX;
        set
        {
            isSFX = value;
            OnIsSFX?.Invoke(isSFX);
        }
    }
    public bool NoInternet
    {
        get => noInternet;
        set
        {
            noInternet = value;
            OnNoInternet?.Invoke(noInternet);
        }
    }

    public bool IsLoading
    {
        get => isLoading;
        set
        {
            isLoading = value;
            OnIsLoading?.Invoke(isLoading);
        }
    }

    public bool IsMenu
    {
        get => isMenu;
        set
        {
            isMenu = value;
            OnIsMenu?.Invoke(isMenu);
        }
    }

    public event Action<bool> OnIsMusic;
    public event Action<bool> OnIsSFX;
    public event Action<bool> OnNoInternet;
    public event Action<bool> OnIsLoading;
    public event Action<bool> OnIsMenu;
    public event Action<bool> OnInternetPopUp;

}