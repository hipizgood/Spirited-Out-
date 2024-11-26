using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Camera : MonoBehaviour
{
    public GameObject firstPlayer;
    public GameObject secondPlayer;

    [SerializeField] private float backDistance = 30f;
    
    //���� ����� ����������!
    public GameObject spiritVire;
    public float spiritVireDistance=20.0f;


    private SecondPlayerControl secondPlayerControl;
    // ��������
    private GameObject lineInScene;


    [SerializeField] private float cameraY;

    // ������� ��������� � ��������� ������������� ����
    private AudioSource audioSource;
    public AudioClip spiritVireSound;
   
    void Start()
    {
        secondPlayerControl = GameObject.Find("SecondPlayer").GetComponent<SecondPlayerControl>();
        
        audioSource = GetComponent<AudioSource>();
        
    }


    private void Update()
    {

        DistanceBetweenPlayer();

        float dist = Vector3.Distance(firstPlayer.transform.position, secondPlayer.transform.position);
        // ��� ������ ��� ��������� � ������ �����
        // � ������ ��������, �� ������, ����� ���� ������ ��������� � ����� ������� �������
        if ((dist<=spiritVireDistance) && (secondPlayerControl.spiritVireExist==true))
        {
            Instantiate(spiritVire);
            audioSource.PlayOneShot(spiritVireSound, 0.05f);
        } else if (dist > spiritVireDistance)
        {
            
            lineInScene = GameObject.FindWithTag("Spirit_Vire");
            Destroy(lineInScene);
            secondPlayerControl.spiritVireExist = true;
        }


    }
    // ����� �������� ����� �������� �������� Update. ����� �� ���� �������
    void LateUpdate()
    {

        CameraPosition();

    }

    // ����� ������� �������� �� �������� ��������� ����� ��������, � ���� ��� ������ ���������� ���� �� �����, �� �� ������������� �������
    private void DistanceBetweenPlayer()
    {

        // ������������� ��������� ����� ��������
         float dist = Vector3.Distance(firstPlayer.transform.position, secondPlayer.transform.position);

        // �������, ��� ������� ������ ������������� �������
        if (dist > backDistance)
        {
            firstPlayer.transform.position = new Vector3(secondPlayer.transform.position.x + 20, secondPlayer.transform.position.y, secondPlayer.transform.position.z);
        }
    }

    private void CameraPosition()
    {
        // ��������� ��� ����, ����� ���������� ����������� ������ ������ � ����������� �� ����������� ������� ���� �� �����
        float heading = Vector3.Distance(firstPlayer.transform.position, secondPlayer.transform.position);

        // ������ �������� �������, ��� ����������� ������ ������������ ������� 2-� �������
        float cameraPositionX = ((firstPlayer.transform.position.x + secondPlayer.transform.position.x) / 2);
        float cameraPositionY = ((firstPlayer.transform.position.y + secondPlayer.transform.position.y) / 2);
        float cameraPositionZ = ((firstPlayer.transform.position.z + secondPlayer.transform.position.z) / 2);
        // ����� ��� ���������� ��������� ������� ������, ������������� ��������� �������� �������, � ���������� ����� �������� + ��������� �� ��� Y
        Vector3 cameraPosition = new Vector3(cameraPositionX, heading + cameraY , cameraPositionZ-10);

        // ��������� ������� ������
        gameObject.transform.position = cameraPosition;
    }

    
}
 