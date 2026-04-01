using UnityEngine;
using UnityEngine.Audio;

public class AudioPlaybackManager : MonoBehaviour
{
    public static AudioPlaybackManager Instance { get; private set; }

    // [Header("Audio Mixer")]
    // [SerializeField] private AudioMixer masterMixer;

    [Header("Audio Source")]
    [SerializeField] private AudioSource masterSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource ambienceSource;
    [SerializeField] private AudioSource voiceSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ============================================================================================== //
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        musicSource.UnPause();
    }

    // ============================================================================================== //

    public void PlaySFXConsistent(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFXAutoRandomize(AudioClip clip, float randomVolumeChange = 0.1f, float randomPitchChange = 0.1f)
    {
        float randomVolumeValue = Random.Range(sfxSource.volume - randomVolumeChange, sfxSource.volume + randomVolumeChange);
        float randomPitchValue = Random.Range(sfxSource.pitch - randomPitchChange, sfxSource.pitch + randomPitchChange);

        float originalPitch = sfxSource.pitch;
        sfxSource.pitch = randomPitchValue;
        sfxSource.PlayOneShot(clip, randomVolumeValue);
        sfxSource.pitch = originalPitch;
    }

    public void PlaySFXRandomizeList(AudioClip[] clips, float randomVolumeChange = 0f, float randomPitchChange = 0f)
    {
        int randomClip = Random.Range(0, clips.Length);
        float randomVolumeValue = Random.Range(sfxSource.volume - randomVolumeChange, sfxSource.volume + randomVolumeChange);
        float randomPitchValue = Random.Range(sfxSource.pitch - randomPitchChange, sfxSource.pitch + randomPitchChange);

        float originalPitch = sfxSource.pitch;
        sfxSource.pitch = randomPitchValue;
        sfxSource.PlayOneShot(clips[randomClip], randomVolumeValue);
        sfxSource.pitch = originalPitch;
    }

    // ============================================================================================== //\

    public void PlayAmbience (AudioClip clip) {
        ambienceSource.clip = clip;
        ambienceSource.loop = true;
        ambienceSource.Play();
    }

    // ============================================================================================== //

    public void PlayVoice(AudioClip clip)
    {
        voiceSource.clip = clip;
        voiceSource.Play();
    }

    public void StopVoice()
    {
        voiceSource.Stop();
    }

}

