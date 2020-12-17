using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RulesWindow : MonoBehaviour
{
    [SerializeField] private RectTransform handRect;
    [SerializeField] private RectTransform figureRect;
    [SerializeField] private Image markImage;
    [SerializeField] private Sprite nought;
    [SerializeField] private Sprite cross;
    private Image handImage;

    public void OpenWindow()
    {
        handImage = handRect.GetComponent<Image>();
        StartCoroutine(HowToPlayAnimation());
    }

    public void CloseWindow()
    {
        WindowManager.GetInstance().CloseWindow(GetComponent<RectTransform>(), true, false);
    }

    private IEnumerator HowToPlayAnimation()
    {
        yield return new WaitForSeconds(0.8f);

        WindowManager.GetInstance().OpenWindow(GetComponent<RectTransform>());
        yield return new WaitForSeconds(0.1f);

        while (true)
        {
            for (int t = 1; t < 6; t++)
            {
                float scale = t * 0.2f;
                figureRect.localScale = new Vector3(scale, scale, 1);
                yield return new WaitForSeconds(0.04f);
            }
            yield return new WaitForSeconds(0.3f);

            Color handColor = handImage.color;

            for (int t = 1; t < 6; t++)
            {
                float a = t * 0.2f;
                handColor.a = a;
                handImage.color = handColor;
                yield return new WaitForSeconds(0.04f);
            }

            float handStartY = -66;
            float handEndY = -26;

            for (int t = 1; t < 11; t++)
            {
                float time = t * 0.1f;
                float newY = Mathf.Lerp(handStartY, handEndY, time);
                handRect.localPosition = new Vector3(handRect.localPosition.x, newY, 0);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.4f);

            float figureStartX = -83;
            float figureEndX = -3;

            for (int t = 1; t < 6; t++)
            {
                float time = t * 0.2f;
                float newX = Mathf.Lerp(figureStartX, figureEndX, time);
                figureRect.localPosition = new Vector3(newX, figureRect.localPosition.y + 2, 0);
                yield return new WaitForSeconds(0.02f);
            }

            figureStartX = -3;
            figureEndX = 77;
            for (int t = 1; t < 6; t++)
            {
                float time = t * 0.2f;
                float newX = Mathf.Lerp(figureStartX, figureEndX, time);
                figureRect.localPosition = new Vector3(newX, figureRect.localPosition.y - 2, 0);
                yield return new WaitForSeconds(0.02f);
            }

            yield return new WaitForSeconds(0.1f);


            for (int t = 4; t >= 0; t--)
            {
                handRect.localPosition = new Vector3(handRect.localPosition.x, handRect.localPosition.y - 8, 0);
                yield return new WaitForSeconds(0.02f);
            }

            for (int t = 4; t >= 0; t--)
            {
                float a = t * 0.2f;
                handColor.a = a;
                handImage.color = handColor;
                yield return new WaitForSeconds(0.02f);
            }
            yield return new WaitForSeconds(0.3f);

            figureRect.localScale = Vector3.zero;
            markImage.sprite = cross;

            figureRect.localPosition = new Vector3(-83, 110, 0);
            handRect.localPosition = new Vector3(11, -66, 0);
            yield return new WaitForSeconds(1.2f);
            markImage.sprite = nought;
        }
    }
}
