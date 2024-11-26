using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritVireTest : MonoBehaviour
{
    private GameObject target_1;
    private GameObject target_2;

    private LineRenderer lR;

    // Префаб для коллайдера
    public GameObject colliderPrefab; // Убедитесь, что у вас есть префаб с BoxCollider

    private GameObject colliderInstance; // Экземпляр префаба коллайдера

    void Start()
    {
        target_1 = GameObject.Find("Player");
        target_2 = GameObject.Find("Player_2");

        lR = gameObject.GetComponent<LineRenderer>();

        // Создаем экземпляр коллайдера
        if (colliderPrefab)
        {
            colliderInstance = Instantiate(colliderPrefab, Vector3.zero, Quaternion.identity); // Создает коллайдер из префаба
            colliderInstance.transform.parent = transform; // Устанавливаем родителем
        }
    }

    void Update()
    {
        Vector3[] spiritPoint = new Vector3[2];

        spiritPoint[0] = target_1.transform.position;
        spiritPoint[1] = target_2.transform.position;

        // Устанавливаем позиции линии
        lR.SetPositions(spiritPoint);

        if (colliderInstance)
        {
            // Средняя точка между игроками для коллайдера
            Vector3 center = (spiritPoint[0] + spiritPoint[1]) / 2;

            // Установка позиции и размера коллайдера
            colliderInstance.transform.position = center; // Устанавливаем позицию центра линии

            // Установка размера коллайдера
            float distance = Vector3.Distance(spiritPoint[0], spiritPoint[1]);
            colliderInstance.transform.localScale = new Vector3(0.1f, 0.1f, distance); // Устанавливаем размеры коллайдера
            colliderInstance.transform.LookAt(spiritPoint[1]); // Поворачиваем коллайдер к целевой точке
        }
    }
}