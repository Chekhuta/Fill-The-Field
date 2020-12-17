using System.Collections;
using UnityEngine;

public class FiguresBackground : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] backgroundsRenderer;

    public void UpdateBackgrounds(int countOfFigures)
    {
        SetPositionsFigures(countOfFigures);
        StartCoroutine(ShowFigureBackgrounds(countOfFigures));
    }

    public static Vector3[] GetBackgroundPositions(int countOfFigures)
    {
        switch (countOfFigures)
        {
            case 1:
                return new Vector3[] { new Vector3(0, -1.8f, 0) };
            case 2:
                return new Vector3[] { new Vector3(-0.6f, -1.8f, 0), new Vector3(0.6f, -1.8f, 0) };
            case 3:
                return new Vector3[] { new Vector3(-1.2f, -1.8f, 0), new Vector3(0, -1.8f, 0), new Vector3(1.2f, -1.8f, 0) };
            case 4:
                return new Vector3[] { new Vector3(-0.6f, -1.08f, 0), new Vector3(0.6f, -1.08f, 0),
                    new Vector3(-0.6f, -2.08f, 0), new Vector3(0.6f, -2.08f, 0) };
            case 5:
                return new Vector3[] { new Vector3(-1.2f, -1.08f, 0), new Vector3(0, -1.08f, 0), new Vector3(1.2f, -1.08f, 0),
                    new Vector3(-0.6f, -2.08f, 0), new Vector3(0.6f, -2.08f, 0) };
            case 6:
                return new Vector3[] { new Vector3(-1.2f, -1.08f, 0), new Vector3(0, -1.08f, 0), new Vector3(1.2f, -1.08f, 0),
                    new Vector3(-1.2f, -2.08f, 0), new Vector3(0f, -2.08f, 0), new Vector3(1.2f, -2.08f, 0) };
            default:
                return new Vector3[] { new Vector3(0, -1.8f, 0) };
        }
    }

    public void SetActiveGlow(bool value)
    {
        FigureSpawner figureSpawner = FigureSpawner.GetInstance();
        for (int i = 0; i < DataStorage.CountOfFigures; i++)
        {
            if (figureSpawner.IsFigureInGame(i) || !value)
            {
                backgroundsRenderer[i].GetComponentsInChildren<SpriteRenderer>()[1].enabled = value;
            }
        }
    }

    private void SetPositionsFigures(int countOfFigures)
    {
        for (int i = 0; i < backgroundsRenderer.Length; i++)
        {
            if (i < countOfFigures)
            {
                backgroundsRenderer[i].gameObject.SetActive(true);
                if (DataStorage.CountOfFigures > 3)
                {
                    backgroundsRenderer[i].transform.localScale = new Vector3(0.9f, 0.9f, 1);
                }
                else
                {
                    backgroundsRenderer[i].transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else
            {
                backgroundsRenderer[i].gameObject.SetActive(false);
            }
        }

        Vector3[] backgroundsPositions = GetBackgroundPositions(countOfFigures);

        for (int i = 0; i < countOfFigures; i++)
        {
            backgroundsRenderer[i].GetComponent<Transform>().localPosition = backgroundsPositions[i];
        }
    }

    private IEnumerator ShowFigureBackgrounds(int countOfFigures)
    {
        Color backgroundColor = backgroundsRenderer[0].color;
        for (int a = 1; a < 6; a++)
        {
            backgroundColor.a = a * 0.01f;
            for (int i = 0; i < countOfFigures; i++)
            {
                backgroundsRenderer[i].color = backgroundColor;
            }
            yield return new WaitForSeconds(0.04f);
        }
    }
}
