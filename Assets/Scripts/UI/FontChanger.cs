using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;



public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ���������� ��� �������� ���������� Text
    private Vector3 originalScale; // �������� ������ ������

    private void Start()
    {
        originalScale = gameObject.transform.localScale; // ��������� �������� ������ ������
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ����������� � ������������ �����
        gameObject.transform.localScale = originalScale * 1.2f; // ����������� ������ ������
       gameObject.transform.Rotate(0, 0, 15); // ������������ ����� �� 15 ��������
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ���������� ����� � �������� ���������
        gameObject.transform.localScale = originalScale; // ���������� �������� ������ ������
       gameObject.transform.rotation = Quaternion.Euler(0, 0, 0); // ���������� ����� � �������� ���������
    }
}
