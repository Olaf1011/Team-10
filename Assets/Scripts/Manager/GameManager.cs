using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button m_startButton;
    [SerializeField] private GameObject loseScreen, winScreen;

    static private GameManager instance = null;

    static bool isPlayerDead, playerWon;

    public enum GameState
    {
        WELCOME,
        MENU,
        PAUSED,
        PLAYING,
        GAME_OVER
    }

    public static GameState gameState;


    void Awake()
    {
        gameState = GameState.WELCOME;
        Debug.Assert(instance == null);
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_startButton.onClick.AddListener(TaskOnClick);
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState) {
            case GameState.PLAYING:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.PAUSED; 
                }
                break;
            case GameState.PAUSED:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.PLAYING;
                }
                break;
        }
    }
    public static void PlayerDied()
    {

    }
    public static void PlayerWon()
    {

    }
    void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    void TaskOnClick()
    {
        SceneManager.LoadScene("Game"); //Switches scene to the game scene
        gameState = GameState.PLAYING;  //Sets the gameState to playing so all the correct scripts run.
    }
}
