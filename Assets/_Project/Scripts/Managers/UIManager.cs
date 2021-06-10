using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region UI Panels Struct
    [System.Serializable]
    private struct UIPanels
    {
        public GameObject MenuPanel;
        public GameObject GamePanel;
        public GameObject GameOverPanel;
        public GameObject PausePanel;
        public GameObject WinPanel;
    }
    #endregion

    #region UI Texts Struct
    [System.Serializable]
    private struct UITexts
    {
        public Text levelText;
        public Text levelTextOnScene;
        public Text coinTextOnMenu;
        public Text coinTextOnGame;
        public Text countText;
        public Text coinTextOnWin;
    }
    #endregion

    #region Singleton
    public static UIManager Instance = null;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    #endregion

    [Header("-PANELS-")]
    [SerializeField] private UIPanels uiPanels;

    [Header("-TEXTS-")]
    [SerializeField] private UITexts uiTexts;
    [SerializeField] private Text earnedCoinText;


    void Start()
    {
        ActivateGamePanel(false);
        ActivateMenuPanel(true);
        UpdateCoinTexts();
        UpdateLevelTexts(LevelManager.Instance.currentLevel);

        Time.timeScale = 1;
    }

    #region Panels
    public void ActivateMenuPanel(bool activate)
    {
        uiPanels.MenuPanel.SetActive(activate);
    }
    public void ActivateGamePanel(bool activate)
    {
        uiPanels.GamePanel.SetActive(activate);
    }
    public void ActivateGameOverPanel(bool activate)
    {
        uiPanels.GameOverPanel.SetActive(activate);
        ActivateGamePanel(false);
    }
    public void ActivatePausePanel(bool activate)
    {
        uiPanels.PausePanel.SetActive(activate);
        GameManager.Instance.GameStatus = activate ? GameStatus.Null : GameStatus.Normal;
        Time.timeScale = activate ? 0 : 1;
    }
    public void ActivateWinPanel(bool activate)
    {
        uiPanels.WinPanel.SetActive(activate);
        ActivateGamePanel(false);
    }

    #endregion

    #region Texts
    public void UpdateLevelTexts(int level)
    {
        uiTexts.levelText.text = "LEVEL " + level;
        uiTexts.levelTextOnScene.text = "Level " + level.ToString();
    }
    public void UpdateCountText(int value)
    {
        uiTexts.countText.text = "x" + value;
    }
    public void UpdateCoinTexts()
    {
        int value = Data.Instance.GetCoin();
        uiTexts.coinTextOnMenu.text = value.ToString("0000");
        uiTexts.coinTextOnGame.text = value.ToString("0000");
    }
    public void UpdateWinTexts(int earnedCoins)
    {
          uiTexts.coinTextOnWin.text = "0";
          StartCoroutine(UpdateCoinTextEnum(earnedCoins));
    }
   
    #endregion

    #region Effects
    public void CollectCoinText(int amount)
    {
        StartCoroutine(CollectCoinTextAnimation(amount));
    }
    private IEnumerator CollectCoinTextAnimation(int amount)
    {
        earnedCoinText.text = "+"+amount;
        earnedCoinText.gameObject.SetActive(true);
        
        earnedCoinText.transform.position += new Vector3(Random.Range(5, 10), Random.Range(5, 10), 0);
        earnedCoinText.transform.localScale *= .05f;

        while (earnedCoinText.transform.localScale.x < 1)
        {
            earnedCoinText.transform.localScale += new Vector3(1,1,1) * Time.deltaTime * 8;
            yield return null;
        }

        yield return new WaitForSeconds(.2f);
        earnedCoinText.gameObject.SetActive(false);
    }

    private IEnumerator UpdateCoinTextEnum(int amount)
    {
        yield return new WaitForSeconds(.6f);
        int i = 0;
        while (i < amount)
        {
            i += 5;
            if (i > amount) i = amount;
              uiTexts.coinTextOnWin.text = "+"+ i.ToString();
            yield return new WaitForSeconds(.08f);
        }
    }

    #endregion
}
