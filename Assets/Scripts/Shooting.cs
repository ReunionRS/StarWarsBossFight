using UnityEngine;
using System.Collections;

public class ShootingDual : MonoBehaviour
{
    // Пистолеты
    public Transform rightHand;
    public Transform leftHand;

    // Префабы пуль
    public GameObject bulletPrefab;

    // Параметры стрельбы
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f; // Скорость стрельбы
    private float nextFireTime = 0f; 

    // Отдача оружия
    public float recoilForce = 2f;
    public float recoilSmoothness = 10f;


    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    // Метод для стрельбы
    private void Shoot()
    {
        // Стрельба из правой руки
        ShootFromHand(rightHand);

        // Стрельба из левой руки
        ShootFromHand(leftHand);

        // Отдача оружия
        transform.Rotate(-Vector3.up * recoilForce, Space.Self);
        StartCoroutine(RecoilSmooth());

    }

    // Стрельба из одной руки
    private void ShootFromHand(Transform hand)
    {
        // Создаем пулю
        GameObject bullet = Instantiate(bulletPrefab, hand.position, hand.rotation);

        // Добавляем силу пуле
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(hand.forward * bulletSpeed, ForceMode.Impulse);
    }

    // Сглаживание отдачи
    private IEnumerator RecoilSmooth()
    {
        float currentRecoil = recoilForce;
        while (currentRecoil > 0)
        {
            currentRecoil -= recoilSmoothness * Time.deltaTime;
            transform.Rotate(Vector3.up * currentRecoil, Space.Self);
            yield return null;
        }
    }
}