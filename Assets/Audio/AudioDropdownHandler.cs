using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioDropdownHandler : MonoBehaviour
{
    private AudioManager audioManager;
    private TMP_Dropdown dropdown;
    private bool isFirstTime;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        isFirstTime = true;
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
        if (isFirstTime)
        {
            isFirstTime = false;
        } else
        {
            // Call the AudioManager to transition to the selected track
            audioManager.TransitionToAudioTrack(trackIndex);
        }
    }
}
