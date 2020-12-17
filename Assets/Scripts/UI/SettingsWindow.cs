using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : MonoBehaviour
{
    [SerializeField] private Image soundIcon;
    [SerializeField] private Sprite[] soundSprites;

    public void OpenWindow()
    {
        UpdateSoundButton();
        WindowManager.GetInstance().OpenWindow(GetComponent<RectTransform>());
    }

    public void CloseWindow()
    {
        WindowManager.GetInstance().CloseWindow(GetComponent<RectTransform>(), false, true);
    }

    public void SwitchSound()
    {
        DataStorage.SwitchSound();
        UpdateSoundButton();
    }

    private void UpdateSoundButton()
    {
        if (DataStorage.IsSoundOn)
        {
            soundIcon.sprite = soundSprites[0];
        }
        else
        {
            soundIcon.sprite = soundSprites[1];
        }
    }

    public void RateGame()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.frozenparrot.fillthefield");
    }

    public void OpenPrivacyPolicy()
    {
        //Application.OpenURL("-");
    }

    public void OpenMoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Frozen+Parrot");
    }
}
