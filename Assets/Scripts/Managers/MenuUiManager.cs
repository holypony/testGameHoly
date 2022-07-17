using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuUiManager : MonoBehaviour
{
    [SerializeField] private GameSettingsSO GameSettings;
    [SerializeField] private GameObject noInternetLabel;
    [Header("Panels")]
    [SerializeField] private RectTransform PanelMenu;
    [SerializeField] private RectTransform PanelSetting;
    [SerializeField] private RectTransform PanelBlockUi;
    [SerializeField] private RectTransform PanelNoInternet;
    [SerializeField] private RectTransform PanelLoadingScreen;
    [Header("Menu Buttons")]
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button SettingButton;
    private bool settingIsOpen = false;
    private int slideMove = 30;
    private bool isMenu = true;

    WaitForSeconds wait = new WaitForSeconds(0.01f);

    public void SettingSwitcher()
    {

        if (settingIsOpen)
        {
            slideMove = 30;
            settingIsOpen = !settingIsOpen;
        }
        else
        {
            settingIsOpen = !settingIsOpen;
            slideMove = -30;
        }
        StartCoroutine(SettingOpenRoutine());

        IEnumerator SettingOpenRoutine()
        {
            float menuX = PanelMenu.localPosition.x;
            float settingsX = PanelSetting.localPosition.x;

            Vector3 newPosMenu;
            Vector3 newPosSettings;

            PanelBlockUi.gameObject.SetActive(true);
            int i = 0;
            while (i < 99)
            {
                i++;
                menuX += slideMove;
                settingsX += slideMove;

                PanelMenu.localPosition = new Vector3(menuX, 0f, 0f);
                PanelSetting.localPosition = new Vector3(settingsX, 0f, 0f);

                yield return wait;
            }
            PanelBlockUi.gameObject.SetActive(false);
        }
    }

    private void NoInternetPopUp(bool noInternet)
    {
        PanelNoInternet.gameObject.SetActive(noInternet);

    }

    private void NoInternetLabel(bool noInternet)
    {
        noInternetLabel.SetActive(noInternet);
    }

    private void MenuSwithcer(bool isMenu)
    {
        PanelMenu.gameObject.SetActive(isMenu);
    }
    private void LoadingScreen(bool isLoading)
    {
        PanelLoadingScreen.gameObject.SetActive(isLoading);
    }

    void OnEnable()
    {
        GameSettings.OnNoInternet += NoInternetLabel;
        GameSettings.OnInternetPopUp += NoInternetPopUp;
        GameSettings.OnIsLoading += LoadingScreen;
        GameSettings.OnIsMenu += MenuSwithcer;
    }

    void OnDisable()
    {
        GameSettings.OnNoInternet -= NoInternetLabel;
        GameSettings.OnIsLoading -= LoadingScreen;
        GameSettings.OnIsMenu -= MenuSwithcer;
        GameSettings.OnInternetPopUp -= NoInternetPopUp;
    }
}
