using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritVireTest : MonoBehaviour
{
    private GameObject target_1;
    private GameObject target_2;

    private LineRenderer lR;

    // ������ ��� ����������
    public GameObject colliderPrefab; // ���������, ��� � ��� ���� ������ � BoxCollider

    private GameObject colliderInstance; // ��������� ������� ����������

    void Start()
    {
        target_1 = GameObject.Find("Player");
        target_2 = GameObject.Find("Player_2");

        lR = gameObject.GetComponent<LineRenderer>();

        // ������� ��������� ����������
        if (colliderPrefab)
        {
            colliderInstance = Instantiate(colliderPrefab, Vector3.zero, Quaternion.identity); // ������� ��������� �� �������
            colliderInstance.transform.parent = transform; // ������������� ���������
        }
    }

    void Update()
    {
        Vector3[] spiritPoint = new Vector3[2];

        spiritPoint[0] = target_1.transform.position;
        spiritPoint[1] = target_2.transform.position;

        // ������������� ������� �����
        lR.SetPositions(spiritPoint);

        if (colliderInstance)
        {
            // ������� ����� ����� �������� ��� ����������
            Vector3 center = (spiritPoint[0] + spiritPoint[1]) / 2;

            // ��������� ������� � ������� ����������
            colliderInstance.transform.position = center; // ������������� ������� ������ �����

            // ��������� ������� ����������
            float distance = Vector3.Distance(spiritPoint[0], spiritPoint[1]);
            colliderInstance.transform.localScale = new Vector3(0.1f, 0.1f, distance); // ������������� ������� ����������
            colliderInstance.transform.LookAt(spiritPoint[1]); // ������������ ��������� � ������� �����
        }
    }
}