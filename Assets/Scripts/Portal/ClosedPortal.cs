using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
// генеральная проблема все еще в том что, если один игрок вышел коррутина продолжается и генерирует новые клавиши
public class ClosedPortal : MonoBehaviour
{
    public KeyCode[] firstPlayerKey = new KeyCode[3];
    public KeyCode[] secondPlayerKey = new KeyCode[3];

    private int firstRandomKey;
    private int secondRandomKey;

    private Coroutine exorcismTime;


    //переменная для отколючения спавна врагов
    private MeleeSpawn_Manager melleSpawn_manager;

    //Текст клавишь для игроков
    public TextMeshProUGUI firstPlayerButton;
    public TextMeshProUGUI secondPlayerButton;

    //добавим спецэффектов
    public GameObject vfx;
   
    void Start()
    {
        melleSpawn_manager = GetComponent<MeleeSpawn_Manager>();
        // Изначально очищаем тексты
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

                
                    // Если оба игрока нажали нужные клавиши
                    Debug.Log("Оба игрока нажали правильные клавиши одновременно!");
                    StopCoroutine(exorcismTime);
                    // Выполните действие экзорцизма
                    PerformExorcismAction();
                
            }
            
        }
    }



    IEnumerator ExorcismTiming()
    {
        float timeLimit = 10f; // Лимит времени, за которое нужно нажать клавиши
        float elapsedTime = 0f;

        while (elapsedTime < timeLimit)
        {
            elapsedTime += Time.deltaTime;
            yield return null; // Ждем следующего кадра
        }

        // Если времени вышло, сбрасываем корутину
        Debug.Log("Время вышло! Генерация новых клавиш...");
        exorcismTime = null;

        // Вызываем Exorcism для генерации новых клавиш
        Exorcism();

    }
    // Делаем проверку на то, вошли игроки оба в триггер
    private void OnTriggerEnter(Collider other)
    {
        // Проверку осуществляем через наличие у них соотвествующих скриптов (почему то не срабатывает с проверкой как в выходе Триггера)
        if (other.gameObject.CompareTag("Player"))
        {
            // затем нам важно удостовериться в том, что коррутина у нас не запущена
            if (exorcismTime == null)
            {
                Exorcism();
            }

        }
    }
    // А тут проверяем если вдруг кто-то решил покинуть здание
    private void OnTriggerExit(Collider other)
    {
        // нам не важно кто это убдет, поэтмоу проверка или
        if (other.GetComponent<FirstPlayerControl>() || other.GetComponent<SecondPlayerControl>())
        {
            // Также чекаем была ли запущена коррутина, которая отсчитывает таймер
            if (exorcismTime != null)
            {
                // Ну и тут все обнуляем
                StopCoroutine(exorcismTime);
                exorcismTime = null;
                Debug.Log("Игрок вышел из зоны");
                

                // Очищаем текст при выходе из триггера
                firstPlayerButton.text = "";
                secondPlayerButton.text = "";

            }
        }
    }
    // В этом методе 
    void Exorcism()
    {
        Collider[] players = Physics.OverlapSphere(transform.position, 12);

        bool firstPlayerRb = false;
        bool secondPlayerRb = false;
        //Проверка коллайдера игроков, что они оба находятся в нужно радиусе
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

            Debug.Log("Оба игрока находятся в зоне!");

            


        }
        //Возможно эта проверка лишняя
        if (!firstPlayerRb && !secondPlayerRb)
        {
            // тут раньше была другая коррутина, общая, поэтому если что-то сломается этот тут!!!!!!!!!
            
            Debug.Log("Оба игрока вышли из зоны!");

            
            StopCoroutine(exorcismTime);

        }
    }

    void StartExorcism()
    {
        //Сохраняем значение новых клавиш
        firstRandomKey = Random.Range(0, firstPlayerKey.Length);
        secondRandomKey = Random.Range(0, secondPlayerKey.Length);

        //Переводим в человеческий язык кнопку для игрока 2      
        string secondKeyText = (secondRandomKey + 1).ToString();
        // И этот человеческий язык уже выводим для игрока на экране
        firstPlayerButton.text = "PUSHHH :" + firstPlayerKey[firstRandomKey];
        secondPlayerButton.text = "PUSHHH :" + secondKeyText;


        exorcismTime = StartCoroutine(ExorcismTiming());
    }

    void PerformExorcismAction()
    {
        // Логика выполнения экзорцизма
        Destroy(gameObject);
        Debug.Log("Экзорцизм выполнен успешно!");
        exorcismTime = null; // Сбрасываем корутину после успешного выполнения
        Instantiate(vfx,transform.position, transform.rotation);
        firstPlayerButton.text = "";
        secondPlayerButton.text = "";
    }

    // Возможно потом подумать над тем как добавить сюда, отталкивание взрывом если клавиши нажаты не те
}

