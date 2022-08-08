using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPersist : MonoBehaviour
{
    static SettingPersist settings;
    public Color32 playerColor = new Color32(255, 255, 255, 255);
    public bool skinBought = false;
    public int goldAmount = 0;
    public bool hasDied = false;

    public SettingPersist GetInstance()
    {
        return settings;
    }

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if(settings != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            settings = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetGoldAmount()
    {
        return goldAmount;
    }

}
