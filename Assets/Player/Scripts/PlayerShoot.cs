using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;    // Bullet prefab to instantiate
    public Transform firePoint;        // Reference to the firepoint GameObject
    public float baseShootSpeed = 0.2f; // Interval between shots in seconds
    public int multishot;

    public float shotSpeedMultiplier = 0;
    private float shootInterval;

    private bool isShooting = false;

    private void Start()
    {
        shootInterval = baseShootSpeed;
        shotSpeedMultiplier = 0;
        multishot = 1;
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
        // Calculate the total spread angle
        float angleBetweenShots = 20f;
        float totalSpreadAngle = angleBetweenShots * (multishot - 1); // Adjust this angle as needed

        // Calculate the initial angle, which is half of the total spread angle to ensure it's centered
        float initialAngle = -totalSpreadAngle / 2f;

        // Loop through the number of shots and spawn bullets with adjusted directions
        for (int i = 0; i < multishot; i++)
        {
            // Calculate the angle for the current shot
            float shotAngle = initialAngle + i * angleBetweenShots;

            // Calculate the direction vector based on the shot angle
            Vector3 shotDirection = Quaternion.Euler(0f, 0f, shotAngle) * firePoint.up; // Use firePoint.up

            // Instantiate bullet at the firepoint's position and add the shot direction
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity).transform.up = shotDirection;
        }
    }

    public void UpdateMultiplier(float multiplier)
    {
        shotSpeedMultiplier = multiplier;
        shootInterval = baseShootSpeed * (1 - shotSpeedMultiplier); // 10% faster per upgrade level
    }

}
