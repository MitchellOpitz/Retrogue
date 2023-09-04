using UnityEngine;

public class PauseToggle : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.P))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        bool isPaused = !pauseMenuUI.activeSelf;
        pauseMenuUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
