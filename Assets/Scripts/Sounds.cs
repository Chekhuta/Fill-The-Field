using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] private AudioClip windowButtonSound;
    private AudioSource audioSource;
    private static Sounds instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static Sounds GetInstance()
    {
        return instance;
    }

    public void PlayGameButtonSound()
    {
        if (!DataStorage.IsSoundOn)
        {
            return;
        }
        audioSource.PlayOneShot(windowButtonSound);
    }

    public void PlayWindowButtonSound()
    {
        if (!DataStorage.IsSoundOn)
        {
            return;
        }
        audioSource.PlayOneShot(windowButtonSound);
    }
}
