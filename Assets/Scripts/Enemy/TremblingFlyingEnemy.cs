using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TremblingFlyingEnemy : MonoBehaviour
{
    public float amplitude = 0.5f; // ������ �����������
    public float speed = 1f; // �������� �����������

    private float originalY; // �������� �������� Y
    private float offsetY; // �������� �� Y

    void Start()
    {
        // ��������� �������� �������� Y
        originalY = transform.position.y;
    }

    void Update()
    {
        // ��������� �������� � ������ �������
        offsetY = amplitude * Mathf.PingPong(Time.time * speed, 1);

        // ������������� ����� �������
        transform.position = new Vector3(transform.position.x, originalY + offsetY, transform.position.z);
    }
}
