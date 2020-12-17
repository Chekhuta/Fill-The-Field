using System.Collections;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public Vector2Int CellPosition { get; private set; }
    [SerializeField] private Mark mark;
    private SpriteRenderer spriteRenderer;

    public void InitializeCell(Sprite gridPart, float spriteSize, Vector2Int position, float markScale)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = gridPart;
        CellPosition = position;
        spriteRenderer.size = new Vector2(spriteSize, spriteSize);
        mark.InitializeMark(new Vector3(markScale, markScale, 1));
    }

    public IEnumerator SpawnCellAnimated()
    {
        Transform cellTransform = GetComponent<Transform>();
        Vector3 startPosition = cellTransform.localPosition;
        for (int t = 1; t < 6; t++)
        {
            float spriteSize = 0.2f * t;
            float x = startPosition.x == 0 ? 1 : Mathf.Abs(spriteSize * startPosition.x);
            float y = startPosition.y == 0 ? 1 : Mathf.Abs(spriteSize * startPosition.y);
            spriteRenderer.size = new Vector2(x, y);
            float newPosition = (1 - ((1 - spriteSize) / 2));
            cellTransform.localPosition = new Vector2(startPosition.x * newPosition, startPosition.y * newPosition);
            yield return new WaitForSeconds(0.02f);
        }
        cellTransform.localPosition = startPosition;
    }

    public Mark GetMark()
    {
        return mark;
    }
}
