using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//В данном скрипте мы будем создавать метод оказывающий обратное овздействие на твердое тело игрока (отталкивание)
public class Melee_atack : MonoBehaviour
{
    // Временно вводим публичную перменную для тестов (пофиксить)
    public float atackPower = 10f;
    //Я был крайне благоразумным когда развел атаки по разным скриптам
    public GameManager gameManager;
    //Достанем аниматор
    private Animator animator;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        animator = gameManager.GetComponent<Animator>();    
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            //Возможно для улучшения возхдействия, так как объект сложно формы, можно поиграть с contacts!!!!
            Vector3 away = -collision.contacts[0].normal;
            playerRb.AddForce(away*atackPower,ForceMode.Impulse);
            
            gameManager.Damage(1);
            Debug.Log("Игрока толкнули");
        }
    }
}
// Как вариант действительно материал противника сделать упругим