using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum GameState
{
    GAME_OVER = 0,
    RUNNING,
    START_GAME
}


public class manager : MonoBehaviour {

    [Header("Points")]
    public float secondsPerPoints;
    public int amountOfPointGain;

    [Header("Speed")]
    public float globalTravelSpeed;
    public float speedIncrease;

    public Player player;

    private float currentTimeIncrease;
    private int currHighscore = 0;
    public int highscore
    {
        get { return currHighscore; }
        set
        {
            if (value > currHighscore)
            {
                currHighscore = value;
                PlayerPrefs.SetInt("Highscore", currHighscore);
                PlayerPrefs.Save();
            }
        }
    }
    // static
    private static GameState state;
    private static float crystalTravelSpeed;

    // static getter:
    public static GameState gameState
    {
        get { return state; }
    }
    public static float travelSpeed
    {
        get { return crystalTravelSpeed; }
    }

    delegate void stateDelegates();
    stateDelegates currentState;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        crystalTravelSpeed = globalTravelSpeed;
        currentTimeIncrease = 0;
        state = GameState.START_GAME;

        currentState = PendingState;

        currHighscore = PlayerPrefs.GetInt("Highscore", 0);
    }

    // States
    private void PendingState()
    {

    }

    private void GameOverState()
    {
        if (ControlInput.Tap())
            RestartGame();
    }

    private void RunningState()
    {
        currentTimeIncrease += Time.deltaTime;
        if (currentTimeIncrease > secondsPerPoints)
        {
            currentTimeIncrease = 0;
            player.AddPoint(amountOfPointGain);
        }
        crystalTravelSpeed += Time.deltaTime * speedIncrease;
    }

    private void Update()
    {
        currentState();

        #if UNITY_STANDALONE
        if (Input.GetButtonUp("Quit Game"))
            Application.Quit();
        #endif
    }

    public void StopGame()
    {
        state = GameState.GAME_OVER;
        currentTimeIncrease = 0;
        currentState = GameOverState;
    }

    public void StartGame()
    {
        state = GameState.RUNNING;
        currentState = RunningState;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("map" ,LoadSceneMode.Single);
    }
}
