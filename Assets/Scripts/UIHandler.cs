using GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject highscoreUI;
    GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void StartGame()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(true);
        gameManager.StartGame();
    }

    public void EnableGameUI(bool flag)
    {
        gameUI.SetActive(flag);
    }

    public void EnableMenuUI(bool flag)
    {
        menuUI.SetActive(flag);
    }
    public void EnableHighscoreUI(bool flag)
    {
        highscoreUI.SetActive(flag);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
