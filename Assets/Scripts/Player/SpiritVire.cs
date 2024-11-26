using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpiritVire : MonoBehaviour
{
    private GameObject firstPlayer;
    private GameObject secondPlayer;

    private LineRenderer lR;

    private Vector3[] spiritPoint;

    private BoxCollider spiritCollider;

    private AudioSource audioSource;
    public AudioClip damageReact;
    void Start()
    {
        // �����, ��� ������, ������� ����� ������� �� ������
        firstPlayer = GameObject.Find("FirstPlayer");
        secondPlayer = GameObject.Find("SecondPlayer");

        lR = gameObject.GetComponent<LineRenderer>();

        //������� ���-�� ������� � ����������
        spiritCollider = gameObject.GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();


    }

    //����� ��������� ��� � ��������� �����
    void Update()
    {
        // � ���������� ����� ����� ������� ����������.


        Vector3[] spiritPoint = new Vector3[2];


        // ��� � ����������� ����� ������ � �����, � �������. ����� ������� �����, ������ ���������� ������� �� ������� � ������� ���������� ������ 
        // UPDATE 14.10.2024 - �������� �������� ��������.� �������� �� ��� Y, ��� ��� � ���������� �������� ��������
        spiritPoint[0].Set(firstPlayer.transform.position.x, firstPlayer.transform.position.y+1.5f, firstPlayer.transform.position.z);
        spiritPoint[1].Set(secondPlayer.transform.position.x, secondPlayer.transform.position.y + 1.5f, secondPlayer.transform.position.z);
        
        // ����� ��������� �����
        lR.SetPositions(spiritPoint);

        // ������� ��������� ������� (������� �������� � �������� � Line Render)

       
        gameObject.transform.position = spiritPoint[0]; 

        // ��������� ������� � ���������� ����������

        float colliderLeight = Vector3.Distance(firstPlayer.transform.position, secondPlayer.transform.position);
        // ������ ����������
        gameObject.transform.localScale = new Vector3(0.1f, 0.1f, colliderLeight-0.5f); 
        // ������� ����������
        gameObject.transform.LookAt(secondPlayer.transform.position);

       
       

    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            audioSource.PlayOneShot(damageReact, 0.05f);

            Destroy(other.gameObject);
        }
    }


}
