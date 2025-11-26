using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;
    public float fireRate = 0.3f;
    private float nextFire = 0f;

    public int bulletLevel = 1;
    public float spreadAngle = 15f;
    public float positionOffset = 0.2f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFire)
        {
            Shoot();
            nextFire = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (firePoint == null || ObjectPool.Instance == null) return;

        int bulletsToFire = 1;
        switch (bulletLevel)
        {
            case 1: bulletsToFire = 1; break;
            case 2: bulletsToFire = 3; break;
            case 3: bulletsToFire = 5; break;
        }

        float startAngle = -(spreadAngle * (bulletsToFire - 1) / 2);
        float startOffset = -(positionOffset * (bulletsToFire - 1) / 2);

        for (int i = 0; i < bulletsToFire; i++)
        {
            float angle = startAngle + i * spreadAngle;
            float offsetX = startOffset + i * positionOffset;

            GameObject bullet = ObjectPool.Instance.GetBullet();
            if (bullet != null)
            {
                bullet.transform.position = firePoint.position + new Vector3(offsetX, 0, 0);
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                bullet.SetActive(true);
            }
        }
    }

    public void UpgradeBullet()
    {
        bulletLevel++;
        if (bulletLevel > 3) bulletLevel = 3;
    }
}