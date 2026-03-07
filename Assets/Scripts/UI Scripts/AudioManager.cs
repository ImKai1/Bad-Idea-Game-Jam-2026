using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource masterSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource voiceSource;

    [Header("Preset Music")]
    public AudioClip bgm;
    [Header("Preset Sound Effects")]
    public AudioClip cursorClickGeneral;
    public AudioClip cursorClickOpenUI;
    public AudioClip cursorClickBackOrClose;
    public AudioClip sfx3;

    [Header("Preset Voices")]
    public AudioClip shinraTenseiTest;
    public AudioClip voice2;
    
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
