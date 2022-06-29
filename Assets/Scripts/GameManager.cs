using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    private int score = 0;

    private bool isGameOver = false;

    public static GameManager Inst { get => instance; }

    public int Score { get => score; set{ score = value; } }
    public bool IsGameOver { get => isGameOver; set { isGameOver = value; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.Initialize();
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Initialize()
    {
        Score = 0;
        IsGameOver = true;
    }
}
