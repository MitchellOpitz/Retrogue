using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioDropdownHandler : MonoBehaviour
{
    private AudioManager audioManager;
    private TMP_Dropdown dropdown;


    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        LoadDropdown();
    }

    private void LoadDropdown()
    {
        dropdown = FindObjectOfType<TMP_Dropdown>();
        dropdown.value = audioManager.currentAudioTrackIndex;
    }

    // This method is called when the dropdown value changes
    public void OnAudioTrackSelected(int trackIndex)
    {
        // Call the AudioManager to transition to the selected track
        audioManager.TransitionToAudioTrack(trackIndex);
    }
}
