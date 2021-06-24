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
        TinySauce.OnGameStarted(levelNumber: LevelManager.Instance.currentLevel.ToString());
    }
    public void FinishGame()
    {
        GameStatus = GameStatus.Finish;
        UIManager.Instance.ActivateWinPanel(true);
        UIManager.Instance.UpdateWinTexts(LifebuoyManager.Instance.LifebuoyCount * 10);
        AudioManager.Instance.PlaySound(AudioType.Win);
        TinySauce.OnGameFinished(true, levelNumber: LevelManager.Instance.currentLevel.ToString(), score: LifebuoyManager.Instance.LifebuoyCount);
    }
    public void GameOver()
    {
        GameStatus = GameStatus.GameOver;
        UIManager.Instance.ActivateGameOverPanel(true);
        AudioManager.Instance.PlaySound(AudioType.GameOver);
        TinySauce.OnGameFinished(false, levelNumber: LevelManager.Instance.currentLevel.ToString(), score: LifebuoyManager.Instance.LifebuoyCount);
    }

    #endregion
}
