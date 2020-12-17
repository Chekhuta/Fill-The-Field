using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class CorrectRatio : MonoBehaviour
{
    [SerializeField] private CanvasScaler[] canvasScaler;

    private void Start()
    {
        float targetAspect = 16.0f / 9.0f;
        float windowAspect = (float)Screen.height / Screen.width;

        if (targetAspect < windowAspect)
        {
            GetComponent<Camera>().orthographicSize = 3.2f * windowAspect / targetAspect;
            for (int i = 0; i < canvasScaler.Length; i++)
            {
                if (canvasScaler != null)
                {
                    canvasScaler[i].matchWidthOrHeight = 0;
                }
            }
        }
    }
}
