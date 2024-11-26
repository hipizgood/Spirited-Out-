using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    // Вводим переменную для жизней
    public TextMeshProUGUI livesText;
    private int lives;

    // Вводим переменную для порталов
    public TextMeshProUGUI portalText;
    private int portalsCount; // Глобальная переменная для количества порталов
    private GameObject[] portals;
    public TextMeshProUGUI enemiesHow;
    // Надписи на экране
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI youWinText;


    // Забиндим кнопки
    public Button restartButton;
    public Button mainMenu;

    //Кнопки иснтрукции
    public Button instructionButton;
    public Button backButtn;

    //разные поля с кнопками
    public GameObject gameOverScreen;
    public GameObject uIInformation;
    public GameObject controllScreen;
    public GameObject instructionScreen;
    public GameObject victoryScreen;
    //Немного паузы
    public GameObject pauseScreen;
    public bool isPaused;

    // Булевые состояний игры
    public bool gameIsOver;
    public bool victory;

    
    void Start()
    {
        // Поищем врагов
       
        // Инициализация жизней
        lives = 7;
        Damage(0);

        // Переменная для паузы

        isPaused = false;

        //Инициализация в старте
        gameIsOver = false;
        victory = false;
        //если не сделать эту проверку, то плохо работает. В теории можно было бы перенести это в restart
        if (!gameIsOver && !victory)
        {
            Time.timeScale = 1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Подсчитываем порталы
        portals = GameObject.FindGameObjectsWithTag("Portal");
        portalsCount = portals.Length; // Получаем количество найденных порталов
        portalText.text = "PORTALS: " + portalsCount; // Обновляем текст с количеством порталов

        //Скрывет текст, без его отключения, как отдельного элемента.
        enemiesHow.text = "";
        
        //Проверка количества порталов
        if (portalsCount == 0)
        {
            portalText.text = "";
           // После закрытия портала, я хочу посчитать врагов, и пока они есть, не показывать кэран победы
           GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            int enemiesCount = enemies.Length;
            
            enemiesHow.text = "ENEMIES: " + enemiesCount;
            if (enemiesCount==0)
            {
                VictoryScreen();
            }
        }

        //Проверка смерти
        if (lives == 0)
        {
            GameOver();
        }

        //Проверка паузы
        // Изначально я делал две проверки, но длела просто 2 if. Из-за чего все ломалось.
        // Добавил проверку на проигрышь и победу
        if (Input.GetKeyDown(KeyCode.Escape)&& !gameIsOver && !victory)
        {
            if (!isPaused)
            {
                PauseMenu();
                
                
            }
            else
            {
                // Тут мы также отключаем экраны которые у нас могут быть открыты через меню паузы
                UnPause();
                controllScreen.SetActive(false);
                instructionScreen.SetActive(false);
            }
        }
            

    }

    // так бы я мог сделать. Но давайте попробуем иначе
    // Тут я вводил метод, который проверял стокновения с коллайдерами врагов и коллайдерами проджектайла
    // Но я не стал так делать, так как хотел выпендриться перед собой, во вторых у летающих врагов длинный колайдер.

    // Теперь аргумент также передается из скрипта Melee_atack и Projectail
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
    // Основные экраны 
    //Экран проигрыша
    public void GameOver()
    {
        // При смерти выключается интерфейс и включается экран с выходом в меню и рестартом
        uIInformation.SetActive(false);
        gameOverScreen.SetActive(true);
        //Булевая, чтобы пауза не срабатывала во время проигрыша
        gameIsOver = true;
        // Останавливаем время
        Time.timeScale = 0;

    }
    //
    public void VictoryScreen()
    {
        victoryScreen.SetActive(true);
        uIInformation.SetActive(false);
        //Булевая чтобы пауза не срабатывала во время выигрыша
        victory = true;
        Time.timeScale = 0;
    }

    // Методы для паузы
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

    // Метод для окна управления
    public void ControllScreen()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 0;
        controllScreen.SetActive(true);
    }
    // Аналогично делаем метод для инструкции
    public void InstructionScreen()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 0;
        instructionScreen.SetActive(true);
    }


    // Универсальная кнопка Back
    // Она не только закрывает экраны, но и сохраняет состаояние паузы
    public void BackButton()
    {
        //проблема в этой проверке
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
