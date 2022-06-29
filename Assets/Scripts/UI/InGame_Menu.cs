using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGame_Menu : MonoBehaviour
{
    private GameObject tap2Play = null;
    private FlappyBird bird = null;

    public GameObject UI_Gameover = null;
    public GameObject UI_GetReady = null;
    public GameObject UI_Home = null;
    public GameObject UI_Restart = null;

    public System.Action onPlayButton = null;

    private void Awake()
    {
        tap2Play = transform.Find("TaptoStart").gameObject;

        bird = FindObjectOfType<FlappyBird>();

        bird.onGameover = ShowGameover;
    }

    void ShowGameover()
    {
        UI_Gameover.SetActive(true);
        UI_Home.SetActive(true);
        UI_Restart.SetActive(true);
    }

    public void OnPlay()
    {
        StartCoroutine(GetReady());
    }

    public void OnHome()
    {
        SceneManager.LoadScene(0);
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator GetReady()
    {
        UI_GetReady.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        UI_GetReady.SetActive(false);
        tap2Play.SetActive(false);
        GameManager.Inst.IsGameOver = false;
        onPlayButton?.Invoke();
    }
}
