using UnityEngine;

public class ScriptBGMManager : MonoBehaviour
{
    public static ScriptBGMManager Instance;
    private AudioSource audioSource;

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StopBGM()
    {
        if (audioSource != null) audioSource.Stop();
    }

    public void PlayBGM()
    {
        if (audioSource != null && !audioSource.isPlaying) audioSource.Play();
    }
}