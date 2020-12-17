using System.Collections;
using UnityEngine;

public class CompleteLines : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] lines;

    public void InitializeCompleteLines()
    {
        float[] spawnPositionY = GetLineSpawnPositionY();

        for (int i = 0; i < DataStorage.FieldSize.x; i++)
        {
            lines[i].transform.localPosition = new Vector3(0, spawnPositionY[i], -1);
        }
    }

    public float[] GetLineSpawnPositionY()
    {
        switch (DataStorage.FieldSize.x)
        {
            case 3:
                return new float[] { 1, 0, -1 };
            case 4:
                return new float[] { 1.12f, 0.37f, -0.37f, -1.12f };
            case 5:
                return new float[] { 1.2f, 0.6f, 0, -0.6f, -1.2f };
            default:
                return new float[] { 1, 0, -1 };
        }
    }

    public void HideLines()
    {
        for (int i = 0; i < DataStorage.FieldSize.x; i++)
        {
            lines[i].size = new Vector2(0, 0.06f);
            lines[i].gameObject.SetActive(false);
        }
    }

    public void ShowLines()
    {
        for (int i = 0; i < DataStorage.FieldSize.x; i++)
        {
            lines[i].gameObject.SetActive(true);
        }
        StartCoroutine(ShowLinesAnimation());
    }

    private IEnumerator ShowLinesAnimation()
    {
        for (int t = 1; t < 11; t++)
        {
            float time = t * 0.1f;
            float newWidth = Mathf.Lerp(0, 3, time);
            for (int i = 0; i < DataStorage.FieldSize.x; i++)
            {
                lines[i].size = new Vector2(newWidth, 0.06f);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
