using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeBackground : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    public IEnumerator ShowFadeBackground()
    {
        SetActiveFadeBackground(true);
        for (int t = 1; t < 9; t++)
        {
            SetFadeColor(t * 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
    }
    
    public IEnumerator HideFadeBackground()
    {
        SetActiveFadeBackground(true);
        for (int t = 9; t >= 0; t--)
        {
            SetFadeColor(t * 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
        SetActiveFadeBackground(false);
    }

    public void SetActiveFadeBackground(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetAlphaZero()
    {
        SetFadeColor(0);
    }

    private void SetFadeColor(float alpha)
    {
        Color fadeColor = fadeImage.color;
        fadeColor.a = alpha;
        fadeImage.color = fadeColor;
    }
}
