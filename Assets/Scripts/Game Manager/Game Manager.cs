using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    // ������ ���������� ��� ������
    public TextMeshProUGUI livesText;
    private int lives;

    // ������ ���������� ��� ��������
    public TextMeshProUGUI portalText;
    private int portalsCount; // ���������� ���������� ��� ���������� ��������
    private GameObject[] portals;
    public TextMeshProUGUI enemiesHow;
    // ������� �� ������
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI youWinText;


    // �������� ������
    public Button restartButton;
    public Button mainMenu;

    //������ ����������
    public Button instructionButton;
    public Button backButtn;

    //������ ���� � ��������
    public GameObject gameOverScreen;
    public GameObject uIInformation;
    public GameObject controllScreen;
    public GameObject instructionScreen;
    public GameObject victoryScreen;
    //������� �����
    public GameObject pauseScreen;
    public bool isPaused;

    // ������� ��������� ����
    public bool gameIsOver;
    public bool victory;

    
    void Start()
    {
        // ������ ������
       
        // ������������� ������
        lives = 7;
        Damage(0);

        // ���������� ��� �����

        isPaused = false;

        //������������� � ������
        gameIsOver = false;
        victory = false;
        //���� �� ������� ��� ��������, �� ����� ��������. � ������ ����� ���� �� ��������� ��� � restart
        if (!gameIsOver && !victory)
        {
            Time.timeScale = 1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // ������������ �������
        portals = GameObject.FindGameObjectsWithTag("Portal");
        portalsCount = portals.Length; // �������� ���������� ��������� ��������
        portalText.text = "PORTALS: " + portalsCount; // ��������� ����� � ����������� ��������

        //������� �����, ��� ��� ����������, ��� ���������� ��������.
        enemiesHow.text = "";
        
        //�������� ���������� ��������
        if (portalsCount == 0)
        {
            portalText.text = "";
           // ����� �������� �������, � ���� ��������� ������, � ���� ��� ����, �� ���������� ����� ������
           GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            int enemiesCount = enemies.Length;
            
            enemiesHow.text = "ENEMIES: " + enemiesCount;
            if (enemiesCount==0)
            {
                VictoryScreen();
            }
        }

        //�������� ������
        if (lives == 0)
        {
            GameOver();
        }

        //�������� �����
        // ���������� � ����� ��� ��������, �� ����� ������ 2 if. ��-�� ���� ��� ��������.
        // ������� �������� �� ��������� � ������
        if (Input.GetKeyDown(KeyCode.Escape)&& !gameIsOver && !victory)
        {
            if (!isPaused)
            {
                PauseMenu();
                
                
            }
            else
            {
                // ��� �� ����� ��������� ������ ������� � ��� ����� ���� ������� ����� ���� �����
                UnPause();
                controllScreen.SetActive(false);
                instructionScreen.SetActive(false);
            }
        }
            

    }

    // ��� �� � ��� �������. �� ������� ��������� �����
    // ��� � ������ �����, ������� �������� ����������� � ������������ ������ � ������������ ������������
    // �� � �� ���� ��� ������, ��� ��� ����� ������������ ����� �����, �� ������ � �������� ������ ������� ��������.

    // ������ �������� ����� ���������� �� ������� Melee_atack � Projectail
    public void Damage(int damage)
    {
        lives -= damage;
        livesText.text = "LIVES: " + lives;


    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    // �������� ������ 
    //����� ���������
    public void GameOver()
    {
        // ��� ������ ����������� ��������� � ���������� ����� � ������� � ���� � ���������
        uIInformation.SetActive(false);
        gameOverScreen.SetActive(true);
        //�������, ����� ����� �� ����������� �� ����� ���������
        gameIsOver = true;
        // ������������� �����
        Time.timeScale = 0;

    }
    //
    public void VictoryScreen()
    {
        victoryScreen.SetActive(true);
        uIInformation.SetActive(false);
        //������� ����� ����� �� ����������� �� ����� ��������
        victory = true;
        Time.timeScale = 0;
    }

    // ������ ��� �����
    public void PauseMenu()
    {
        
        pauseScreen.SetActive(true);

        Time.timeScale = 0;
        isPaused = true;
        Debug.Log("Game Paused");
    }

    public void UnPause()
    {
        
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        Debug.Log("Game Unpaused");
    }

    // ����� ��� ���� ����������
    public void ControllScreen()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 0;
        controllScreen.SetActive(true);
    }
    // ���������� ������ ����� ��� ����������
    public void InstructionScreen()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 0;
        instructionScreen.SetActive(true);
    }


    // ������������� ������ Back
    // ��� �� ������ ��������� ������, �� � ��������� ���������� �����
    public void BackButton()
    {
        //�������� � ���� ��������
        if (!gameIsOver)
        {
            PauseMenu();
            controllScreen.SetActive(false);
            instructionScreen.SetActive(false);
        } else
        {
            controllScreen.SetActive(false);
            instructionScreen.SetActive(false);
        }

        
       
        
    }

    

    
}
