using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Data/AudioLibrary")]
public class AudioLibrary_SO : ScriptableObject
{
    [Header("Music-UI")]
    public UIMusicLibrary musicUI;

    [Header("SFX-UI")]
    public UISFXLibrary uiSFX;

    [Header("Music-InGame")]
    public InGameMusicLibrary musicInGame;

    [Header("SFX-InGame")]
    public InGameSFXLibrary inGameSFX;

    [Header("Ambience-InGame")]
    public InGameAmbienceLibrary ambience;

    [Header("Voice-CharactersInGame")]
    public InGameVoiceCharactersLibrary voiceCharactersInGame;
}

[System.Serializable]
public class UIMusicLibrary
{
    public AudioClip MainMenuBGM;
}

[System.Serializable]
public class UISFXLibrary
{
    public AudioClip[] cursorClickSFX;
}

[System.Serializable]
public class InGameMusicLibrary
{
    public AudioClip potionShopBGM;
}

[System.Serializable]
public class InGameSFXLibrary
{
    public AudioClip generalInteractSFX;
}

[System.Serializable]
public class InGameAmbienceLibrary
{
    public AudioClip natureAmbience;
}

[System.Serializable]
public class InGameVoiceCharactersLibrary
{
    public AudioClip mainCharacterResponse1;
}