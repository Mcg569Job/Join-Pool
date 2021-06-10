using UnityEngine;

public enum AudioType {Coin,Connect,Disconnect,Win,GameOver }

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance = null;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    #endregion

    [SerializeField] private AudioSource source;

    [SerializeField] private AudioClip Coin;
    [SerializeField] private AudioClip Connect;
    [SerializeField] private AudioClip Disconnect;   
    [SerializeField] private AudioClip Win;
    [SerializeField] private AudioClip GameOver;


    public void PlaySound(AudioType audioType)
    {
        if (PlayerPrefs.GetInt("sound") != 0) return;

        AudioClip clip = null;
        switch (audioType)
        {
            case AudioType.Coin: clip = Coin; break;           
            case AudioType.Connect: clip = Connect; break;
            case AudioType.Disconnect: clip = Disconnect; break;  
            case AudioType.Win: clip = Win; break;           
            case AudioType.GameOver: clip = GameOver; break;           
       
        }

        if (clip != null)
            source.PlayOneShot(clip);
    }

    public void Vibrate()
    {
        if (PlayerPrefs.GetInt("vibrate") == 0)
            Handheld.Vibrate();
    }
}
