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
    public InGameAmbienceLibrary inGameAmbience;

    [Header("Voice-CharactersInGame")]
    public InGameVoiceCharactersLibrary voiceCharactersInGame;
}

[System.Serializable]
public class UIMusicLibrary
{
    public AudioClip MainMenuBGM;
    public AudioClip GameplayBGM;
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
    public AudioClip[] pickup;
    public AudioClip[] waterDrop;
    public AudioClip generalInteractSFX;
    public AudioClip bottlePickUp;
    public AudioClip bottlePutDown;
    public AudioClip coinSFX;
    public AudioClip coinExchange;
}

[System.Serializable]
public class InGameAmbienceLibrary
{
    public AudioClip stoneWalk;
    public AudioClip dirtWalk;
    public AudioClip cauldronConstantBoiling;
}

[System.Serializable]
public class InGameVoiceCharactersLibrary
{
    public AudioClip mainCharacterResponse1;
}