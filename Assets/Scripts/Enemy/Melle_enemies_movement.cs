using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Melle_enemies_movement : MonoBehaviour
{
    public float speed = 5f; // ���������� �������� ������������
    private Rigidbody rb;

    private Vector3 randomPoint;
    private bool isPointReach = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // �� ��������� ���������, ��� ���� ����� ������������� ����� ��������� �����
        //����� ��������� �������� � Start(), � ��� ����� ��������� ������� � ������� ���� ����, ���� ��� �������������.
        //��� ����� ���� ������� ��� ���������� ��������� �����, ����� ��� �������������� ��������� ������,
        //���������� ��������� ��� ������������ ������� �  ������� ����.
        StartCoroutine(GenerateRandomPoint());
    }
    /*����� FixedUpdate:
- ���� ����� ���������� ������ ����, ������� �� ���������� ������.
- ������ ��� ��� ��� ������ ���������� �������� ��������� ����� isPointReach.
- ���� isPointReach ����� false (�� ���� ������ ��� �� ������ ��������� �����), ����� FixedUpdate �������� MoveToRandomPoint.
- ���� MoveToRandomPoint ����� �� ��������� ����������, ��� ��� ������ �� ��� �������� � �����, ���������� ����� ������������ � FixedUpdate, � ������� �����������.

����� �������, ���� ������ �� ��������� ��������� ����� � �� ��������� ���� isPointReach � true, ����� FixedUpdate ����� ���������� �������� MoveToRandomPoint, 
 �������� ������� ��������� � ������ �����������. ��� ������ ������ ��������� �����, ���� ����� ���������� � true, 
 � ��� ������������ ���������� ������ ������ MoveToRandomPoint, �������� �������� (���� ��� �����������) ������������� ����� ��������� �����.
     */
    void FixedUpdate()
    {
        // ��������� �������� �� ����� ��� ���.
        if (!isPointReach)
        {
            // ����� ����� �� ����������, �� ������������ �� �������� � �����. � �� �������� � �����
            MoveToRandomPoint();
        }
    }
    /* ����� MoveToRandomPoint:
- � ���� ������ ������� ����������� ��������, ������ �� ������ ��������� �����. 
  ���� �������� �� �������� (�� ���� ������ �� ������ �����), ����������� ������ �������� � ���� �����.
- ������ ���������� ��������� � ����������� ��������� �����, ���� ������� �� ������ �������� (�� ���� ���� ������ �� �������� ������ � ���� �����).
     */
    void MoveToRandomPoint()
    {
        // ��������, �������� �� �� ��������� �����
        if (Vector3.Distance(transform.position, randomPoint) < 0.5f) // 0.5f ��� �������� ����������
        {
            isPointReach = true; // ���������� ����
            // ���� ������� ��������� ������� �� ������
            return;

        }
        // ���� �� ������� �� ����������, �� ��������� � ����� ������.
        Vector3 moveDirection = (randomPoint - transform.position).normalized;
        rb.MovePosition(transform.position + moveDirection * speed * Time.fixedDeltaTime);
    }
    // ��� �������� ��� ��� �����, � ������ ������� �� 3 �������
    IEnumerator GenerateRandomPoint()
    {
        // � ��������� �� ��������� isPointReach, � ���� ��� ��������� ����� �������� �����
        
        while (true)
        {
            if (isPointReach)
            {
                float randomPosX = Random.Range(-30f, 30f);
                float randomPosZ = Random.Range(50f, 19f);
                // ��������� ������ � ���������� ����������
                randomPoint = new Vector3(randomPosX, transform.position.y, randomPosZ);
                // � ���������� ���� �� ����. ����� �������, �� ������� � ��� ��� ����� ����� ��� �� ����������
                isPointReach = false;
            }
            // � ���� 3 ������� ������ ��� ������������ ����� �����
            yield return new WaitForSeconds(3f); // ���� 3 ������� ����� ���������� ����� �����
        }
    }
}
