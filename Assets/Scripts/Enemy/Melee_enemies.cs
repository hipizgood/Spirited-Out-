using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_enemies : MonoBehaviour
{
    //переменная для хранения данных
    private GameObject playerTarget;


   
    private float maxDistance;
    // Для удобства храюнб тут просто данные, их использую и в физическом методе и в инициализации максимальнйо дистанции
    public float radius = 25f;

    public bool isAttacking = false;

    private Vector3 attackPosition;
    //больше никаких векторов для физических тел
    public float forceAttack;
    private Rigidbody rb;

    //Для аниматор
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
        //Используем физический метод, чтобы найти все коллайдеры в радиусе 25 юнитов
        Collider[] players = Physics.OverlapSphere(gameObject.transform.position, radius);
        // Инициализируем данную переменную как пустую
        playerTarget = null;
        //Инициализируем максимальное расстояние, оно будет использоваться для сравнения(как абсолютная величина)
        maxDistance = radius;
        
        
      
        // Дальше вводим условие для каждого коллайдера в массиве плауерс
        foreach (Collider player in players)
        {
            // Указываем тот типо коллайдеров который нас интересует, с тегом Player
            if (player.gameObject.CompareTag("Player"))

            {
                // далее я считаю расстояние между выбранным игроком и объектом
                float distanceToPlayer = Vector3.Distance(gameObject.transform.position,player.transform.position);

                Vector3 vectorToPlayer =(player.transform.position - gameObject.transform.position).normalized;
                

                //Если расстояние меньше максимальнйо дистанции, то выбирается ближайшее по значениию тело 
                //Другими словами кто ближе того и санки
                if (distanceToPlayer < maxDistance)
                {
                    // Передаю в переменную новые значения, которые после фиксации равны дистации до билэайшего объекта
                    maxDistance = distanceToPlayer;
                    // передаю в переменную значение, ближайшего объъекта
                    playerTarget = player.gameObject;

                   
                }
                
            }
        }
        //Ввожу проверку , чтобы получение прошло успешно
        if (playerTarget != null)
        {
            
            // И Использю полученные данные о позиции объекта.
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
        //Проблема была  втом что метод MoveTowards, который я использовла до этого работал с компонентом трансформ. И на прямую менял позициию игрока.
        //Это приводило к проблемам с тем, что объект либо не двигался, либо двигался очень медленно.
        // В итоге мы просто использовали классический метод работы с твердым телом AddForce.
        isAttacking = true;
        // В этой строчке мы считаем направление приложенной силы, используя в методе аргумент полученный из проверки коллайдеров и расстояния до ближайшей цели
        Vector3 directionMove = (currentTarget - gameObject.transform.position).normalized;
        //Прикладываем силу в направлении цели игрока(ближайшего)
        rb.AddForce(directionMove * forceAttack,ForceMode.Impulse);
        // Чтобы сделать паузу, мы запускаем коррутину
        StartCoroutine(ChillTime());
    }

    IEnumerator ChillTime()
    {
        // тут все просто мы ждем задаенное время, и возвращаем значение isAttack = false
        // В дальнейшем это позволяет в вызываемом в каждом кадре методе FindPlayer,проводить проверку после выбора ближайшей цели, вызывать метод Atack
        yield return new WaitForSeconds(5);
        Debug.Log("Cooldown finished. Ready to attack again.");
        isAttacking = false;
    }

}
