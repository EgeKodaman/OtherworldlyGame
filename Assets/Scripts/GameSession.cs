using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    int score;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Image thirdLife;
    [SerializeField] Image secondLife;
    [SerializeField] Image firstLife;
    SettingPersist setting;
    public bool poweredUp = false;
    public bool hasKey = false;
    public int laserDamage = 1;

    void Awake()
    {
        setting = FindObjectOfType<SettingPersist>();
        score = setting.GetInstance().GetGoldAmount();
        scoreText.text = setting.GetInstance().GetGoldAmount().ToString();
    }

    public void ProcessPlayerDeath(int playerLives)
    {
        if(playerLives >= 0)
        {
            switch(playerLives)
            {
                case 2:
                thirdLife.enabled = false;
                break;
                case 1:
                secondLife.enabled = false;
                break;
                case 0:
                firstLife.enabled = false;
                break;
            }
        }
        if(playerLives == 0)
        StartCoroutine(EndGameSession());
    }

    public void IncreaseDamage()
    {
        laserDamage = 10;
    }

    public void IncreaseScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
        setting.GetInstance().goldAmount += scoreToAdd;
    }

    IEnumerator EndGameSession()
    {
        yield return new WaitForSecondsRealtime(2f);
        setting.GetInstance().hasDied = true;
        SceneManager.LoadScene(2);
    }

}
