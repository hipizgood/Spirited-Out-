using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    // Забьем сюда экраны главного меню
    public GameObject mainScreen;
    public GameObject controllScreen;
    public GameObject instructionScreen;
    public GameObject creditsScreen;

    //Важные кнопки
    public Button startGame;
    public Button instruction;
    public Button controll;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Метод для кнопки начинающий игру
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    // Кнопка отключает любой из откртых экранов и включает экран главного меню
    public void BackToTitle()
    {
        controllScreen.SetActive(false);
        instructionScreen.SetActive(false);
        creditsScreen.SetActive(false);

        mainScreen.SetActive(true);
    }

    public void MainControllScreen()
    {
        mainScreen.SetActive(false);
        controllScreen.SetActive(true);
    }

    public void MainInstructionScreen()
    {
        mainScreen.SetActive(false);
        instructionScreen.SetActive(true);
    }

    public void CreditScreen()
    {
        mainScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void QuitTheGame()
    {
        //Метод для выхода из игры
        Application.Quit();
    }
}
