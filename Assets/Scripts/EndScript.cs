using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerGold;
    [SerializeField] TextMeshProUGUI price;
    int currentGold;
    Color32 skinColor = new Color32(255, 0, 0, 255);
    Color32 defaultColor = new Color32(255, 255, 255, 255);
    SettingPersist setting;
    
    void Awake()
    {
        setting = FindObjectOfType<SettingPersist>();
        currentGold = setting.GetInstance().GetGoldAmount();
        playerGold.text = currentGold.ToString();
    }

    public void BuySkin()
    {
        if(currentGold >= 5000 && !setting.GetInstance().skinBought)
        {
            currentGold -= 5000;
            playerGold.text = currentGold.ToString();
            setting.GetInstance().playerColor = skinColor;
            setting.GetInstance().skinBought = true;
            price.enabled = false;
        }
    }

    public void DefaultSkin()
    {
        setting.GetInstance().playerColor = defaultColor;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGameSession()
    {
        SceneManager.LoadScene(1);
    }
}
