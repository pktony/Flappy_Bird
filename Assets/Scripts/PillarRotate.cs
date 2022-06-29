using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarRotate : MonoBehaviour
{
    Pillar[] children = null;

    public float pillarSpace = 4.0f;

    public float moveSpeed = 3.0f;

    const float boundary = -11.0f;

    private void Awake()
    {
        children = new Pillar[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).GetComponent<Pillar>();
            children[i].transform.Translate(pillarSpace * i * Vector2.right);
            children[i].Set((pillarSpace * i + 10.0f) * Vector2.right);
            children[i].ResetHeight();
        }
    }

    private void FixedUpdate()
    {
        PositionReset();
    }

    void PositionReset()
    {
        if (!GameManager.Inst.IsGameOver)
        {
            foreach (Pillar pillar in children)
            {
                pillar.Move(Time.fixedDeltaTime * moveSpeed * Vector2.left);

                if (pillar.transform.position.x < boundary)
                {
                    pillar.Move(pillarSpace * transform.childCount * Vector2.right);
                    pillar.ResetHeight();
                }
            }
        }
    }
}
