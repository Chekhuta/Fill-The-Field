using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    [SerializeField] private GameObject hintButton;
    [SerializeField] private GameObject moreHintsButton;
    [SerializeField] private GameObject closeHintButton;
    [SerializeField] private Text hintCountText;
    private static Hint instance;

    public static Hint GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        instance = this;
    }

    public void AddHints()
    {
        DataStorage.UpdateHintCount(10);
        UpdateHintsCountText();
        moreHintsButton.SetActive(false);
        hintButton.SetActive(true);
    }

    public void UpdateButtons()
    {
        UpdateHintsCountText();
        if (DataStorage.HintsCount != 0)
        {
            moreHintsButton.GetComponent<RectTransform>().localScale = Vector3.one;
        }
        else
        {
            hintButton.gameObject.SetActive(false);
            hintButton.GetComponent<RectTransform>().localScale = Vector3.one;
            moreHintsButton.gameObject.SetActive(true);
        }
    }

    public void UpdateHintsCountText()
    {
        hintCountText.text = DataStorage.HintsCount + "";
    }

    public void UseHint()
    {
        if (!FigureSpawner.GetInstance().IsAvailableFigures())
        {
            return;
        }
        GetComponent<LevelInfoPanel>().SetButtonsInteractable(false);
        hintButton.SetActive(false);
        closeHintButton.SetActive(true);
        FigureSpawner.GetInstance().SetEnabledHintMode(true);
    }

    public void CloseHint()
    {
        GetComponent<LevelInfoPanel>().SetButtonsInteractable(true);
        hintButton.SetActive(true);
        closeHintButton.SetActive(false);
        FigureSpawner.GetInstance().SetEnabledHintMode(false);
    }

    public void HintUsedOnFigure(int figureIndex)
    {
        FigureSpawner.GetInstance().SetHintUsed(figureIndex);
        FigureSpawner.GetInstance().SetEnabledHintMode(false, 0.7f);
        DataStorage.UseHint();
        if (DataStorage.HintsCount == 0)
        {
            closeHintButton.SetActive(false);
            moreHintsButton.SetActive(true);
        }
        else
        {
            UpdateHintsCountText();
            GetComponent<LevelInfoPanel>().ShowHintButtonAfterHint();
            closeHintButton.SetActive(false);
        }
        GetComponent<LevelInfoPanel>().SetButtonsInteractable(true);
    }

    public void GetMoreHints()
    {
        #if UNITY_EDITOR
            AddHints();
        #endif

        #if UNITY_ANDROID
            AdManager.GetInstance().DisplayRewardVideo();
        #endif
    }
}
