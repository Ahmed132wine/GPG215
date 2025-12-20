using UnityEngine;

public class NormalWeapon : IWeaponStrategy
{
    public void Fire(Transform firePoint)
    {
        GameObject bullet = ObjectPool.Instance.GetBullet();
        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.SetActive(true);
        }
    }
}

public class TripleShotWeapon : IWeaponStrategy
{
    public void Fire(Transform firePoint)
    {
        SpawnBullet(firePoint, 0);

        SpawnBullet(firePoint, -15);

        SpawnBullet(firePoint, 15);
    }

    private void SpawnBullet(Transform origin, float angleOffset)
    {
        GameObject bullet = ObjectPool.Instance.GetBullet();
        if (bullet != null)
        {
            bullet.transform.position = origin.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angleOffset);
            bullet.SetActive(true);
        }
    }
}


