using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    private GameObject UI_New = null;

    private int score = 0;

    const int rankCount = 5;
    public int rank = -1;
    public const int INVALID_RANK = -1;

    private int[] highScore = new int[rankCount];       //0이 가장높음
    private string[] highName = new string[rankCount];


    private bool isGameOver = false;

    public static GameManager Inst { get => instance; }

    public int Score { get => score; set { score = value; } }
    public int BestScore { get => highScore[0];}

    public int[] HighScore { get => highScore; }

    public string[] HighName { get => highName; }
    public bool IsGameOver { get => isGameOver; 
        set
        { 
            isGameOver = value;
            if (IsGameOver)
            {
                OnGameOver();
            }
        } 
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //instance.Initialize();
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += onScoreOpen;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void onScoreOpen(Scene arg0, LoadSceneMode arg1)
    {
        Initialize();
        UI_New = FindObjectOfType<InGame_Menu>().transform.Find("New").gameObject;
        UI_New.SetActive(false);
    }

    void Initialize()
    {
        Score = 0;
        IsGameOver = true;

        LoadGameData();
    }

    public void SaveGameData()
    {
        SaveData saveData = new();
        saveData.highScore = highScore;
        saveData.highName = highName;

        string json = JsonUtility.ToJson(saveData);
        string path = $"{Application.dataPath}/Save/"; //Assets folder
        // /Save 디렉토리 존재여부 확인
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        //full path = 파일명 + 확장자 명
        string fullPath = $"{path}Save.json";
        File.WriteAllText(fullPath, json);
    }

    public void LoadGameData()
    {
        string path = $"{Application.dataPath}/Save/";
        string fullPath = $"{path}Save.json";

        if (!Directory.Exists(path) && File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            highScore = saveData.highScore;
            highName = saveData.highName;
        }
    }

    public void OnGameOver()
    {
        // 현재스코어와 하이스코어 비교...
        // 1. 0부터 4까지 비교
        // 2. 현재 스코어보다 작으면 한칸씩 밀기
        rank = INVALID_RANK;
        for (int i = 0; i < rankCount; i++)
        {
            if (highScore[i] < Score)
            {
                for (int j = rankCount - 1; j > i; j--)
                {
                    highScore[j] = highScore[j - 1];
                    highName[j] = highName[j - 1];
                }
                highScore[i] = score;
                //SaveGameData();
                rank = i;
                UI_New.SetActive(true);
                break;
            }
        }
        //if (Score > Highscore)
        //{
        //    Highscore = Score;
        //    SaveGameData();
        //    UI_New.color = Color.white;
        //}
    }

    public void SetHighName(int rank, string newName)
    {
        HighName[rank] = newName;
        SaveGameData();
    }

}
