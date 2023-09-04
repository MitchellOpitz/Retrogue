using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public TMP_Dropdown dropdown; // Reference to the TMP_Dropdown for selecting music tracks
    public static AudioManager instance;
    public Sound[] sounds;

    public int currentAudioTrackIndex; // Default track index

    private const string MusicVolumePlayerPrefKey = "MusicVolume";

    void Awake()
    {
        /*
        // Checks if an instance of the AudioManager already exists in a scene.
        // Used when transitioning between scenes to avoid restarting music.
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        */

        // Attaches AudioSource component from each sound to the AudioManager game object.
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }

        // Load the saved audio track preference
        currentAudioTrackIndex = PlayerPrefs.GetInt("CurrentAudioTrackIndex", currentAudioTrackIndex);
        PlayAudioTrack(currentAudioTrackIndex);

        // Load the saved music volume preference
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumePlayerPrefKey, 0.5f); // Default to 0.5 (50% volume)
        musicVolumeSlider.value = savedMusicVolume; // Update the slider's value
        UpdateMusicVolume(); // Set the volume based on the loaded preference
    }

    public void PlaySound(string name)
    {
        Sound sound = System.Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound not found: " + name + ".");
            return;
        }
        sound.source.Play();
    }

    public void PlayAudioTrack(int trackIndex)
    {
        if (trackIndex >= 0 && trackIndex < sounds.Length)
        {
            StopAllAudio();
            currentAudioTrackIndex = trackIndex;
            PlaySound(sounds[currentAudioTrackIndex].name);

            // Save the current audio track preference
            PlayerPrefs.SetInt("CurrentAudioTrackIndex", currentAudioTrackIndex);
        }
    }

    private void StopAllAudio()
    {
        foreach (Sound sound in sounds)
        {
            sound.source.Stop();
        }
    }

    public void TransitionToAudioTrack(int trackIndex)
    {
        StartCoroutine(FadeOutAndChangeTrack(trackIndex));
    }

    private System.Collections.IEnumerator FadeOutAndChangeTrack(int newTrackIndex)
    {
        float fadeDuration = 2.0f; // Duration of fade-out
        float timer = 0f;
        float startVolume = sounds[currentAudioTrackIndex].source.volume;

        while (timer < fadeDuration)
        {
            float normalizedTime = timer / fadeDuration;
            float currentVolume = Mathf.Lerp(startVolume, 0f, normalizedTime);
            sounds[currentAudioTrackIndex].source.volume = currentVolume;

            timer += Time.deltaTime;
            yield return null;
        }

        sounds[currentAudioTrackIndex].source.Stop();
        currentAudioTrackIndex = newTrackIndex;
        PlaySound(sounds[currentAudioTrackIndex].name);

        StartCoroutine(FadeInNewTrack());
    }

    private System.Collections.IEnumerator FadeInNewTrack()
    {
        float fadeDuration = 2.0f; // Duration of fade-in
        float timer = 0f;
        float startVolume = sounds[currentAudioTrackIndex].source.volume * musicVolumeSlider.value;

        sounds[currentAudioTrackIndex].source.volume = 0f;
        sounds[currentAudioTrackIndex].source.Play();

        while (timer < fadeDuration)
        {
            float normalizedTime = timer / fadeDuration;
            float currentVolume = Mathf.Lerp(0f, startVolume, normalizedTime);
            sounds[currentAudioTrackIndex].source.volume = currentVolume;

            timer += Time.deltaTime;
            yield return null;
        }

        sounds[currentAudioTrackIndex].source.volume = startVolume;

        // Update the dropdown value to reflect the selected track
        dropdown = FindObjectOfType<TMP_Dropdown>();
        dropdown.value = currentAudioTrackIndex;

        // Save the current audio track preference
        PlayerPrefs.SetInt("CurrentAudioTrackIndex", currentAudioTrackIndex);
    }

    // Add this method to update the music volume
    public void UpdateMusicVolume()
    {
        float volume = musicVolumeSlider.value;
        // Update the volume of the currently selected music track's AudioSource
        if (currentAudioTrackIndex >= 0 && currentAudioTrackIndex < sounds.Length)
        {
            sounds[currentAudioTrackIndex].source.volume = volume;
        }

        // Save the music volume preference to PlayerPrefs
        PlayerPrefs.SetFloat(MusicVolumePlayerPrefKey, volume);
        PlayerPrefs.Save();
    }
}
