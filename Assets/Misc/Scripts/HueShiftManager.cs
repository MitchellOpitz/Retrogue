using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class HueShiftManager : MonoBehaviour
{
    public Slider hueShiftSlider;
    public PostProcessVolume colorGradingVolume; // Reference to the Post-processing volume
    private ColorGrading colorGradingLayer;

    private const string HueShiftPlayerPrefKey = "HueShiftPreference";

    private void Awake()
    {
        // Get the Color Grading layer from the Post-processing volume
        if (colorGradingVolume != null && colorGradingVolume.profile.TryGetSettings<ColorGrading>(out colorGradingLayer))
        {
            // Load the Hue Shift preference from PlayerPrefs or set a default value
            float savedHueShift = PlayerPrefs.GetFloat(HueShiftPlayerPrefKey, 0f);
            hueShiftSlider.value = savedHueShift;

            // Initialize the Color Grading Hue Shift to the loaded value
            colorGradingLayer.hueShift.value = savedHueShift;
        }
    }

    public void UpdateHueShift()
    {
        // Update the Hue Shift value based on the slider's value (mapped to -180 to 180 range)
        if (colorGradingLayer != null)
        {
            float hueShiftValue = hueShiftSlider.value;
            colorGradingLayer.hueShift.value = hueShiftValue;

            // Save the Hue Shift preference to PlayerPrefs
            PlayerPrefs.SetFloat(HueShiftPlayerPrefKey, hueShiftValue);
            PlayerPrefs.Save();
        }
    }
}
