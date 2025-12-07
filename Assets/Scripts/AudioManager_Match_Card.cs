using UnityEngine;

public class AudioManager_Match_Card : MonoBehaviour
{
    public static AudioManager_Match_Card Instance;

    [Header("Audio Clips")]
    public AudioClip showIconClip;
    public AudioClip gameOverClip;
    public AudioClip cardMatchClip;
    public AudioClip winClip;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayShowIcon() => audioSource.PlayOneShot(showIconClip);
    public void PlayGameOver() => audioSource.PlayOneShot(gameOverClip);
    public void PlayCardMatch() => audioSource.PlayOneShot(cardMatchClip);
    public void PlayWin() => audioSource.PlayOneShot(winClip);
}