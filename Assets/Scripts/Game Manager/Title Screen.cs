using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    // ������ ���� ������ �������� ����
    public GameObject mainScreen;
    public GameObject controllScreen;
    public GameObject instructionScreen;
    public GameObject creditsScreen;

    //������ ������
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
    //����� ��� ������ ���������� ����
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    // ������ ��������� ����� �� ������� ������� � �������� ����� �������� ����
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
        //����� ��� ������ �� ����
        Application.Quit();
    }
}
