using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource masterSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource voiceSource;

    [Header("Music")]
    public AudioClip bgm;
    public AudioClip bgm1;
    public AudioClip bgm2;
    public AudioClip bgm3;
    public AudioClip bgm4;

    [Header("Sound Effects")]
    public AudioClip cursorClickGeneral;
    public AudioClip cursorClickOpenUI;
    public AudioClip cursorClickBackOrClose;
    public AudioClip sfx3;
    public AudioClip sfx4;
    public AudioClip sfx5;

    [Header("Voices")]
    public AudioClip shinraTenseiTest;
    public AudioClip voice2;
    public AudioClip voice3;
    public AudioClip voice4;
    public AudioClip voice5;
    public AudioClip voice6;

    private void Start()
    {
        musicSource.clip = bgm;
        musicSource.Play();
    }

    // ========================= 
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayVoice(AudioClip clip)
    {
        voiceSource.clip = clip;
        voiceSource.Play();
    }

    // ================================

    public void PlayCursorGeneralClick()
    {
        PlaySFX(cursorClickGeneral);
    }
    public void PlayCursorOpenUIClick()
    {
        PlaySFX(cursorClickOpenUI);
    }
    public void PlayCursorCloseOrBack()
    {
        PlaySFX(cursorClickBackOrClose);
    }
}
