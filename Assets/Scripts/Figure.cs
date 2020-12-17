using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Figure : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Sprite miniFigureTile;
    [SerializeField] private Sprite figureTile;

    private int figureIndex;
    private FigureStruct figureStruct;
    private Transform figureTransform;
    private Transform[] partsTransform;

    private float figureCellSize;
    private const float miniFigureCellSize = 0.2f;
    private bool isDragging;
    private bool canDragging = true;
    private float scale;
    private bool isHintMode;

    private Vector3 startPosition;
    private Vector3 draggingOffset;
    private BoxCollider2D boxCollider;
    private Collider2D gridCell;

    public void InitializeFigure(int figureIndex, Transform[] partsTransform, FigureStruct figureStruct, float scale)
    {
        this.figureIndex = figureIndex;
        this.scale = scale;
        this.figureStruct = figureStruct;
        this.partsTransform = partsTransform;
        figureCellSize = (float)3 / DataStorage.FieldSize.x;
        figureTransform = GetComponent<Transform>();
        startPosition = new Vector3(
            figureTransform.localPosition.x - (figureStruct.figureSize.x - 1) * miniFigureCellSize / 2,
            figureTransform.localPosition.y - (figureStruct.figureSize.y - 1) * miniFigureCellSize / 2,
            figureTransform.localPosition.z);
        figureTransform.localPosition = startPosition;
        boxCollider = GetComponent<BoxCollider2D>();
        UpdateMiniFigureBoxCollider();
        draggingOffset = new Vector3(figureCellSize * (-(figureStruct.figureSize.x - 1)) / 2, figureCellSize / 2 + 0.6f, 0);
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - figureTransform.position + draggingOffset;
            mousePosition = new Vector2((int)(mousePosition.x * 100) / 100f, (int)(mousePosition.y * 100) / 100f);
            transform.Translate(mousePosition);
        }
    }

    public void SetActiveBoxCollider(bool active)
    {
        boxCollider.enabled = active;
    }

    public void SetHintMode(bool mode)
    {
        isHintMode = mode;
    }

    public void SetCanDragging(bool can)
    {
        canDragging = can;
    }

    public IEnumerator MoveFigureByHint()
    {
        GridCell spawnCell = Game.GetInstance().GetFigureSpawnCell(figureIndex);
        Vector3 destPosition = spawnCell.transform.position;
        destPosition.z = 0;
        Vector3 startPosition = figureTransform.localPosition += new Vector3(0, 0.2f, 0);

        UpdateFigureParts(figureCellSize, figureTile);
        yield return new WaitForSeconds(0.1f);

        for (int t = 1; t < 11; t++)
        {
            float time = t * 0.1f;
            Vector3 positionBuffer = Vector3.Lerp(startPosition, destPosition, time);
            figureTransform.localPosition = positionBuffer;
            yield return new WaitForSeconds(0.01f);
        }

        UpdateBoxCollider(Vector2.zero, new Vector2(figureCellSize * 0.6f, figureCellSize * 0.6f));
        yield return new WaitForSeconds(0.1f);
        PlaceFigureOnBoard(spawnCell);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isHintMode)
        {
            StartCoroutine(MoveFigureByHint());
            Hint.GetInstance().HintUsedOnFigure(figureIndex);
        }
        if (!canDragging)
        {
            return;
        }
        isDragging = true;
        canDragging = false;
        UpdateFigureParts(figureCellSize, figureTile);
        UpdateBoxCollider(Vector2.zero, new Vector2(figureCellSize * 0.6f, figureCellSize * 0.6f));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDragging)
        {
            return;
        }
        isDragging = false;
        if (gridCell != null && BoardGrid.GetInstance().CanPlaceFigure(gridCell.GetComponent<GridCell>(), figureStruct.figureSize))
        {
            figureTransform.localPosition = new Vector3(gridCell.transform.position.x, gridCell.transform.position.y, figureTransform.localPosition.z);
            PlaceFigureOnBoard(gridCell.GetComponent<GridCell>());
            FigureSpawner.GetInstance().DeleteFigure(figureIndex);
        }
        else
        {
            StartCoroutine(MoveFigureToStartPosition());
        }
    }

    public void InverseBoardMarks(Vector2Int spawnPosition)
    {
        BoardGrid.GetInstance().InverseMarks(spawnPosition, figureStruct.partsPositions, false);
    }

    private void PlaceFigureOnBoard(GridCell spawnCell)
    {
        BoardGrid board = BoardGrid.GetInstance();
        board.InverseMarks(spawnCell.CellPosition, figureStruct.partsPositions, true);
        board.CheckWin();
        StartCoroutine(DestroyFigure());
    }

    private IEnumerator DestroyFigure()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Grid Cell"))
        {
            gridCell = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Grid Cell"))
        {
            gridCell = null;
        }
    }

    private IEnumerator MoveFigureToStartPosition()
    {
        Vector3 startMovingPosition = figureTransform.localPosition;

        float movingTime = 0;
        bool isSpriteChanged = false;
        int scaleDownCount = 3;

        while (figureTransform.localPosition != startPosition)
        {
            movingTime += 0.1f;
            float interpolatedValueX = Mathf.Lerp(startMovingPosition.x, startPosition.x, movingTime);
            float interpolatedValueY = Mathf.Lerp(startMovingPosition.y, startPosition.y, movingTime);

            if (!isSpriteChanged)
            {
                if (scaleDownCount != 0)
                {
                    figureTransform.localScale -= new Vector3(0.16f, 0.16f, 0);
                    scaleDownCount--;
                }
                else
                {
                    figureTransform.localScale = new Vector3(scale, scale, 1);
                    UpdateFigureParts(miniFigureCellSize, miniFigureTile);
                    isSpriteChanged = true;
                }
            }
            figureTransform.localPosition = new Vector3(interpolatedValueX, interpolatedValueY, startPosition.z);
            yield return new WaitForFixedUpdate();
        }
        UpdateMiniFigureBoxCollider();
        canDragging = true;
    }

    private void UpdateFigureParts(float cellSize, Sprite tile)
    {
        for (int i = 0; i < partsTransform.Length; i++)
        {
            partsTransform[i].localPosition = new Vector3(cellSize * figureStruct.partsPositions[i].x, cellSize * figureStruct.partsPositions[i].y, 0);
            partsTransform[i].GetComponent<SpriteRenderer>().sprite = tile;
            partsTransform[i].GetComponent<SpriteRenderer>().size = new Vector2(cellSize, cellSize);
        }
    }

    private void UpdateMiniFigureBoxCollider()
    {
        boxCollider.offset = new Vector2((figureStruct.figureSize.x - 1) * miniFigureCellSize / 2, (figureStruct.figureSize.y - 1) * miniFigureCellSize / 2);
        boxCollider.size = new Vector2(0.6f, 0.6f);
    }

    private void UpdateBoxCollider(Vector2 colliderOffset, Vector2 colliderSize)
    {
        boxCollider.offset = colliderOffset;
        boxCollider.size = colliderSize;
    }
}
