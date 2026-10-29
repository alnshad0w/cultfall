using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private new AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Play()
    {
        audio.Play();
    }
}
