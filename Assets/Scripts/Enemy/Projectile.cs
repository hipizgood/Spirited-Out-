using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float powerBall = 10f;

    // Тут я просто догадался что проджектайлу по хорошему нужен отдельный скрипт
    private GameManager gameManager;

    void Start()
    {
      gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Destroy(gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb =  collision.gameObject.GetComponent<Rigidbody>();
            Vector3 away = -collision.contacts[0].normal;
            rb.AddForce(away * powerBall, ForceMode.Impulse);

            //Наносим 1 урон по игроку
            gameManager.Damage(1);

            Destroy(gameObject);
        }
    }
}
