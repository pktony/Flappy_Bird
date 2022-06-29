using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlappyBird : MonoBehaviour
{
    BirdAction birdInputAction = null;
    Rigidbody2D rigid = null;
    //Animator anim = null;
    InGame_Menu menu = null;

    public System.Action onGameover = null;

    public float jumpPower = 5.0f;
    public float pitchMaxAngle = 45.0f;
    public float reflectionPower = 5.0f;

    private void Awake()
    {
        birdInputAction = new();                // new Bird()�� ����
        rigid = GetComponent<Rigidbody2D>();    //�̸� ĳ��
        //anim = GetComponent<Animator>();
        rigid.gravityScale = 0f;


        menu = FindObjectOfType<InGame_Menu>();
        menu.onPlayButton = EnableInputs;
    }

    private void OnEnable()
    {
        birdInputAction.Player.Disable();                // ��ũ��Ʈ�� Input System ���� ��, Ȱ��ȭ/��Ȱ��ȭ�� ����� ��.
        birdInputAction.Player.Fly.performed += OnFly;  // Fly �׼� �ߵ� �� ����� �Լ� ���
    }

    private void OnDisable()
    {
        birdInputAction.Player.Fly.performed -= OnFly;  // Fly �׼� �ߵ� �� ����� �Լ� ��� ����
        birdInputAction.Player.Disable();
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, 0.0f, 2 * Camera.main.orthographicSize));

        if (!GameManager.Inst.IsGameOver)
        {
            float vel = Mathf.Clamp(rigid.velocity.y, -jumpPower, jumpPower);       // vel -jumpPower ~ jumpPower���� ����
                                                                                    //float velNormalized = (vel + jumpPower) / (jumpPower * 2);              // vel ����ȭ ( 0 ~ 1)
                                                                                    //float angle = (velNormalized * pitchMaxAngle * 2) - pitchMaxAngle;      // �ӵ��� ���� ��Ī
            float angle = vel * pitchMaxAngle / jumpPower;

            rigid.MoveRotation(angle);
        }
        //CheckFalling();
    }

    void EnableInputs()
    {
        birdInputAction.Player.Enable();
        GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        //anim.SetTrigger("GameStart");
    }

    void Die()
    {
        birdInputAction.Player.Disable();
        
        //anim.SetTrigger("Dead");
    }

    //void CheckFalling()
    //{
    //    if (rigid.velocity.y < 0.0f)   // �ӵ��� 0���� ���� �� : ������ ��
    //    {
    //        anim.SetBool("onJump", false);
    //    }
    //}
    void OnFly(InputAction.CallbackContext context)
    {
        rigid.velocity = Vector2.zero;                  // ���� �ӵ��� �ʱ�ȭ (�������� �ӵ� ����)
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        //Velocity.y ���� -10 ~ 10
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();

        ContactPoint2D contact = collision.GetContact(0);
        Vector2 dir = (contact.point - (Vector2)transform.position).normalized;
        Vector2 reflect = Vector2.Reflect(dir, contact.normal);
        rigid.velocity = reflect * reflectionPower;
        rigid.AddTorque(15.0f);

        GameManager.Inst.IsGameOver = true;
        onGameover?.Invoke();
    }
}
