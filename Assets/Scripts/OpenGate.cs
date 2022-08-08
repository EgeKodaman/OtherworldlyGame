using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class OpenGate : MonoBehaviour
{
    Animator animator;
    [SerializeField] TextMeshProUGUI warning;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player" && FindObjectOfType<GameSession>().GetComponent<GameSession>().hasKey)
        StartCoroutine("FinishGame");
        else if(other.gameObject.tag == "Player" && !FindObjectOfType<GameSession>().GetComponent<GameSession>().hasKey)
        StartCoroutine("ShowWarningText");

    }

    IEnumerator ShowWarningText()
    {
        warning.text = "You need to find a key";
        yield return new WaitForSecondsRealtime(3f);
        warning.text = "";
    }

    IEnumerator FinishGame()
    {
        animator.SetTrigger("openDoor");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(2);
    }
}
