using UnityEngine;

public class BoardGrid : MonoBehaviour
{
    private GridCell[,] gridCells;
    private static BoardGrid instance;

    public static BoardGrid GetInstance()
    {
        return instance;
    }

    public GridCell GetGridCell(int x, int y)
    {
        return gridCells[x, y];
    }

    public void SpawnField()
    {
        instance = this;
        gridCells = GetComponent<CellsSpawner>().SpawnCells(DataStorage.FieldSize.x);
        FillBoard(MarkType.Cross);
        for (int i = 0; i < gridCells.GetLength(0); i++)
        {
            for (int j = 0; j < gridCells.GetLength(1); j++)
            {
                if (i != 1 || j != 1)
                {
                    StartCoroutine(gridCells[i, j].SpawnCellAnimated());
                }
            }
        }
    }

    public void RespawnField()
    {
        DestroyBoardGrid();
        GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        gridCells = GetComponent<CellsSpawner>().SpawnCells(DataStorage.FieldSize.x);
    }

    public void UpdateFieldScale()
    {
        Transform boardTrasform = GetComponent<Transform>();
        if (DataStorage.CountOfFigures > 3)
        {
            boardTrasform.localScale = new Vector3(0.9f, 0.9f, 1);
            boardTrasform.localPosition = new Vector3(0, 0.9f, 1);
        }
        else
        {
            boardTrasform.localScale = new Vector3(1, 1, 1);
            boardTrasform.localPosition = new Vector3(0, 0.6f, 1);
        }
    }

    public void FillBoard(MarkType markType)
    {
        for (int i = 0; i < gridCells.GetLength(0); i++)
        {
            for (int j = 0; j < gridCells.GetLength(1); j++)
            {
                gridCells[i, j].GetMark().SetMark(markType, false);
            }
        }
    }

    public void ShowMarks(bool animated)
    {
        for (int i = 0; i < gridCells.GetLength(0); i++)
        {
            for (int j = 0; j < gridCells.GetLength(1); j++)
            {
                gridCells[i, j].GetMark().SetActiveMark(true, animated);
            }
        }
    }

    public bool CanPlaceFigure(GridCell cell, Vector2Int figureSize)
    {
        if ((cell.CellPosition.x + figureSize.x <= DataStorage.FieldSize.x) && (cell.CellPosition.y + figureSize.y <= DataStorage.FieldSize.y))
        {
            return true;
        }
        return false;
    }

    public void InverseMarks(Vector2Int cellPosition, Vector2Int[] figureBlocks, bool animated)
    {
        for (int i = 0; i < figureBlocks.Length; i++)
        {
            gridCells[cellPosition.x + figureBlocks[i].x, cellPosition.y + figureBlocks[i].y].GetMark().InverseMark(animated);
        }
    }

    public void CheckWin()
    {
        if (IsAllMarksCross())
        {
            FindObjectOfType<Game>().NextLevel();
        }
    }

    public bool IsAllMarksCross()
    {
        for (int i = 0; i < gridCells.GetLength(0); i++)
        {
            for (int j = 0; j < gridCells.GetLength(1); j++)
            {
                if (!gridCells[i, j].GetMark().IsCross())
                {
                    return false;
                }
            }
        }
        return true;
    }

    public Vector2Int GetFieldSize()
    {
        return new Vector2Int(gridCells.GetLength(0), gridCells.GetLength(1));
    }

    private void DestroyBoardGrid()
    {
        for (int i = 0; i < gridCells.GetLength(0); i++)
        {
            for (int j = 0; j < gridCells.GetLength(1); j++)
            {
                Destroy(gridCells[i, j].gameObject);
            }
        }
    }
}
