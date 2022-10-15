using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inTheGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;

    public GameState currentGameState = GameState.menu;
    public Canvas menuCanvas, gameCanvas, gameOverCanvas;
    public int collectedCoins = 0;

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentGameState = GameState.menu;
        menuCanvas.enabled = true;
        gameCanvas.enabled = false;
        gameOverCanvas.enabled = false;
    }

    public void StartGame()
    {
        PlayerController.sharedInstance.StartGame();
        LevelGenerator.sharedInstance.GenerateInitialBlocks();
        ChangeGameState(GameState.inTheGame);
        ViewInGame.sharedInstance.UpdateHighScoreLabel();
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        LevelGenerator.sharedInstance.RemoveAllTheBlocks();
        ChangeGameState(GameState.gameOver);
        ViewGameOver.sharedInstance.UpdateUI();
        Time.timeScale = 0;
    }

    public void BackToMainMenu()
    {
        ChangeGameState(GameState.menu);
    }

    void ChangeGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            // currentGameState = GameState.menu;
            menuCanvas.enabled = true;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
        }
        else if (newGameState == GameState.inTheGame)
        {
            // currentGameState = GameState.inTheGame;
            menuCanvas.enabled = false;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
        }
        else if (newGameState == GameState.gameOver)
        {
            // currentGameState = GameState.gameOver;
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = true;
        }

        currentGameState = newGameState;
    }

    public void CollectCoin()
    {
        collectedCoins++;
        ViewInGame.sharedInstance.UpdateCoinsLabel();
    }
}
