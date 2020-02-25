using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button m_startButton;

    static private GameManager instance = null;

    static bool isPlayerDead, playerWon;
    
    //Tells everything in the game what GameState the game is in.
    public enum GameState           
    {
        WELCOME,    //The first screen. Shows the title and waits for player input to be moved to the menu
        MENU,       //Menu is where the player can look at the instructions and press "Play"
        PAUSED,     //Makes sure everything in the game is paused. For example the music or the enemy AI
        PLAYING,    //For the scripts to check whether they are allowed to do things
        GAME_OVER   //Player died [Fill in more]
    }
    public static GameState gameState;

    //Tells everything in the game when the game is "PLAYING" what the room state is in
    public enum RoomState
    {
        ENTERED,    //The player just entered the room [Not sure when we want the combo counter to start going down yet]
        FIGHTING,   //The player has initialised fighting things like the combo meter can now start counting down or up
        CLEARED     //The room is clear and no more enemy's start spawning. Things like the combo meter are now paused.
    }
    public static RoomState roomState;


    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;

        roomState = RoomState.FIGHTING;
        gameState = GameState.WELCOME;

    }

    // Start is called before the first frame update
    void Start()
    {
        m_startButton.onClick.AddListener(TaskOnClick);
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
