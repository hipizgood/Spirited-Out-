using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Melle_enemies_movement : MonoBehaviour
{
    public float speed = 5f; // Установите скорость передвижения
    private Rigidbody rb;

    private Vector3 randomPoint;
    private bool isPointReach = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // мы запускаем коррутину, для того чтобы сгенерировать новую случайную точку
        //Можно запускать корутину в Start(), и она может проверять условия в течение всей игры, пока это целесообразно.
        //Это может быть полезно для реализации различных логик, таких как детектирование состояния игрока,
        //Управление таймерами или отслеживание событий в  игровом мире.
        StartCoroutine(GenerateRandomPoint());
    }
    /*Метод FixedUpdate:
- Этот метод вызывается каждый кадр, отвечая за обновление физики.
- Каждый раз при его вызове происходит проверка состояния флага isPointReach.
- Если isPointReach равен false (то есть объект еще не достиг случайной точки), метод FixedUpdate вызывает MoveToRandomPoint.
- Если MoveToRandomPoint опять не завершает выполнение, так как объект всё еще движется к точке, управление снова возвращается в FixedUpdate, и процесс повторяется.

Таким образом, пока объект не достигнет случайной точки и не установит флаг isPointReach в true, метод FixedUpdate будет продолжать вызывать MoveToRandomPoint, 
 позволяя объекту двигаться в нужном направлении. Как только объект достигнет точки, флаг будет установлен в true, 
 и это предотвратит дальнейшие вызовы метода MoveToRandomPoint, позволяя корутине (если она выполняется) сгенерировать новую случайную точку.
     */
    void FixedUpdate()
    {
        // Состояние достигли мы точки или нет.
        if (!isPointReach)
        {
            // Несли точка не достигнута, мы отправляемся на проверку в метод. И на движение к точку
            MoveToRandomPoint();
        }
    }
    /* Метод MoveToRandomPoint:
- В этом методе сначала выполняется проверка, достиг ли объект случайной точки. 
  Если проверка не проходит (то есть объект не достиг точки), выполняется логика движения к этой точке.
- Объект продолжает двигаться в направлении случайной точки, пока условие не станет истинным (то есть пока объект не подойдет близко к этой точке).
     */
    void MoveToRandomPoint()
    {
        // Проверка, достигли ли мы случайной точки
        if (Vector3.Distance(transform.position, randomPoint) < 0.5f) // 0.5f для легкости достижения
        {
            isPointReach = true; // Установить флаг
            // Если условие выполнено выходит из метода
            return;

        }
        // если же условие не выполненно, мы двигаемся к точке заново.
        Vector3 moveDirection = (randomPoint - transform.position).normalized;
        rb.MovePosition(transform.position + moveDirection * speed * Time.fixedDeltaTime);
    }
    // она работает все это время, и делает перерыв на 3 секунды
    IEnumerator GenerateRandomPoint()
    {
        // В коррутине мы проверяем isPointReach, и если так генинруем новую случайну точку
        
        while (true)
        {
            if (isPointReach)
            {
                float randomPosX = Random.Range(-30f, 30f);
                float randomPosZ = Random.Range(50f, 19f);
                // Сохраняем данные в глобальной переменной
                randomPoint = new Vector3(randomPosX, transform.position.y, randomPosZ);
                // И выставляем флаг на фолс. Таким образом, мы говорим о том что новая точка еще не достигнута
                isPointReach = false;
            }
            // И ждем 3 секунды прежде чем сгенинровать новую точку
            yield return new WaitForSeconds(3f); // ждем 3 секунды перед генерацией новой точки
        }
    }
}
