using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartScript : MonoBehaviour, ISubmitHandler
{
    [SerializeField] TextMeshProUGUI continueText;
    float timeToWait = 1f;
    
    void Update()
    {
        timeToWait -= Time.deltaTime;
        if(timeToWait <= 0)
        BlinkText();
    }

    void BlinkText()
    {
        continueText.enabled = !continueText.enabled;
        timeToWait = 1f;
    }

    public void OnSubmit(BaseEventData eventData)
    {
        SceneManager.LoadScene(1);
    }

}
