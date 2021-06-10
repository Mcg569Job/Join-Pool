using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus { Null, Normal, Finish, GameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public GameStatus GameStatus;


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        GameStatus = GameStatus.Null;
    }

    #region Game Status


    public bool IsGameStatNormal() => GameStatus == GameStatus.Normal;
    public void StartGame()
    {
        if (GameManager.Instance.GameStatus != GameStatus.Null)
            return;

        GameStatus = GameStatus.Normal;
        UIManager.Instance.ActivateMenuPanel(false);
        UIManager.Instance.ActivateGamePanel(true);
    }
    public void FinishGame()
    {
        GameStatus = GameStatus.Finish;
        UIManager.Instance.ActivateWinPanel(true);
        AudioManager.Instance.PlaySound(AudioType.Win);
    }
    public void GameOver()
    {
        GameStatus = GameStatus.GameOver;
        UIManager.Instance.ActivateGameOverPanel(true);
        AudioManager.Instance.PlaySound(AudioType.GameOver);
    }

    #endregion
}
