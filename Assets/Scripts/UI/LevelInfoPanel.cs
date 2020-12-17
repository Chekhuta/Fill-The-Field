using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelInfoPanel : MonoBehaviour
{
    [SerializeField] private Text levelHeaderText;
    [SerializeField] private Text levelValueText;
    [SerializeField] private RectTransform hintButton;
    [SerializeField] private RectTransform optionsButton;
    [SerializeField] private RectTransform modesButton;
    [SerializeField] private RectTransform restartButton;
    [SerializeField] private RectTransform levelText;
    [SerializeField] private RectTransform moreHintsButton;

    public void UpdateLevelValue(int level)
    {
        if (level > DataStorage.GetCurrentMaxLevel())
        {
            levelValueText.text = "Random";
        }
        else
        {
            levelValueText.text = level + "";
        }
    }

    public void SetLevelHeaderText(string header)
    {
        levelHeaderText.text = header;
    }

    public void ShowPanelButtons()
    {
        GetComponent<Hint>().UpdateButtons();
        StartCoroutine(ShowPanelElement(levelText, 0));
        StartCoroutine(ShowPanelElement(optionsButton, 0.5f));
        StartCoroutine(ShowPanelElement(restartButton, 0.5f));
        StartCoroutine(ShowPanelElement(modesButton, 0.5f));
        if (DataStorage.HintsCount != 0)
        {
            StartCoroutine(ShowPanelElement(hintButton, 0.5f));
        }
        else
        {
            moreHintsButton.gameObject.SetActive(true);
            StartCoroutine(ShowPanelElement(moreHintsButton, 0.5f));
        }
    }

    public void SetPanelEnabled(bool value)
    {
        if (DataStorage.HintsCount != 0)
        {
            hintButton.GetComponent<Button>().enabled = value;
        }
        else
        {
            moreHintsButton.GetComponent<Button>().enabled = value;
        }

        optionsButton.GetComponent<Button>().enabled = value;
        modesButton.GetComponent<Button>().enabled = value;
        restartButton.GetComponent<Button>().enabled = value;
    }

    public void SetButtonsInteractable(bool value)
    {
        optionsButton.GetComponent<Button>().interactable = value;
        modesButton.GetComponent<Button>().interactable = value;
        restartButton.GetComponent<Button>().interactable = value;
    }

    public void ShowTutorialButtons()
    {
        StartCoroutine(ShowPanelElement(levelText, 0));
        StartCoroutine(ShowPanelElement(restartButton, 0.5f));
    }

    public void ShowRestOfButtons()
    {
        GetComponent<Hint>().UpdateButtons();
        StartCoroutine(ShowPanelElement(optionsButton, 0.5f));
        StartCoroutine(ShowPanelElement(modesButton, 0.5f));
        if (DataStorage.HintsCount != 0)
        {
            StartCoroutine(ShowPanelElement(hintButton, 0.5f));
        }
        else
        {
            moreHintsButton.gameObject.SetActive(true);
            StartCoroutine(ShowPanelElement(moreHintsButton, 0.5f));
        }
    }

    public void ShowHintButtonAfterHint()
    {
        hintButton.localScale = new Vector3(0, 0, 1);
        hintButton.gameObject.SetActive(true);
        StartCoroutine(ShowPanelElement(hintButton, 1));
    }

    private IEnumerator ShowPanelElement(RectTransform rect, float delay)
    {
        yield return new WaitForSeconds(delay);
        for (int t = 1; t < 6; t++)
        {
            float elementScale = t * 0.2f;
            rect.localScale = new Vector3(elementScale, elementScale, 1);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
