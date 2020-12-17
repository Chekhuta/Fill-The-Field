using UnityEngine;
using System.Collections;

public class FigureSpawner : MonoBehaviour
{
    [SerializeField] private GameObject miniFigurePrefab;
    [SerializeField] private GameObject miniFigurePartPrefab;
    [SerializeField] private FiguresBackground figuresBackground;
    private Figure[] figures;
    private Vector3[] spawnPositions;
    private const float cellSize = 0.2f;
    private bool[] isHintUsed;

    private static FigureSpawner instance;

    public static FigureSpawner GetInstance()
    {
        return instance;
    }

    public bool IsAvailableFigures()
    {
        for (int i = 0; i < figures.Length; i++)
        {
            if (figures[i] != null)
            {
                return true;
            }
        }
        return false;
    }

    public void DeleteFigure(int fIndex)
    {
        figures[fIndex] = null;
    }

    public void InitializeFigureSpawner()
    {
        instance = this;
    }

    public bool IsFigureInGame(int index)
    {
        return figures[index] != null;
    }

    public void SpawnFigures(FigureSpawnInfo[] levelFigures, int countOfFigures)
    {
        UpdateFiguresBackgrounds(countOfFigures);
        figures = new Figure[countOfFigures];
        isHintUsed = new bool[countOfFigures];
        spawnPositions = FiguresBackground.GetBackgroundPositions(countOfFigures);
        for (int i = 0; i < figures.Length; i++)
        {
            SpawnFigure(i, GetFigureStruct(levelFigures[i].FigureId, levelFigures[i].AngleId), 0.2f);
            figures[i].InverseBoardMarks(new Vector2Int(levelFigures[i].PositionX, levelFigures[i].PositionY));
        }
    }

    public void SetHintUsed(int figureIndex)
    {
        isHintUsed[figureIndex] = true;
    }
    
    public void SetEnabledHintMode(bool enabled, float delay = 0)
    {
        for (int i = 0; i < figures.Length; i++)
        {
            if (IsFigureInGame(i))
            {
                figures[i].SetHintMode(enabled);
                figuresBackground.SetActiveGlow(enabled);
            }
        }
        StartCoroutine(SetBlockFigures(!enabled, delay));
    }

