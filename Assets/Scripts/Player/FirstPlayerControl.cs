using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class FirstPlayerControl : MonoBehaviour
{
    [SerializeField] private float moveForce;
    private float verticalInput;
    private float horizontalInput;

    private Rigidbody firstPlayerRb;
    private float nextDash;

    // ��������� ���������� ��� Animator
    private Animator animator;

    // ������� ���������� ��� VF ����� �� ����� ����� �����
   
    void Start()
    {
        firstPlayerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Dash();
        UpdateAnimator();
      
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextDash)
        {
            nextDash = Time.time + 2;

            firstPlayerRb.AddRelativeForce(Vector3.forward * 100.0f, ForceMode.VelocityChange);
            Debug.Log(nextDash);
        }
    }

    private void Move()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");


        firstPlayerRb.AddForce(Vector3.forward * verticalInput * moveForce, ForceMode.Impulse);
        firstPlayerRb.AddForce(Vector3.right * horizontalInput * moveForce, ForceMode.Impulse);
        // �� ������� �������� �� ���� ���������

        // �������� ������ � �����
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
        // �������� ����� � ������
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
        // ������������� ��������� ��������
        if (animator != null)
        {
            animator.SetBool("run", verticalInput != 0 || horizontalInput != 0);  // ��������� �������� 'run' � true, ���� �������� ������
           
        }
    }

   


}
