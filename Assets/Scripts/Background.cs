using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform[] ground = null;
    public Transform[] sky = null;

    public float groundMoveSpeed = 3.0f;
    public float skyMoveSpeed = 3.0f;

    private const float groundWidth = 8.4f;  //1.68*5
    private const float skyWidth = 7.2f;

    private void Update()
    {
        if (!GameManager.Inst.IsGameOver)
        {
            float currentPosition = transform.position.x - groundWidth * 2 - 0.5f;
            foreach (Transform ground in ground)
            {
                ground.transform.position += groundMoveSpeed * Time.deltaTime * -transform.right;
                if (ground.position.x < currentPosition)
                {
                    //Debug.Log("Áö±Ý");
                    ground.Translate(3 * groundWidth, 0, 0);
                }
            }

            float skyPosition = transform.position.x - skyWidth * 2 - 1.7f;
            foreach (Transform skies in sky)
            {
                skies.transform.position += skyMoveSpeed * Time.deltaTime * -transform.right;
                if (skies.position.x < skyPosition)
                {
                    skies.Translate(4 * skyWidth, 0, 0);
                }
            }

        }
    }
}
