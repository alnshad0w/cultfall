using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Settings")]
    [SerializeField] private float fadeDuration = 1.0f;

    [Header("Intro")]
    [SerializeField] private AudioClip intro;

    [Header("Lv1")]
    [SerializeField] private AudioClip[] tracksLv1;

    [Header("Lv2")]
    [SerializeField] private AudioClip[] tracksLv2;

    [Header("Final")]
    [SerializeField] private AudioClip final;

    private AudioSource audioSource;
    private AudioClip targetClip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.volume = 0f;
            audioSource.loop = true;
            audioSource.playOnAwake = false;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic(intro);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var index = PlayerPrefs.GetInt("LvTrack");
        switch (scene.name)
        {
            case "Level1":
                ChangeMusic(tracksLv1[index]);
                break;
            case "Level2 1":
                ChangeMusic(tracksLv2[index]);
                break;
            case "Level2 2":
                ChangeMusic(final, 0.266f);
                break;
            default:
                ChangeMusic(intro);
                break;
        }
    }

    public void ChangeMusic(AudioClip newClip, float targetVolume = 1f)
    {
        if (newClip == null || audioSource.clip == newClip) return;

        targetClip = newClip;
        StartCoroutine(FadeMusic(targetVolume));
    }

    private IEnumerator FadeMusic(float targetVolume)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = targetClip;
        audioSource.Play();

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, targetVolume, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }


    public void PlayMusic(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

}
