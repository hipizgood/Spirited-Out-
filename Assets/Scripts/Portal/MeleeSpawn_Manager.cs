using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSpawn_Manager : MonoBehaviour
{
    public GameObject meleeEnemy;


    void Start()
    {
        InvokeRepeating("SpawnMeleeEnemies", 10, Random.Range(7, 21));
    }

    void Update()
    {
        // ������ �� ����� ������ � Update, ������� ��������� ������� � OnTriggerEnter � OnTriggerExit
    }

    void SpawnMeleeEnemies()
    {

        Instantiate(meleeEnemy, gameObject.transform.position, meleeEnemy.transform.rotation);

    }




}
