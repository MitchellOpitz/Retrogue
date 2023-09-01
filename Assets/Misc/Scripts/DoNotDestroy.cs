using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    private void Awake()
    {
        // Prevent the Particle System GameObject from being destroyed on scene changes
        DontDestroyOnLoad(gameObject);
    }
}
