using UnityEngine;

public class MobileInputManager : MonoBehaviour
{
    [SerializeField] private GameObject mobileJoysticks;

    private void Start()
    {
        // Check if the current platform is a mobile device
        if (Application.isMobilePlatform)
        {
            // Enable the mobile joysticks
            mobileJoysticks.SetActive(true);
        }
        else
        {
            // Disable the mobile joysticks if not on a mobile device
            mobileJoysticks.SetActive(false);
        }
    }
}
