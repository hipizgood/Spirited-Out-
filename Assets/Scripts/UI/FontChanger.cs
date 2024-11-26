using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;



public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Переменная для хранения компонента Text
    private Vector3 originalScale; // Исходный размер текста

    private void Start()
    {
        originalScale = gameObject.transform.localScale; // Сохраняем исходный размер текста
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Увеличиваем и поворачиваем текст
        gameObject.transform.localScale = originalScale * 1.2f; // Увеличиваем размер текста
       gameObject.transform.Rotate(0, 0, 15); // Поворачиваем текст на 15 градусов
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Возвращаем текст в исходное состояние
        gameObject.transform.localScale = originalScale; // Возвращаем исходный размер текста
       gameObject.transform.rotation = Quaternion.Euler(0, 0, 0); // Возвращаем текст в исходное положение
    }
}
