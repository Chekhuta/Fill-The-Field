using UnityEngine;

[CreateAssetMenu(fileName = "New MarkSprites", menuName = "Mark Sprites", order = 51)]
public class MarkSprites : ScriptableObject
{
    [SerializeField]
    private Sprite[] markSprites;

    public int Length
    {
        get
        {
            return markSprites.Length;
        }
    }

    public Sprite this[int index]
    {
        get
        {
            return markSprites[index];
        }
    }
}
