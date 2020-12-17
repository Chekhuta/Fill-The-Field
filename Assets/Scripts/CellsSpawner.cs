using UnityEngine;

public class CellsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Sprite[] gridSprites;

    public GridCell[,] SpawnCells(int cellsInRow)
    {
        GridCell[,] gridCells = new GridCell[cellsInRow, cellsInRow];
        float cellSize = (float)3 / cellsInRow;
        cellPrefab.GetComponent<BoxCollider2D>().size = new Vector2(cellSize * 0.2f, cellSize * 0.2f);
        Transform cellParent = GetComponent<Transform>();

        float spawnOffset = 0;
        if (cellsInRow % 2 == 0)
        {
            spawnOffset = cellSize / 2;
        }

        float x = -(cellsInRow / 2 * cellSize) + spawnOffset;
        for (int i = 0; i < cellsInRow; i++)
        {
            float y = -(cellsInRow / 2 * cellSize) + spawnOffset;
            for (int j = 0; j < cellsInRow; j++)
            {
                gridCells[i, j] = Instantiate(cellPrefab, new Vector3(x, y + cellParent.position.y, 1), Quaternion.identity, cellParent).GetComponent<GridCell>();
                y += cellSize;
                gridCells[i, j].InitializeCell(gridSprites[SpriteIndex(i, j, cellsInRow)], cellSize, new Vector2Int(i, j), cellSize);
            }
            x += cellSize;
        }

        return gridCells;
    }

    private int SpriteIndex(int i, int j, int cellsInRow)
    {
        if (i == 0)
        {
            return GetIndexByJ();
        }
        else if (i == cellsInRow - 1)
        {
            return GetIndexByJ() + 2;
        }
        else
        {
            return GetIndexByJ() + 1;
        }

        int GetIndexByJ()
        {
            if (j == 0)
            {
                return 6;
            }
            else if (j == cellsInRow - 1)
            {
                return 0;
            }
            else
            {
                return 3;
            }
        }
    }

}
