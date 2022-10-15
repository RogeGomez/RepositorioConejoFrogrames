using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour
{
    public static ViewInGame sharedInstance;

    [SerializeField] private Text coinsLabel;
    [SerializeField] private Text scoreLabel;
    [SerializeField] private Text maxScoreLabel;

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            scoreLabel.text = PlayerController.sharedInstance.GetDistance().ToString("f0");
        }
    }

    public void UpdateHighScoreLabel()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            maxScoreLabel.text = PlayerPrefs.GetFloat("highScore", 0).ToString("f0");
        }
    }

    public void UpdateCoinsLabel()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            coinsLabel.text = GameManager.sharedInstance.collectedCoins.ToString();
        }
    }
}
