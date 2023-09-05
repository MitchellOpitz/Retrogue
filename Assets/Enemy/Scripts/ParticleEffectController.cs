using UnityEngine;

public class ParticleEffectController : MonoBehaviour
{
    private ParticleSystem particleSystem;

    private void Start()
    {
        // Get the ParticleSystem component
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        // Check if all particles have finished their lifetime
        if (!particleSystem.isPlaying)
        {
            // Destroy the GameObject when the particle effect is done
            Destroy(gameObject);
        }
    }
}
