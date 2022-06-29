using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Pillar : MonoBehaviour
{
    Rigidbody2D rigid = null;

    public float min = 0.0f;
    public float max = 5.0f;

    float randomHeight = 0.0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        ResetHeight();
    }

    public void ResetHeight()
    {
        randomHeight = Random.Range(min, max);
        //위와 같은 코드임.
        //rigid.MovePosition(new Vector2(transform.position.x, Random.Range(min, max)));
    }

    public void Move(Vector2 displacement)
    {
        Vector2 pos = new(rigid.position.x, randomHeight);
        rigid.MovePosition(pos + displacement);
    }

    public void Set(Vector2 pos)
    {
        transform.position = pos;
    }
}
