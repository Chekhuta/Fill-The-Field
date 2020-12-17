using UnityEngine;
using UnityEngine.UI;

public class ModeButton : MonoBehaviour
{
    public Text buttonHeader;
    public Text completedText;
    public Image completedLine;

    public void SetButtonHeader(string text)
    {
        buttonHeader.text = text;
    }

    public void SetCompletedText(int currentLevel, int maxLevel)
    {
        string levelText = "";
        if (currentLevel > maxLevel)
        {
            levelText = "Completed: " + maxLevel;
        }
        else
        {
            levelText = "Level: " + currentLevel + " / " + maxLevel;
        }
        completedText.text = levelText;
    }

    public void UpdateCompletedLine(int currentLevel, int maxLevel)
    {
        float width = (float)currentLevel / maxLevel;
        if (width < 0.01f)
        {
            width = 0.01f;
        }
        completedLine.fillAmount = width;
    }
}
