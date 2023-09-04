using UnityEngine;
using System.Collections;

public class MuzzleFlash : MonoBehaviour
{
    public SpriteRenderer muzzleFlashRenderer; // Reference to the SpriteRenderer for the muzzle flash
    public float flashDuration = 0.1f; // Duration in seconds for the muzzle flash
    public float fadeSpeed = 5f; // Speed at which the muzzle flash fades out

    // This function is called when the player shoots to show the muzzle flash.
    public void ShowMuzzleFlash()
    {
        // Set the alpha (opacity) of the muzzle flash sprite to 1 (fully visible)
        Color flashColor = muzzleFlashRenderer.color;
        flashColor.a = 1f;
        muzzleFlashRenderer.color = flashColor;

        // Start a coroutine to fade out the muzzle flash
        StartCoroutine(FadeMuzzleFlash());
    }

    // Coroutine to smoothly fade out the muzzle flash
    private IEnumerator FadeMuzzleFlash()
    {
        // Calculate the step for fading based on fadeSpeed
        float step = fadeSpeed * Time.deltaTime;

        // Gradually reduce the alpha (opacity) of the muzzle flash sprite to 0 (invisible)
        while (muzzleFlashRenderer.color.a > 0)
        {
            Color flashColor = muzzleFlashRenderer.color;
            flashColor.a -= step;
            muzzleFlashRenderer.color = flashColor;

            yield return null;
        }

        // Ensure the alpha is exactly 0 (to avoid rounding errors)
        Color finalColor = muzzleFlashRenderer.color;
        finalColor.a = 0;
        muzzleFlashRenderer.color = finalColor;
    }
}