    private IEnumerator SetBlockFigures(bool block, float delay)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < figures.Length; i++)
        {
            if (IsFigureInGame(i))
            {
                figures[i].SetCanDragging(block);
            }
        }
    }

    public FigureStruct GetFigureStruct(int figureId, int angleId)
    {
        Vector2Int[] partsPositions = null;
        Vector2Int figureSize = Vector2Int.zero;

        switch (figureId)
        {
            case 0:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 0) };
                figureSize = new Vector2Int(1, 1);
                break;
            case 1:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1) };
                figureSize = new Vector2Int(1, 2);
                break;
            case 2:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(1, 1) };
                figureSize = new Vector2Int(2, 2);
                break;
            case 3:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(1, 1) };
                figureSize = new Vector2Int(2, 2);
                break;
            case 4:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, 2) };
                figureSize = new Vector2Int(1, 3);
                break;
            case 5:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(1, 1) };
                figureSize = new Vector2Int(2, 2);
                break;
            case 6:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(1, 2) };
                figureSize = new Vector2Int(2, 3);
                break;
            case 7:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(1, 1), new Vector2Int(0, 2) };
                figureSize = new Vector2Int(2, 3);
                break;
            case 8:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, 2), new Vector2Int(1, 2) };
                figureSize = new Vector2Int(2, 3);
                break;
            case 9:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, 2), new Vector2Int(1, 1) };
                figureSize = new Vector2Int(2, 3);
                break;
            case 10:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(1, 1), new Vector2Int(2, 2) };
                figureSize = new Vector2Int(3, 3);
                break;
            case 11:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, 2), new Vector2Int(0, 3) };
                figureSize = new Vector2Int(1, 4);
                break;
            case 12:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, 2), new Vector2Int(1, 0), new Vector2Int(1, 2), new Vector2Int(2, 0), new Vector2Int(2, 1), new Vector2Int(2, 2) };
                figureSize = new Vector2Int(3, 3);
                break;
            case 13:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(1, 2), new Vector2Int(2, 1) };
                figureSize = new Vector2Int(3, 3);
                break;
            case 14:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, 2), new Vector2Int(1, 2), new Vector2Int(2, 2) };
                figureSize = new Vector2Int(3, 3);
                break;
            case 15:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, 2), new Vector2Int(1, 0), new Vector2Int(1, 2) };
                figureSize = new Vector2Int(2, 3);
                break;
            case 16:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, 2), new Vector2Int(1, 1), new Vector2Int(2, 1) };
                figureSize = new Vector2Int(3, 3);
                break;
            case 17:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(1, 1), new Vector2Int(2, 2), new Vector2Int(3, 3) };
                figureSize = new Vector2Int(4, 4);
                break;
            case 18:
                partsPositions = new Vector2Int[] { new Vector2Int(0, 2), new Vector2Int(0, 3), new Vector2Int(1, 0), new Vector2Int(1, 1) };
                figureSize = new Vector2Int(2, 4);
                break;
        }

        if (angleId != 0)
        {
            Vector2 pivotPoint = Vector2.zero;
            switch (angleId)
            {
                case 1:
                    pivotPoint = new Vector2((float)(figureSize.x - 1) / 2, (float)(figureSize.x - 1) / 2);
                    break;
                case 2:
                    pivotPoint = new Vector2((float)(figureSize.x - 1) / 2, (float)(figureSize.y - 1) / 2);
                    break;
                case 3:
                    pivotPoint = new Vector2((float)(figureSize.y - 1) / 2, (float)(figureSize.y - 1) / 2);
                    break;
            }

            for (int i = 0; i < partsPositions.Length; i++)
            {
                switch (angleId)
                {
                    case 1:
                        partsPositions[i] = new Vector2Int(
                            (int)(pivotPoint.x + partsPositions[i].y - pivotPoint.y),
                            (int)(pivotPoint.y - partsPositions[i].x + pivotPoint.x));
                        break;
                    case 2:
                        partsPositions[i] = new Vector2Int(
                            (int)(2 * pivotPoint.x - partsPositions[i].x),
                            (int)(2 * pivotPoint.y - partsPositions[i].y));
                        break;
                    case 3:
                        partsPositions[i] = new Vector2Int(
                            (int)(pivotPoint.x - partsPositions[i].y + pivotPoint.y),
                            (int)(pivotPoint.y + partsPositions[i].x - pivotPoint.x));
                        break;
                }

            }
            if (angleId == 1 || angleId == 3)
            {
                figureSize = new Vector2Int(figureSize.y, figureSize.x);
            }
        }
        return new FigureStruct(partsPositions, figureSize);
    }

    public void DestroyFigures()
    {
        for (int i = 0; i < figures.Length; i++)
        {
            if (figures[i] != null)
            {
                Destroy(figures[i].gameObject);
            }
        }
    }

    public void RespawnFigures(FigureSpawnInfo[] levelFigures)
    {
        for (int i = 0; i < levelFigures.Length; i++)
        {
            if (!isHintUsed[i])
            {
                if (figures[i] == null)
                {
                    SpawnFigure(i, GetFigureStruct(levelFigures[i].FigureId, levelFigures[i].AngleId), 0);
                }
                figures[i].InverseBoardMarks(new Vector2Int(levelFigures[i].PositionX, levelFigures[i].PositionY));
            }
        }
    }

    private void UpdateFiguresBackgrounds(int countOfFigures)
    {
        if (figures == null || figures.Length != countOfFigures)
        {
            figuresBackground.UpdateBackgrounds(countOfFigures);
        }
    }

    private void SpawnFigure(int figureIndex, FigureStruct figureStruct, float spawnDelay)
    {
        figures[figureIndex] = Instantiate(miniFigurePrefab, spawnPositions[figureIndex], Quaternion.identity).GetComponent<Figure>();

        Transform[] partsTransform = new Transform[figureStruct.partsPositions.Length];

        for (int i = 0; i < figureStruct.partsPositions.Length; i++)
        {
            partsTransform[i] = Instantiate(miniFigurePartPrefab).GetComponent<Transform>();
            partsTransform[i].parent = figures[figureIndex].transform;
            partsTransform[i].localPosition = new Vector3(cellSize * figureStruct.partsPositions[i].x, cellSize * figureStruct.partsPositions[i].y, 0);
        }
        float destScale = 1;
        if (DataStorage.CountOfFigures > 3)
        {
            destScale = 0.9f;
        }
        figures[figureIndex].InitializeFigure(figureIndex, partsTransform, figureStruct, destScale);
        figures[figureIndex].transform.localScale = new Vector2(0, 0);
        StartCoroutine(ShowFigure(figureIndex, spawnDelay, destScale));
    }

    private IEnumerator ShowFigure(int figureIndex, float spawnDelay, float destScale)
    {
        yield return new WaitForSeconds(spawnDelay);
        Transform figureTransform = figures[figureIndex].GetComponent<Transform>();
        for (int t = 1; t < 6; t++)
        {
            float time = 0.2f * t;
            float scale = Mathf.Lerp(0, destScale, time);
            figureTransform.localScale = new Vector2(scale, scale);
            yield return new WaitForSeconds(0.02f);
        }
    }
}

public class FigureStruct
{
    public Vector2Int[] partsPositions;
    public Vector2Int figureSize;

    public FigureStruct(Vector2Int[] parts, Vector2Int size)
    {
        partsPositions = parts;
        figureSize = size;
    }
}
