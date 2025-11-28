using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    [Header("Base Stats")]
    public Transform firePoint;
    public float normalFireRate = 0.3f;

    [Header("Overdrive Stats ")]
    public float overdriveFireRate = 0.1f;
    public float overdriveDuration = 5f;

    private bool isOverdrive = false;
    private float nextFire = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFire)
        {
            Shoot();
            float rate = isOverdrive ? overdriveFireRate : normalFireRate;
            nextFire = Time.time + rate;
        }
    }

    void Shoot()
    {
        if (firePoint == null || ObjectPool.Instance == null) return;

        if (isOverdrive)
        {
          
            SpawnBullet(0);
            SpawnBullet(15);
            SpawnBullet(-15);
        }
        else
        {
            
            SpawnBullet(0);
        }

        
        AudioManager.Instance?.PlayPlayerShoot();
    }

    void SpawnBullet(float angle)
    {
        GameObject bullet = ObjectPool.Instance.GetBullet();
        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            bullet.SetActive(true);
        }
    }

    public void ActivateOverdrive()
    {
        StopCoroutine("OverdriveRoutine");
        StartCoroutine("OverdriveRoutine");
    }

    IEnumerator OverdriveRoutine()
    {
        isOverdrive = true;

        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(overdriveDuration);

        isOverdrive = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        Debug.Log("Overdrive Ended.");
    }
}