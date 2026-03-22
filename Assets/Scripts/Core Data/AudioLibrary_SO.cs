using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Data/AudioLibrary")]
public class AudioLibrary_SO : ScriptableObject
{
    [Header("Music-UI")]
    public MusicUILibrary musicUI;

    [Header("SFX-UI")]
    public UISFXLibrary uiSFX;

    [Header("Music-InGame")]
    public MusicInGameLibrary musicInGame;

    [Header("SFX-InGame")]
    public InGameSFXLibrary inGameSFX;

    [Header("Ambience-InGame")]
    public AmbienceLibrary ambience;

    [Header("Voice-CharactersInGame")]
    public VoiceCharactersInGameLibrary voiceCharactersInGame;
}

[System.Serializable]
public class MusicUILibrary
{
    public AudioClip MainMenuBGM;
}

[System.Serializable]
public class UISFXLibrary
{
    public AudioClip[] cursorClickSFX;
}

[System.Serializable]
public class MusicInGameLibrary
{
    public AudioClip potionShopBGM;
}

[System.Serializable]
public class InGameSFXLibrary
{
    public AudioClip generalInteractSFX;
}

[System.Serializable]
public class AmbienceLibrary
{
    public AudioClip natureAmbience;
}

[System.Serializable]
public class VoiceCharactersInGameLibrary
{
    public AudioClip mainCharacterResponse1;
}