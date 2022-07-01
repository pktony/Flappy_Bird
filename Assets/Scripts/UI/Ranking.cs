using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

//{"highScore":[6,5,4,3,2],"highName":["aa","bb","cc","dd","ee"]}

public class Ranking : MonoBehaviour
{
    HighScoreLine[] highScoreLines = null;
    TMP_InputField inputField = null;

    const int highscoreNum = 5;

    public Vector3 inputFieldOffset = Vector3.zero;
    public string inputName = null;
    public int inputRank = GameManager.INVALID_RANK;

    private void Awake()
    {
        highScoreLines = new HighScoreLine[highscoreNum];
        for (int i = 0; i < highScoreLines.Length; i++)
        {
            highScoreLines[i] = transform.GetChild(i).GetComponent<HighScoreLine>();
        }

        inputField = transform.GetChild(transform.childCount - 1).GetComponent<TMP_InputField>();
        inputField.onEndEdit.AddListener(OnInputName);
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void Open(int rank)
    {
        for (int i = 0; i < highScoreLines.Length; i++)
        {
            highScoreLines[i].SetHighScore(GameManager.Inst.HighScore[i]);
            highScoreLines[i].SetHighName(GameManager.Inst.HighName[i]);
        }

        if (rank != GameManager.INVALID_RANK)
        {
            inputField.gameObject.SetActive(true);
            inputField.gameObject.transform.position = highScoreLines[rank].gameObject.transform.position + inputFieldOffset;

            inputRank = rank;
        }
    }

    private void OnInputName(string input)
    {
        highScoreLines[inputRank].SetHighName(input);
        inputField.gameObject.SetActive(false);
        GameManager.Inst.SetHighName(inputRank, input);
    }

}
