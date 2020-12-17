using UnityEngine;
using System.Collections;

public class Mark : MonoBehaviour
{
    [SerializeField] private MarkSprites marks;
    private MarkType markType = MarkType.Empty;
    private SpriteRenderer markRenderer;

    public void InitializeMark(Vector3 markScale)
    {
        markRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = markScale;
    }

    public void SetActiveMark(bool active, bool animated)
    {
        gameObject.SetActive(active);
        if (animated)
        {
            StartCoroutine(ShowMark());
        }
    }

    public void SetMark(MarkType type, bool animated)
    {
        markType = type;
        switch (type)
        {
            case MarkType.Nought:
                ShowNought(animated);
                break;
            case MarkType.Cross:
                ShowCross(animated);
                break;
        }
    }

    public void ShowNought(bool animated)
    {
        if (animated)
        {
            StartCoroutine(NoughtMarkAnimation());
        }
        else
        {
            markRenderer.sprite = marks[3];
        }
    }

    public void ShowCross(bool animated)
    {
        if (animated)
        {
            StartCoroutine(CrossMarkAnimation());
        }
        else
        {
            markRenderer.sprite = marks[0];
        }
    }

    public void RemoveMark()
    {
        markRenderer.sprite = null;
        markType = MarkType.Empty;
    }

    public void InverseMark(bool animated)
    {
        if (markType == MarkType.Nought)
        {
            SetMark(MarkType.Cross, animated);
        }
        else if (markType == MarkType.Cross)
        {
            SetMark(MarkType.Nought, animated);
        }
    }

    public bool IsCross()
    {
        return markType == MarkType.Cross;
    }

    private IEnumerator ShowMark()
    {
        Transform markTransform = GetComponent<Transform>();
        markTransform.localScale = Vector2.zero;
        yield return new WaitForSeconds(0.3f);
        for (int t = 1; t < 6; t++)
        {
            float markScale = 0.2f * t;
            markTransform.localScale = new Vector2(markScale, markScale);
            yield return new WaitForSeconds(0.02f);
        }
    }

    private IEnumerator NoughtMarkAnimation()
    {
        for (int i = 1; i < marks.Length; i++)
        {
            markRenderer.sprite = marks[i];
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator CrossMarkAnimation()
    {
        for (int i = marks.Length - 2; i >= 0; i--)
        {
            markRenderer.sprite = marks[i];
            yield return new WaitForSeconds(0.05f);
        }
    }
}

public enum MarkType
{
    Empty = -1,
    Nought = 0,
    Cross = 1
}
