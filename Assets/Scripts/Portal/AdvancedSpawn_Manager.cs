using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ���� ������ ��������� ��� ������ ����� ������� � �������� �� ����
public class AdvancedSpawn_Manager : MonoBehaviour
{
public GameObject meleeEnemy;
private int playerCount = 0;
private bool isSpawning = true;  // ���� ��� ��������, ���� �� ����� ������

void Start()
{
    InvokeRepeating("SpawnMeleeEnemies", 10, Random.Range(3, 10));
}

void Update()
{
    // ������ �� ����� ������ � Update, ������� ��������� ������� � OnTriggerEnter � OnTriggerExit
}

void SpawnMeleeEnemies()
{
    if (isSpawning)
    {
        Instantiate(meleeEnemy, gameObject.transform.position, meleeEnemy.transform.rotation);
    }
}

private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.CompareTag("Player"))
    {
        playerCount++;
        // ��� ������ ���� ����� ������, ��������� �����
        if (playerCount == 1)
        {
            CancelInvoke("SpawnMeleeEnemies");
            isSpawning = false;
        }
    }
}
// ����� ����������� ����� ����� �� ���������� ��� ���
private void OnTriggerExit(Collider other)
{
    if (other.gameObject.CompareTag("Player"))
    {
        playerCount--;
        // ���� ��� ������ �����, ���������� �����
        if ((playerCount == 0) || (playerCount == 1)) 
        {
            isSpawning = true;
            InvokeRepeating("SpawnMeleeEnemies", 10, Random.Range(3, 10));
        }
    }
}
}
