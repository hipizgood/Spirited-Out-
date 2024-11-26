using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_enemies : MonoBehaviour
{
    //���������� ��� �������� ������
    private GameObject playerTarget;


   
    private float maxDistance;
    // ��� �������� ������ ��� ������ ������, �� ��������� � � ���������� ������ � � ������������� ������������ ���������
    public float radius = 25f;

    public bool isAttacking = false;

    private Vector3 attackPosition;
    //������ ������� �������� ��� ���������� ���
    public float forceAttack;
    private Rigidbody rb;

    //��� ��������
    private Animator animator;
    void Start()
    {
        
        rb = GetComponent<Rigidbody>(); 
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {

       
            FindPlayer();
          
    }

    void FindPlayer()
    {
        //���������� ���������� �����, ����� ����� ��� ���������� � ������� 25 ������
        Collider[] players = Physics.OverlapSphere(gameObject.transform.position, radius);
        // �������������� ������ ���������� ��� ������
        playerTarget = null;
        //�������������� ������������ ����������, ��� ����� �������������� ��� ���������(��� ���������� ��������)
        maxDistance = radius;
        
        
      
        // ������ ������ ������� ��� ������� ���������� � ������� �������
        foreach (Collider player in players)
        {
            // ��������� ��� ���� ����������� ������� ��� ����������, � ����� Player
            if (player.gameObject.CompareTag("Player"))

            {
                // ����� � ������ ���������� ����� ��������� ������� � ��������
                float distanceToPlayer = Vector3.Distance(gameObject.transform.position,player.transform.position);

                Vector3 vectorToPlayer =(player.transform.position - gameObject.transform.position).normalized;
                

                //���� ���������� ������ ������������ ���������, �� ���������� ��������� �� ��������� ���� 
                //������� ������� ��� ����� ���� � �����
                if (distanceToPlayer < maxDistance)
                {
                    // ������� � ���������� ����� ��������, ������� ����� �������� ����� �������� �� ���������� �������
                    maxDistance = distanceToPlayer;
                    // ������� � ���������� ��������, ���������� ��������
                    playerTarget = player.gameObject;

                   
                }
                
            }
        }
        //����� �������� , ����� ��������� ������ �������
        if (playerTarget != null)
        {
            
            // � �������� ���������� ������ � ������� �������.
            transform.LookAt(playerTarget.transform.position);
            
            if (!isAttacking) 
            {
                attackPosition = playerTarget.transform.position;

                animator.SetBool("isAtack", true);
                Attack(attackPosition);
            }
            
        }
    }

    void Attack(Vector3 currentTarget)
    {
        //�������� ����  ���� ��� ����� MoveTowards, ������� � ����������� �� ����� ������� � ����������� ���������. � �� ������ ����� �������� ������.
        //��� ��������� � ��������� � ���, ��� ������ ���� �� ��������, ���� �������� ����� ��������.
        // � ����� �� ������ ������������ ������������ ����� ������ � ������� ����� AddForce.
        isAttacking = true;
        // � ���� ������� �� ������� ����������� ����������� ����, ��������� � ������ �������� ���������� �� �������� ����������� � ���������� �� ��������� ����
        Vector3 directionMove = (currentTarget - gameObject.transform.position).normalized;
        //������������ ���� � ����������� ���� ������(����������)
        rb.AddForce(directionMove * forceAttack,ForceMode.Impulse);
        // ����� ������� �����, �� ��������� ���������
        StartCoroutine(ChillTime());
    }

    IEnumerator ChillTime()
    {
        // ��� ��� ������ �� ���� ��������� �����, � ���������� �������� isAttack = false
        // � ���������� ��� ��������� � ���������� � ������ ����� ������ FindPlayer,��������� �������� ����� ������ ��������� ����, �������� ����� Atack
        yield return new WaitForSeconds(5);
        Debug.Log("Cooldown finished. Ready to attack again.");
        isAttacking = false;
    }

}
