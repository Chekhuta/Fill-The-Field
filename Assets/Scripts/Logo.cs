using System.Collections;
using UnityEngine;

public class Logo : MonoBehaviour
{
    [SerializeField] private Sprite[] blinkSprites;
    [SerializeField] private SpriteRenderer blinkRenderer;
    [SerializeField] private Sprite[] logoSprites;
    [SerializeField] private GameObject logoText;
    [SerializeField] private AudioClip logoSound;
    private SpriteRenderer logoRenderer;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        logoRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator ShowLogo()
    {
        yield return new WaitForSeconds(2f);
        PlayLogoSound();
        yield return StartCoroutine(PlayBlinkAnimation());
        logoText.SetActive(false);
        yield return StartCoroutine(PlayLogoAnimation());
    }

    private IEnumerator PlayLogoAnimation()
    {
        float delay = 0.06f;
        for (int i = 0; i < logoSprites.Length; i++)
        {
            logoRenderer.sprite = logoSprites[i];
            if (i == 2 || i == 5)
            {
                delay = 0.2f;
            }
            else
            {
                delay = 0.06f;
            }
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator PlayBlinkAnimation()
    {
        for (int i = 0; i < blinkSprites.Length; i++)
        {
            blinkRenderer.sprite = blinkSprites[i];
            yield return new WaitForSeconds(0.08f);
        }
        blinkRenderer.sprite = null;
        yield return new WaitForSeconds(1f);
    }

    private void PlayLogoSound()
    {
        if (DataStorage.IsSoundOn)
        {
            audioSource.PlayOneShot(logoSound);
        }
    }
}
