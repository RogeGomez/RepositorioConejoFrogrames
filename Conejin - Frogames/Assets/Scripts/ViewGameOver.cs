using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewGameOver : MonoBehaviour
{
    public static ViewGameOver sharedInstance;

    [SerializeField] private Text coinsLabel;
    [SerializeField] private Text scoreLabel;

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

    private void Update()
    {

    }

    public void UpdateUI()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.gameOver)
        {
            coinsLabel.text = GameManager.sharedInstance.collectedCoins.ToString();
            scoreLabel.text = PlayerController.sharedInstance.GetDistance().ToString("f0");
        }
    }
}
