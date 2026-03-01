using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioMixer musicMixer;

    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioMixer sfxMixer;
    [SerializeField] private int volumeMultiplier;

    // private void Start () {
    //     SetVolume(PlayerProfs.GetFloat("SavedMasterVolume", 100));
    // }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        musicMixer.SetFloat("music", Mathf.Log10(volume) * volumeMultiplier);
    }

}
