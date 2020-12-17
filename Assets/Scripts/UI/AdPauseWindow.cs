using UnityEngine;

public class AdPauseWindow : MonoBehaviour
{
    [SerializeField] private RectTransform windowRect;

    public void CloseWindow()
    {
        WindowManager.GetInstance().CloseWindowImmediate(windowRect);
    }

    public void OpenWindow()
    {
        WindowManager.GetInstance().OpenWindow(windowRect);
    }
}
