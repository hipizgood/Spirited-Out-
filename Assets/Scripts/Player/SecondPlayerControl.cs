using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayerControl : MonoBehaviour
{

    public float verticalInput;
    private float horizontalInput;

    [SerializeField] private float moveForce;
    private Rigidbody secondPlayerRb;

    public bool spiritVireExist = true;
    // ƒл€ времени дэша
    private float nextDash;

    //переменные дл€ аниматора
    private Animator animator;

    // переменна€ дл€ второго игроа - урон

    
    void Start()
    {
        secondPlayerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // нужно поработать с этим временем, так как онообновл€тес€ от текущего общего времени. не заново обсчитываетс€, а идет бесконечный отсчет
        Dash();
        UpdateAnimator();

    }

    private void FixedUpdate()
    {

        Move();
       
    }
    // создает условие, дл€ того тчобы не было бесконтрольного спавна спиритической линии
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spirit_Vire"))
        {
            spiritVireExist = false;
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextDash)
        {
            nextDash = Time.time + 2;

            secondPlayerRb.AddRelativeForce(Vector3.forward * 50.0f, ForceMode.VelocityChange);
            Debug.Log(nextDash);
        }
    }

    private void Move()
    {
        verticalInput = Input.GetAxis("Vertical_1");
        horizontalInput = Input.GetAxis("Horizontal_1");


        secondPlayerRb.AddForce(Vector3.forward * verticalInput * moveForce, ForceMode.Impulse);
        secondPlayerRb.AddForce(Vector3.right * horizontalInput * moveForce, ForceMode.Impulse);
        // ѕо другому повороты не смог придумать

        // ѕовороты вперед и назад
        if (verticalInput < 0)
        {
            Vector3 rotate = transform.eulerAngles;
            rotate.y = -180;
            transform.rotation = Quaternion.Euler(rotate);
        }
        else if (verticalInput > 0)
        {
            Vector3 rotate = transform.eulerAngles;
            rotate.y = 0;
            transform.rotation = Quaternion.Euler(rotate);
        }
        // ѕовороты влево и вправо
        if (horizontalInput < 0)
        {
            Vector3 rotate = transform.eulerAngles;
            rotate.y = -90;
            transform.rotation = Quaternion.Euler(rotate);
        }
        else if (horizontalInput > 0)
        {
            Vector3 rotate = transform.eulerAngles;
            rotate.y = 90;
            transform.rotation = Quaternion.Euler(rotate);
        }
    }
    private void UpdateAnimator()
    {
        // ”станавливаем параметры анимации
        if (animator != null)
        {
            animator.SetBool("run", verticalInput != 0 || horizontalInput != 0);  

        }
    }

  
}



