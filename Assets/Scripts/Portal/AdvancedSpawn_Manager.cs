using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Этот скрипт учитывает что игроки могут входить и выходить их поля
public class AdvancedSpawn_Manager : MonoBehaviour
{
public GameObject meleeEnemy;
private int playerCount = 0;
private bool isSpawning = true;  // Флаг для проверки, идет ли спавн врагов

void Start()
{
    InvokeRepeating("SpawnMeleeEnemies", 10, Random.Range(3, 10));
}

void Update()
{
    // Ничего не нужно делать в Update, следует управлять спавном в OnTriggerEnter и OnTriggerExit
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
        // Как только один игрок входит, отключаем спавн
        if (playerCount == 1)
        {
            CancelInvoke("SpawnMeleeEnemies");
            isSpawning = false;
        }
    }
}
// Метод проверяющий вышел игрок из коллайдера или нет
private void OnTriggerExit(Collider other)
{
    if (other.gameObject.CompareTag("Player"))
    {
        playerCount--;
        // Если все игроки вышли, продолжаем спавн
        if ((playerCount == 0) || (playerCount == 1)) 
        {
            isSpawning = true;
            InvokeRepeating("SpawnMeleeEnemies", 10, Random.Range(3, 10));
        }
    }
}
}
