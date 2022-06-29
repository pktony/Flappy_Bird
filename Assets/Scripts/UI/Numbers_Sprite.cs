using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Numbers_Sprite : MonoBehaviour
{
    public Sprite[] number_Sprite = new Sprite[10];
    public Image[] number_Places = null;

    //char[] score_array = null;

    int number = 0;

    public int Number 
    { 
        get => number;
        set 
        { 
            number = value; 
            Mathf.Clamp(number, 0, 999);

            int temp = number;  //number_Places[1] = 1,  [2] = 10, [3] = 100
            for (int i = 0; i < number_Places.Length; i++)
            {
                if (temp > 0)
                {
                    int rest = temp % 10;
                    number_Places[i].sprite = number_Sprite[rest];
                    number_Places[i].color = Color.white;
                    temp /= 10;
                }
                else
                {
                    number_Places[i].color = Color.clear;
                }
            }
        } 
    }

    private void Update()
    {
        if (Score_Adder.onUpdate)
        {
            Number = GameManager.Inst.Score;
            Score_Adder.onUpdate = false;
        }
    }

    //void UpdateScore()
    //{
    //    score_array = GameManager.Inst.Score.ToString().ToCharArray();

    //    ScorePad_Positioner();

    //    for (int i = 0; i < score_array.Length; i++)
    //    {
    //        int number = int.Parse(score_array[i].ToString());
    //        number_Prefab[i].GetComponent<Image>().sprite = number_Sprite[number];
    //    }
    //}

    //void ScorePad_Positioner()
    //{
    //    if (score_array.Length == 1)  //number_Places[1] = 1,  [2] = 10, [3] = 100
    //    {
    //        number_Prefab[0].GetComponent<RectTransform>().localPosition = new Vector2(0, -80);
    //        number_Prefab[1].SetActive(false);
    //        number_Prefab[2].SetActive(false);
    //    }
    //    else if (score_array.Length == 2)
    //    {
    //        number_Prefab[0].GetComponent<RectTransform>().localPosition = new Vector2(-50, -80);
    //        number_Prefab[1].SetActive(true);
    //        number_Prefab[1].GetComponent<RectTransform>().localPosition = new Vector2(50, -80);
    //    }
    //    else
    //    {
    //        number_Prefab[0].GetComponent<RectTransform>().localPosition = new Vector2(-70, -80);
    //        number_Prefab[1].GetComponent<RectTransform>().localPosition = new Vector2(0, -80);
    //        number_Prefab[2].SetActive(true);
    //        number_Prefab[2].GetComponent<RectTransform>().localPosition = new Vector2(70, -80);
    //    }
    //}
}
