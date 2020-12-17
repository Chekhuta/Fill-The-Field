using UnityEngine;
using UnityEngine.UI;

public class ModesWindow : MonoBehaviour
{
    [SerializeField] private ModeButton[] modeButtons;
    [SerializeField] private Button previousModesButton;
    [SerializeField] private Button nextModesButton;
    private const int MIN_PAGE = 0;
    private const int MAX_PAGE = 2;
    private int currentPage = 0;

    public void OpenWindow()
    {
        currentPage = 0;
        UpdateModeButtons();
        ResetSwitchButtons();
        WindowManager.GetInstance().OpenWindow(GetComponent<RectTransform>());
    }

    public void NextModes()
    {
        if (currentPage == MIN_PAGE)
        {
            ChangeButtonAlpha(previousModesButton, true);
        }
        currentPage++;
        UpdateModeButtons();
        if (currentPage == MAX_PAGE)
        {
            ChangeButtonAlpha(nextModesButton, false);
        }
    }

    public void PreviousModes()
    {
        if (currentPage == MAX_PAGE)
        {
            ChangeButtonAlpha(nextModesButton, true);
        }
        currentPage--;
        UpdateModeButtons();
        if (currentPage == MIN_PAGE)
        {
            ChangeButtonAlpha(previousModesButton, false);
        }
    }

    public void UpdateModeButtons()
    {
        for (int i = 0; i < modeButtons.Length; i++)
        {
            modeButtons[i].SetButtonHeader((currentPage + 3) + " x " + (currentPage + 3) + "  -  " + (i + 3) + " figures");
            modeButtons[i].SetCompletedText(DataStorage.CurrentLevel[i + currentPage * 4], DataStorage.MaxLevel[i + currentPage * 4]);
            modeButtons[i].UpdateCompletedLine(DataStorage.CurrentLevel[i + currentPage * 4], DataStorage.MaxLevel[i + currentPage * 4]);
        }
    }

    public void SetMode(int mode)
    {
        int gameMode = currentPage * 4 + mode;
        if (DataStorage.CurrentGameMode == gameMode)
        {
            CloseWindow();
        }
        else
        {
            DataStorage.CurrentGameMode = gameMode;
            DataStorage.FieldSize = new Vector2Int(currentPage + 3, currentPage + 3);
            DataStorage.CountOfFigures = mode + 3;
            Game.GetInstance().ChangeGameMode();
            CloseWindow();
        }
    }

    private void ChangeButtonAlpha(Button button, bool interactable)
    {
        button.interactable = interactable;
        float a = 0;
        if (interactable)
        {
            a = 1;
        }
        else
        {
            a = 0.5f;
        }

        Text arrowText = button.GetComponentInChildren<Text>();
        Color color = arrowText.color;
        color.a = a;
        arrowText.color = color;
        Image buttonImage = button.GetComponent<Image>();
        color = buttonImage.color;
        color.a = a;
        buttonImage.color = color;
    }

    private void CloseWindow()
    {
        WindowManager.GetInstance().CloseWindow(GetComponent<RectTransform>(), false, true);
    }

    private void ResetSwitchButtons()
    {
        ChangeButtonAlpha(previousModesButton, false);
        ChangeButtonAlpha(nextModesButton, true);
    }
}
