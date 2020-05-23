using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScore = null;
    [SerializeField] private TextMeshProUGUI bestScore = null;

    private GameStatus game;

    void Start()
    {
        ShowScores();
    }

    private void ShowScores()
    {
        game = FindObjectOfType<GameStatus>();
        currentScore.text = game.GetScore().ToString();
        bestScore.text = PlayerPrefs.GetInt("bestScore") == 0 ?
            currentScore.text : PlayerPrefs.GetInt("bestScore").ToString();
    }
}
