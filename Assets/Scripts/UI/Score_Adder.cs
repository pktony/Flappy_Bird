using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Score_Adder : MonoBehaviour
{
    //public System.Action onScoreUp = null;
    Numbers_Sprite numberSprite = null;
    public static bool onUpdate = false;

    private void Awake()
    {
        numberSprite = FindObjectOfType<Numbers_Sprite>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !GameManager.Inst.IsGameOver)
        {
            GameManager.Inst.Score += 1;
            onUpdate = true;
        }
    }
}
