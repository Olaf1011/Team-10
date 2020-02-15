using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject loseScreen, winScreen;
    static private GameManager instance = null;
    static bool isPlayerDead, playerWon;

    public enum GameState
    {
        WELCOME,
        MENU,
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
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (loseScreen)
        {
            loseScreen.SetActive(true);
        }
        if (playerWon)
        {
            winScreen.SetActive(true);
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
}
