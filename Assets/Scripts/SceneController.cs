using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject WinPanel;

    [SerializeField] private int sceneIndex;

    [Header("Audio Settings")]
    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(false);
        } 

        if (WinPanel != null)
        {
            WinPanel.SetActive(false);
        }
    }
    public void ActivatePanel(GameObject panel)
    {
        panel.SetActive(true);
        ResetTime(0f);
        panel.GetComponent<AnimationController>().ActivateAnimation(null, "appearing panel");
    }

    public void ActivatePanel(GameObject panel, string animName)
    {
        panel.SetActive(true);
        ResetTime(0f);
        panel.GetComponent<AnimationController>().ActivateAnimation(null, animName);
        Debug.Log(animName);

    }

    public void DeactivatePanel(GameObject panel)
    {
        panel.GetComponent<AnimationController>().ActivateAnimation("Deactivate");
        StartCoroutine(DelayedDeactivation(panel));
        if (audioMixer != null)
        {
            audioMixer.SetFloat("SFX", 0f);
            audioMixer.SetFloat("Music", 0f);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResetTime();
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator DelayedDeactivation(GameObject panel)
    {
        yield return new WaitForSecondsRealtime(1f);
        panel.GetComponent<AnimationController>().DeactivateAnimation("Deactivate");
        panel.SetActive(false);
        ResetTime();
    }

    public void ResetTime(float time = 1f)
    {
        Time.timeScale = time;
        if (audioMixer != null)
        {
            audioMixer.SetFloat("SFX", time == 1f ? 0f : -80f);
            audioMixer.SetFloat("Music", time == 1f ? 0f : -20f);
        }
    }

    public void ChangeSceneIndex(int index)
    {
        sceneIndex = index;
    }
}
