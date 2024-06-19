using UnityEngine;
using System.Collections;

public class Aiming : MonoBehaviour
{
    // Ссылка на камеру
    public Transform cameraTransform;

    // Параметры плавного приближения
    public float aimDistance = 2f; // Расстояние прицеливания
    public float aimSpeed = 5f; // Скорость плавного приближения

    // Параметры анимации
    private Animator animator;
    private bool aiming = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Проверяем, нажата ли кнопка прицеливания (например, правая кнопка мыши)
        if (Input.GetMouseButton(1))
        {
            Aim();
        }
        else
        {
            UnAim();
        }
    }

    // Прицеливание
    private void Aim()
    {
        if (!aiming)
        {
            aiming = true;

            // Плавное приближение камеры
            StartCoroutine(SmoothZoom(aimDistance));

            // Изменение анимации
            animator.SetBool("Aiming", true);
        }
    }

    // Отмена прицеливания
    private void UnAim()
    {
        if (aiming)
        {
            aiming = false;

            // Плавное отдаление камеры
            StartCoroutine(SmoothZoom(0f));

            // Изменение анимации
            animator.SetBool("Aiming", false);
        }
    }

    // Плавное приближение/отдаление камеры
    private IEnumerator SmoothZoom(float targetDistance)
    {
        float currentDistance = Vector3.Distance(transform.position, cameraTransform.position);

        while (Mathf.Abs(currentDistance - targetDistance) > 0.1f)
        {
            currentDistance = Mathf.Lerp(currentDistance, targetDistance, aimSpeed * Time.deltaTime);
            cameraTransform.position = transform.position + transform.forward * currentDistance;
            yield return null;
        }
    }
}