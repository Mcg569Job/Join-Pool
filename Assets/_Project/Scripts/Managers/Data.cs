using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static Data Instance = null;
   

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public int GetCoin()
    {
        int c = PlayerPrefs.GetInt("Coin");       
        return c;
    }
    public void AddCoin(int amount)
    {
        int c = PlayerPrefs.GetInt("Coin");
        PlayerPrefs.SetInt("Coin", c + amount);
        UIManager.Instance.UpdateCoinTexts();
        UIManager.Instance.CollectCoinText(amount);
        AudioManager.Instance.PlaySound(AudioType.Coin);
    }

}
