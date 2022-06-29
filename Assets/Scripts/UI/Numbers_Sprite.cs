using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Numbers_Sprite : MonoBehaviour
{
    public Sprite[] number_Sprite = null;
    public GameObject[] number_Places = null;
    //private RectTransform[] rect = null;

    char[] score_array = null;

    private void Awake()
    {
        //for (int i = 0; i < 3; i++)
        //{
        //    rect[i] = number_Places[i].GetComponent<RectTransform>();
        //}
    }

    private void Start()
    {
        //Adder.onScoreUp = UpdateScore;
    }

    private void Update()
    {
        if (Score_Adder.onUpdate)
        {
            UpdateScore();
            Score_Adder.onUpdate = false;
        }
    }

    void UpdateScore()
    {
        score_array = GameManager.Inst.Score.ToString().ToCharArray();

        ScorePad_Positioner();

        for (int i = 0; i < score_array.Length; i++)
        {
            int y = int.Parse(score_array[i].ToString());
            number_Places[i].GetComponent<Image>().sprite = number_Sprite[y];
        }
    }

    void ScorePad_Positioner()
    {
        if (score_array.Length == 1)  //number_Places[1] = 1,  [2] = 10, [3] = 100
        {
            number_Places[0].GetComponent<RectTransform>().localPosition = new Vector2(0, -80);
            number_Places[1].SetActive(false);
            number_Places[2].SetActive(false);
        }
        else if (score_array.Length == 2)
        {
            number_Places[0].GetComponent<RectTransform>().localPosition = new Vector2(-90, -80);
            number_Places[1].SetActive(true);
            number_Places[1].GetComponent<RectTransform>().localPosition = new Vector2(90, -80);
        }
        else
        {
            number_Places[0].GetComponent<RectTransform>().localPosition = new Vector2(-90, -80);
            number_Places[1].GetComponent<RectTransform>().localPosition = new Vector2(0, -80);
            number_Places[2].SetActive(true);
            number_Places[2].GetComponent<RectTransform>().localPosition = new Vector2(90, -80);
        }
    }
}
