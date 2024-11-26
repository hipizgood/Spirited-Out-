using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TremblingFlyingEnemy : MonoBehaviour
{
    public float amplitude = 0.5f; // Высота покачивания
    public float speed = 1f; // Скорость покачивания

    private float originalY; // Исходное значение Y
    private float offsetY; // Смещение по Y

    void Start()
    {
        // Сохраняем исходное значение Y
        originalY = transform.position.y;
    }

    void Update()
    {
        // Обновляем смещение с учетом времени
        offsetY = amplitude * Mathf.PingPong(Time.time * speed, 1);

        // Устанавливаем новую позицию
        transform.position = new Vector3(transform.position.x, originalY + offsetY, transform.position.z);
    }
}
