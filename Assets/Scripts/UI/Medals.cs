using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Medals : MonoBehaviour
{
    public Sprite[] medal_Sprite = null;
    private Image medal = null;

    private void Awake()
    {
        medal = GetComponent<Image>();
    }

    private void OnEnable()
    {
        int score = GameManager.Inst.Score;
        if (score < 10)
        {
            medal.color = Color.clear;
        }
        else if (score >= 10) //bronze
        {
            medal.sprite = medal_Sprite[0];
            medal.color = Color.white;
        }
        else if (score >= 20) //silver
        {
            medal.color = Color.white;
            medal.sprite = medal_Sprite[1];
        }
        else if (score >= 30) //gold
        {
            medal.color = Color.white;
            medal.sprite = medal_Sprite[2];
        }
        else  //platinum
        {
            medal.color = Color.white;
            medal.sprite = medal_Sprite[3];
        }

    }
}
