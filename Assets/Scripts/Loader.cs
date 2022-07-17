using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [SerializeField] private GameSettingsSO GameSettings;


    void Awake()
    {
        StartCoroutine(DownloadAndCache());
    }

    private IEnumerator DownloadAndCache()
    {
        while (!Caching.ready)
            yield return null;

        using (WWW www = WWW.LoadFromCacheOrDownload(GameSettings.BundleUrl, 0))
        {
            yield return www;
            if (www.error != null) throw new Exception("WWW download had an error:" + www.error);

            AssetBundle bundle = www.assetBundle;

            GameObject spawner = GameObject.Find("CoinSpawner(Clone)");
            if (spawner == null)
            {

                Instantiate(bundle.LoadAsset("Scene2"), transform.position, Quaternion.identity, gameObject.transform);

            }

            PlayerPrefs.SetInt("BundleDownloaded", 1);
            GameSettings.IsLoading = false;
            SoundManager.instance.ChangeBgMusic(false);
            bundle.Unload(false);

            GameObject menuBtn = GameObject.Find("ButtonMenu");
            menuBtn.GetComponent<Button>().onClick.AddListener(MenuBtn);
        }
    }

    public void MenuBtn()
    {
        GameSettings.IsLoading = true;
        SceneManager.UnloadSceneAsync(1, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        SoundManager.instance.ChangeBgMusic(true);
        GameSettings.IsMenu = true;
        GameSettings.IsLoading = false;
    }
}
