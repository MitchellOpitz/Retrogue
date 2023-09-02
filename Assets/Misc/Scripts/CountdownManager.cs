using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class CountdownManager : MonoBehaviour
{
    public float countdownDuration = 5f;
    public TextMeshProUGUI countdownText;

    private Color originalColor;
    private PlayerManager playerManager;

    private void Start()
    {
        originalColor = countdownText.color; // Store the original color
        playerManager = FindObjectOfType<PlayerManager>();
        StartCountdown();
    }

    public void StartCountdown()
    {
        // Debug.Log("Starting countdown.");
        playerManager.ActivateRegen();
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        float timer = countdownDuration;
        int previousSecond = -1;

        while (timer > 0)
        {
            int seconds = Mathf.CeilToInt(timer);
            if (seconds != previousSecond)
            {
                countdownText.text = seconds > 3 ? "" : seconds.ToString();
                previousSecond = seconds;

                StartCoroutine(FadeTextAlpha()); // Start the fading effect
            }

            timer -= Time.deltaTime;
            yield return null;
        }

        countdownText.text = "Wave Start!";
        StartCoroutine(FadeTextAlpha()); // Start the fading effect
        yield return new WaitForSeconds(1f);

        countdownText.text = "";
        countdownText.color = originalColor; // Restore the original color

        FindObjectOfType<EnemySpawner>().StartSpawner();

    }

    private IEnumerator FadeTextAlpha()
    {
        float elapsedTime = 0f;
        float fadeDuration = .75f; // Duration of fading effect

        Color targetColor = countdownText.color;
        targetColor.a = 0f; // Set target alpha to 0

        while (elapsedTime < fadeDuration)
        {
            countdownText.color = Color.Lerp(originalColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        countdownText.color = targetColor; // Ensure alpha is exactly 0 at the end
    }
}
