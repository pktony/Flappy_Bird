using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Numbers_Sprite_Mid : MonoBehaviour
{
    public Sprite[] number_Sprite = new Sprite[10];
    public Image[] number_Places = null;

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

    private void OnEnable()
    {
        Number = GameManager.Inst.BestScore;
    }
}
