using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpawn_Manager : MonoBehaviour
{
    public GameObject flyEnemy;
    void Start()
    {
        InvokeRepeating("FlyEnemySpawner", 6, Random.Range(5, 20));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FlyEnemySpawner()
    {
        Instantiate(flyEnemy, gameObject.transform.position, flyEnemy.transform.rotation);
    }
}
