using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundSourceManager : MonoBehaviour
{
    [Header("Steps")]
    [SerializeField] private AudioClip[] steps;

    [SerializeField] private bool isPlayer;
    [SerializeField] private bool isEnemy;

    private PlayerController MC;
    private EnemyController cultist;
    private AudioSource soundSource;
    private Animator animator;
    private Coroutine attackSoundCoroutine;

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (isPlayer)
        {
            MC = GetComponentInParent<PlayerController>();
            animator = MC.GetComponent<Animator>();
        }

        else if (isEnemy)
        {
            cultist = GetComponentInParent<EnemyController>();
            animator = cultist.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (soundSource != null)
        {
            HandleAnimationSounds();
        }           
    }

    private void HandleAnimationSounds()
    {
        if (isEnemy)
        {
            if (cultist == null) return;

            if (!cultist.CanMove) return;

            Debug.Log(animator.GetCurrentAnimatorClipInfo(0).Length);
            var anim = animator.GetCurrentAnimatorClipInfo(0);

            if (anim.Length > 0 && anim[0].clip.name.Contains("Falling"))
            {
                PlayClip(steps[1]);
            }
            else PlayClip(steps[0], true);

            return;      
        }

        if (isPlayer)
        {
            if (MC == null) return;
            Debug.Log("NOW PLAYING.... " + soundSource.clip?.name);

            if (MC.Health <= 0)
            {
                StopSound();
                return;
            }

            if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Attack") && MC.name.Contains("Rei"))
            {
                if (attackSoundCoroutine == null)
                {
                    attackSoundCoroutine = StartCoroutine(PlayClipDelayed(steps[3]));
                }

                return;
            }

            if (MC.IsWalking)
            {
                if (steps != null && steps.Length > 0)
                {
                    string currentSceneName = SceneManager.GetActiveScene().name;

                    int soundIndex = 0;
                    int loc = PlayerPrefs.GetInt("LvTrack", 0);

                    switch (currentSceneName)
                    {
                        case "Level1":
                            switch (loc)
                            {
                                case 0:
                                    soundIndex = 2;
                                    break;
                                case 1:
                                    soundIndex = 2;
                                    break;
                                case 2:
                                    soundIndex = 0;
                                    break;
                            }
                            break;

                        case "Level2 1":
                            switch (loc)
                            {
                                case 0:
                                    soundIndex = 1;
                                    break;
                                case 1:
                                    soundIndex = 0;
                                    break;
                                case 2:
                                    soundIndex = 1;
                                    break;
                                case 3:
                                    soundIndex = 0;
                                    break;
                            }
                            break;

                        case "Level2 2":
                            soundIndex = 2;
                            break;

                        default:
                            soundIndex = 0;
                            break;
                    }

                    if (soundIndex < steps.Length && steps[soundIndex] != null)
                    {
                        PlayClip(steps[soundIndex], true);
                    }
                    else
                    {
                        PlayClip(steps[0], true);
                    }
                }

            }
            else
            {
                StopSound();
            }
            return;
        }
    }


    private void PlayClip(AudioClip clip, bool loop = false)
    {
        if (clip == null) return;

        if (soundSource.clip == clip && soundSource.isPlaying) return;

        soundSource.clip = clip;
        soundSource.loop = loop;
        soundSource.Play();
    }

    private IEnumerator PlayClipDelayed(AudioClip clip, bool loop = false)
    {
        if (clip == null) yield break;

        soundSource.clip = clip;
        soundSource.loop = loop;
        soundSource.Play();

        yield return new WaitForSeconds(1.0f);

        // Очищаем ссылку, чтобы при следующем ударе корутина смогла запуститься снова
        attackSoundCoroutine = null;
    }

    private void StopSound()
    {
        if (soundSource.isPlaying)
        {
            soundSource.Stop();
        }
    }
}
