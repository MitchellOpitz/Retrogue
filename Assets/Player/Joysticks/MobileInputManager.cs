using UnityEngine;

public class MobileInputManager : MonoBehaviour
{
    [SerializeField] private GameObject mobileJoysticks;

    private void Start()
    {
        // Determine the current platform's name
        string currentPlatform = GetPlatformName();
        Debug.Log("Playing on: " + currentPlatform);

        // Check if the current platform is a mobile device based on its name
        bool isMobile = IsMobilePlatform(currentPlatform);

        if (isMobile)
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

    private string GetPlatformName()
    {
        // Get the current platform name using the UnityEngine.SystemInfo class
        return SystemInfo.operatingSystem;
    }

    private bool IsMobilePlatform(string platformName)
    {
        // Check if the platform name contains keywords commonly found in mobile devices
        platformName = platformName.ToLower();

        // List of common mobile platform keywords
        string[] mobileKeywords = { "android", "iphone", "ipad", "ios", "mobile" };

        // Check if any of the keywords are found in the platform name
        foreach (string keyword in mobileKeywords)
        {
            if (platformName.Contains(keyword))
            {
                return true;
            }
        }

        return false;
    }
}
