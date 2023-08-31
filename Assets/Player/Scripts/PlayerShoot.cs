using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;    // Bullet prefab to instantiate
    public Transform firePoint;        // Reference to the firepoint GameObject
    public float baseShootSpeed = 0.2f; // Interval between shots in seconds
   
    public float shootMultiplier = 0;
    private float shootInterval;

    private bool isShooting = false;

    private void Start()
    {
        shootInterval = baseShootSpeed;
        shootMultiplier = 0;
    }

    private void Update()
    {
        // Rotate player towards the mouse cursor
        RotatePlayerTowardsMouse();

        // Check for left mouse button press and release
        if (Input.GetButtonDown("Fire1"))
        {
            StartShooting();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopShooting();
        }
    }

    private void RotatePlayerTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // Ensure the same Z position as the player
        Vector3 lookDirection = mousePosition - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f); // Adjust rotation by -90 degrees
    }

    private void StartShooting()
    {
        if (!isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootContinuously());
        }
    }

    private void StopShooting()
    {
        if (isShooting)
        {
            isShooting = false;
            StopCoroutine(ShootContinuously());
        }
    }

    private IEnumerator ShootContinuously()
    {
        while (isShooting)
        {
            Shoot();
            yield return new WaitForSeconds(shootInterval);
        }
    }

    private void Shoot()
    {
        // Instantiate bullet at the firepoint's position and rotation
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void UpdateMultiplier(float multiplier)
    {
        shootMultiplier = multiplier;
        shootInterval = baseShootSpeed * (1 - shootMultiplier); // 10% faster per upgrade level
    }

}
