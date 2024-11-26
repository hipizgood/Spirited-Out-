using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Fly_enemies : MonoBehaviour
{
    private GameObject[] players = new GameObject[2];
    private int target; // ������ �������� ������
    private float switchTime = 2f; // �����, ����� ������� ����������� ����
    private float timer; // ������ ��� ������������ �������

    // ���������� ��� ������������
    public GameObject projectail;

    //C������� ��������
    public float speed = 10f;



    // ���������� ��� �������� ��������� �����
    private Vector3 randomPoint;

    private bool isPointReach = false;
    private float nextDash;

    // ���������� ��� ���������
    private Animator animator;
    public GameObject vfx;

    // ��������� ������� ��������
    private BoxCollider movementArea;

    void Start()
    {
        players[0] = GameObject.Find("FirstPlayer");
        players[1] = GameObject.Find("SecondPlayer");

        // �������� �������� ������� ������ � ������
        target = Random.Range(0, players.Length);
        // �������� ��������
        animator = GetComponent<Animator>();

        movementArea = GameObject.Find("Movement Area").GetComponent<BoxCollider>();

    }

    void Update()
    {

        StartCoroutine(GenerateRandomPoint());

        FindTarget();

        if (Vector3.Distance(transform.position, randomPoint) < 0.1f)
        {
            animator.SetBool("isAttack", true);
            MoveOn();
            Shoot();
        }

    }

    IEnumerator GenerateRandomPoint()
    {



        if (isPointReach == false)
        {
            float randomPosX = Random.Range(movementArea.bounds.min.x, movementArea.bounds.max.x);
            float randomPosZ = Random.Range(movementArea.bounds.min.z, movementArea.bounds.max.z);

            // �� ����� ����� ����������
            randomPoint = new Vector3(randomPosX, transform.position.y, randomPosZ);
            isPointReach = true;

            Debug.Log("��������� ������������� �������: " + randomPoint);
        }
        animator.SetBool("isAttack", false);
        transform.position = Vector3.MoveTowards(gameObject.transform.position, randomPoint, speed * Time.deltaTime);

        yield return new WaitForSeconds(7);


    }

    void FindTarget()
    {
        if (players[0] == null || players[1] == null)
        {
            return;
        }

        // ��������� ������
        timer += Time.deltaTime;

        // ���������, ����� �� ����������� ����
        if (timer >= switchTime)
        {
            target = Random.Range(0, players.Length);
            timer = 0; // ���������� ������
        }

        // ������� �� ���������� ������
        if (players[target] != null)
        {
            Vector3 lookForPlayer = players[target].transform.position;
            gameObject.transform.LookAt(lookForPlayer);

            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }

    void MoveOn()
    {

        isPointReach = false;

    }

    void Shoot()
    {

        Vector3 projectailPointSpawn = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z + 1);
        Instantiate(projectail, projectailPointSpawn, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Spirit_Vire"))
        {
            Instantiate(vfx, transform.position, transform.rotation);
        }
    }
}





