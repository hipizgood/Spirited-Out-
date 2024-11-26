using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Camera : MonoBehaviour
{
    public GameObject firstPlayer;
    public GameObject secondPlayer;

    [SerializeField] private float backDistance = 30f;
    
    //Ввел новые переменные!
    public GameObject spiritVire;
    public float spiritVireDistance=20.0f;


    private SecondPlayerControl secondPlayerControl;
    // Временно
    private GameObject lineInScene;


    [SerializeField] private float cameraY;

    // Эффекты появления и исчезания спиритической нити
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
        // Тут описан код созхдания и смерти линии
        // в первой проверке, мы чекаем, чтобы была нужная дистанция и нужое булевое условие
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
    // Метод вызываем после основных действий Update. Чтобы не было залипов
    void LateUpdate()
    {

        CameraPosition();

    }

    // Метод который отвевает за контроль дистанции между игроками, и если они сильно отдаляются друг от друга, то их телепортирует обратно
    private void DistanceBetweenPlayer()
    {

        // Высчитывается дистанция между игроками
         float dist = Vector3.Distance(firstPlayer.transform.position, secondPlayer.transform.position);

        // Условие, при котором игрока телепортирует обратно
        if (dist > backDistance)
        {
            firstPlayer.transform.position = new Vector3(secondPlayer.transform.position.x + 20, secondPlayer.transform.position.y, secondPlayer.transform.position.z);
        }
    }

    private void CameraPosition()
    {
        // Дистанция для того, чтобы равномерно увеличивать подъем камеры в зависимости от удаленности игроков друг от друга
        float heading = Vector3.Distance(firstPlayer.transform.position, secondPlayer.transform.position);

        // Расчет серидины вектора, для отцентровки камеры относительно позиции 2-х игроков
        float cameraPositionX = ((firstPlayer.transform.position.x + secondPlayer.transform.position.x) / 2);
        float cameraPositionY = ((firstPlayer.transform.position.y + secondPlayer.transform.position.y) / 2);
        float cameraPositionZ = ((firstPlayer.transform.position.z + secondPlayer.transform.position.z) / 2);
        // Место где происходит финальная просчет камеры, отсносительно координат середины вектора, и расстояния между игроками + отдаление по оси Y
        Vector3 cameraPosition = new Vector3(cameraPositionX, heading + cameraY , cameraPositionZ-10);

        // настройка позиции камеры
        gameObject.transform.position = cameraPosition;
    }

    
}
 