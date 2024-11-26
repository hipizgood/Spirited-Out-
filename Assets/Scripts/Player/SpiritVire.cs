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
        // Линия, это префаб, поэтому задаю объекты на прямую
        firstPlayer = GameObject.Find("FirstPlayer");
        secondPlayer = GameObject.Find("SecondPlayer");

        lR = gameObject.GetComponent<LineRenderer>();

        //Пытаюсь что-то сделать с колайдером
        spiritCollider = gameObject.GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();


    }

    //Позже перенесли все в отдельный метод
    void Update()
    {
        // В настройках линии стоят мировые координаты.


        Vector3[] spiritPoint = new Vector3[2];


        // Тут я приравниваю точки начала и конца, к игрокам. Можно сделать проще, просто прировнять позиции из массива к позиции конретного игрока 
        // UPDATE 14.10.2024 - Пришлось добавить небольшу.ю надбавку по оси Y, так как у персонажей странная привязка
        spiritPoint[0].Set(firstPlayer.transform.position.x, firstPlayer.transform.position.y+1.5f, firstPlayer.transform.position.z);
        spiritPoint[1].Set(secondPlayer.transform.position.x, secondPlayer.transform.position.y + 1.5f, secondPlayer.transform.position.z);
        
        // Задаю положение линии
        lR.SetPositions(spiritPoint);

        // Зададим положение объекта (который содержит и колайдер и Line Render)

       
        gameObject.transform.position = spiritPoint[0]; 

        // Установка размера и полложение коллайдера

        float colliderLeight = Vector3.Distance(firstPlayer.transform.position, secondPlayer.transform.position);
        // Размер коллайдера
        gameObject.transform.localScale = new Vector3(0.1f, 0.1f, colliderLeight-0.5f); 
        // Поворот коллайдера
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
