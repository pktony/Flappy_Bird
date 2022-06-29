using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Score_Adder : MonoBehaviour
{
    //public System.Action onScoreUp = null;

    public static bool onUpdate = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Inst.Score += 1;
            //Debug.Log(GameManager.Inst.Score);
            onUpdate = true;
            //onScoreUp?.Invoke();
        }
    }
}
