using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//� ������ ������� �� ����� ��������� ����� ����������� �������� ����������� �� ������� ���� ������ (������������)
public class Melee_atack : MonoBehaviour
{
    // �������� ������ ��������� ��������� ��� ������ (���������)
    public float atackPower = 10f;
    //� ��� ������ ������������� ����� ������ ����� �� ������ ��������
    public GameManager gameManager;
    //�������� ��������
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
            //�������� ��� ��������� ������������, ��� ��� ������ ������ �����, ����� �������� � contacts!!!!
            Vector3 away = -collision.contacts[0].normal;
            playerRb.AddForce(away*atackPower,ForceMode.Impulse);
            
            gameManager.Damage(1);
            Debug.Log("������ ��������");
        }
    }
}
// ��� ������� ������������� �������� ���������� ������� �������