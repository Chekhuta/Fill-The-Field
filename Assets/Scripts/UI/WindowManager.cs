using System.Collections;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    [SerializeField] private FadeBackground fadeBackground;
    private static WindowManager instance;

    private void Awake()
    {
        instance = this;
    }

    public static WindowManager GetInstance()
    {
        return instance;
    }

    public void OpenWindow(RectTransform windowRect)
    {
        StartCoroutine(OpenWindowAnimated(windowRect));
    }

    public void CloseWindow(RectTransform windowRect, bool destroy, bool deactivate)
    {
        StartCoroutine(CloseWindowAnimated(windowRect, destroy, deactivate));
    }

    public void CloseWindowImmediate(RectTransform windowRect)
    {
        windowRect.localScale = new Vector3(0, 0, windowRect.localScale.z);
        fadeBackground.SetAlphaZero();
        windowRect.gameObject.SetActive(false);
        fadeBackground.SetActiveFadeBackground(false);
    }

    private IEnumerator OpenWindowAnimated(RectTransform windowRect)
    {
        windowRect.gameObject.SetActive(true);
        StartCoroutine(fadeBackground.ShowFadeBackground());
        for (int t = 1; t < 11; t++)
        {
            float scale = t * 0.1f;
            windowRect.localScale = new Vector3(scale, scale, windowRect.localScale.z);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator CloseWindowAnimated(RectTransform windowRect, bool destroy, bool deactivate)
    {
        StartCoroutine(fadeBackground.HideFadeBackground());
        for (int t = 9; t >= 0; t--)
        {
            float scale = t * 0.1f;
            windowRect.localScale = new Vector3(scale, scale, windowRect.localScale.z);
            yield return new WaitForSeconds(0.01f);
        }
        if (deactivate)
        {
            windowRect.gameObject.SetActive(false);
        }
        if (destroy)
        {
            Destroy(windowRect.gameObject);
        }
    }
}
