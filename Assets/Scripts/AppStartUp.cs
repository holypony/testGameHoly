using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using LitJson;
using System;

public class AppStartUp : MonoBehaviour
{
    [SerializeField] private GameSettingsSO GameSettings;
    [SerializeField] private Animator animator;
    private const string jsonUrl = "https://main.d11911cpi0cl2h.amplifyapp.com/UnityBndles/BundleManager.json";
    private string bundleUrl;
    private int TryReConnect = 3;

    private void Start()
    {

        GameSettings.Init();
        GetJson();
    }

    public void Play()
    {
        if (GameSettings.NoInternet)
        {
            if (PlayerPrefs.GetInt("BundleDownloaded", 0) == 0)
            {
                GameSettings.NoInternetPopUp = true;
                return;
            }
        }
        GameSettings.IsLoading = true;
        animator.SetTrigger("Fade");

    }
    public void Loading()
    {

        GameSettings.IsMenu = false;
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        animator.SetTrigger("TurnOn");
    }


    private void GetJson()
    {
        StartCoroutine(DownloadJson());

        IEnumerator DownloadJson()
        {

            WWW www = new WWW(jsonUrl);
            yield return www;

            if (www.error == null)
            {
                Processjson(www.text);
            }
            else
            {
                GameSettings.NoInternet = true;
                TryReConnect--;
                if (TryReConnect > 0) StartCoroutine(DownloadJson());
                yield return new WaitForSecondsRealtime(0.25f);

            }
        }
    }

    private void Processjson(string jsonString)
    {
        JsonData jsonvale = JsonMapper.ToObject(jsonString);

        GameSettings.ActualVersion = jsonvale["ActualVersion"].ToString();
        GameSettings.BundleUrl = jsonvale["BundleLink"].ToString();
        Debug.Log(GameSettings.BundleUrl);
    }
}
