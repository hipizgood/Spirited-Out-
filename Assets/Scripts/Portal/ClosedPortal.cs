using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
// ����������� �������� ��� ��� � ��� ���, ���� ���� ����� ����� ��������� ������������ � ���������� ����� �������
public class ClosedPortal : MonoBehaviour
{
    public KeyCode[] firstPlayerKey = new KeyCode[3];
    public KeyCode[] secondPlayerKey = new KeyCode[3];

    private int firstRandomKey;
    private int secondRandomKey;

    private Coroutine exorcismTime;


    //���������� ��� ����������� ������ ������
    private MeleeSpawn_Manager melleSpawn_manager;

    //����� ������� ��� �������
    public TextMeshProUGUI firstPlayerButton;
    public TextMeshProUGUI secondPlayerButton;

    //������� ������������
    public GameObject vfx;
   
    void Start()
    {
        melleSpawn_manager = GetComponent<MeleeSpawn_Manager>();
        // ���������� ������� ������
        firstPlayerButton.text = "";
        secondPlayerButton.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (exorcismTime != null)
        {
            if (Input.GetKey(firstPlayerKey[firstRandomKey]) && Input.GetKey(secondPlayerKey[secondRandomKey]))
            {

                
                    // ���� ��� ������ ������ ������ �������
                    Debug.Log("��� ������ ������ ���������� ������� ������������!");
                    StopCoroutine(exorcismTime);
                    // ��������� �������� ����������
                    PerformExorcismAction();
                
            }
            
        }
    }



    IEnumerator ExorcismTiming()
    {
        float timeLimit = 10f; // ����� �������, �� ������� ����� ������ �������
        float elapsedTime = 0f;

        while (elapsedTime < timeLimit)
        {
            elapsedTime += Time.deltaTime;
            yield return null; // ���� ���������� �����
        }

        // ���� ������� �����, ���������� ��������
        Debug.Log("����� �����! ��������� ����� ������...");
        exorcismTime = null;

        // �������� Exorcism ��� ��������� ����� ������
        Exorcism();

    }
    // ������ �������� �� ��, ����� ������ ��� � �������
    private void OnTriggerEnter(Collider other)
    {
        // �������� ������������ ����� ������� � ��� �������������� �������� (������ �� �� ����������� � ��������� ��� � ������ ��������)
        if (other.gameObject.CompareTag("Player"))
        {
            // ����� ��� ����� �������������� � ���, ��� ��������� � ��� �� ��������
            if (exorcismTime == null)
            {
                Exorcism();
            }

        }
    }
    // � ��� ��������� ���� ����� ���-�� ����� �������� ������
    private void OnTriggerExit(Collider other)
    {
        // ��� �� ����� ��� ��� �����, ������� �������� ���
        if (other.GetComponent<FirstPlayerControl>() || other.GetComponent<SecondPlayerControl>())
        {
            // ����� ������ ���� �� �������� ���������, ������� ����������� ������
            if (exorcismTime != null)
            {
                // �� � ��� ��� ��������
                StopCoroutine(exorcismTime);
                exorcismTime = null;
                Debug.Log("����� ����� �� ����");
                

                // ������� ����� ��� ������ �� ��������
                firstPlayerButton.text = "";
                secondPlayerButton.text = "";

            }
        }
    }
    // � ���� ������ 
    void Exorcism()
    {
        Collider[] players = Physics.OverlapSphere(transform.position, 12);

        bool firstPlayerRb = false;
        bool secondPlayerRb = false;
        //�������� ���������� �������, ��� ��� ��� ��������� � ����� �������
        foreach (Collider player in players)
        {
            if (player.CompareTag("Player"))
            {
                if (player.gameObject.name == "FirstPlayer")
                {
                    firstPlayerRb = true;
                }
                else if (player.gameObject.name == "SecondPlayer")
                {
                    secondPlayerRb = true;
                }

            }

        }

        if (firstPlayerRb && secondPlayerRb)
        {


            StartExorcism();

            Debug.Log("��� ������ ��������� � ����!");

            


        }
        //�������� ��� �������� ������
        if (!firstPlayerRb && !secondPlayerRb)
        {
            // ��� ������ ���� ������ ���������, �����, ������� ���� ���-�� ��������� ���� ���!!!!!!!!!
            
            Debug.Log("��� ������ ����� �� ����!");

            
            StopCoroutine(exorcismTime);

        }
    }

    void StartExorcism()
    {
        //��������� �������� ����� ������
        firstRandomKey = Random.Range(0, firstPlayerKey.Length);
        secondRandomKey = Random.Range(0, secondPlayerKey.Length);

        //��������� � ������������ ���� ������ ��� ������ 2      
        string secondKeyText = (secondRandomKey + 1).ToString();
        // � ���� ������������ ���� ��� ������� ��� ������ �� ������
        firstPlayerButton.text = "PUSHHH :" + firstPlayerKey[firstRandomKey];
        secondPlayerButton.text = "PUSHHH :" + secondKeyText;


        exorcismTime = StartCoroutine(ExorcismTiming());
    }

    void PerformExorcismAction()
    {
        // ������ ���������� ����������
        Destroy(gameObject);
        Debug.Log("��������� �������� �������!");
        exorcismTime = null; // ���������� �������� ����� ��������� ����������
        Instantiate(vfx,transform.position, transform.rotation);
        firstPlayerButton.text = "";
        secondPlayerButton.text = "";
    }

    // �������� ����� �������� ��� ��� ��� �������� ����, ������������ ������� ���� ������� ������ �� ��
}

