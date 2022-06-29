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
        birdInputAction = new();                // new Bird()와 같음
        rigid = GetComponent<Rigidbody2D>();    //미리 캐싱
        //anim = GetComponent<Animator>();
        rigid.gravityScale = 0f;


        menu = FindObjectOfType<InGame_Menu>();
        menu.onPlayButton = EnableInputs;
    }

    private void OnEnable()
    {
        birdInputAction.Player.Disable();                // 스크립트로 Input System 제어 시, 활성화/비활성화를 해줘야 함.
        birdInputAction.Player.Fly.performed += OnFly;  // Fly 액션 발동 시 실행될 함수 등록
    }

    private void OnDisable()
    {
        birdInputAction.Player.Fly.performed -= OnFly;  // Fly 액션 발동 시 실행될 함수 등록 해제
        birdInputAction.Player.Disable();
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, 0.0f, 2 * Camera.main.orthographicSize));

        if (!GameManager.Inst.IsGameOver)
        {
            float vel = Mathf.Clamp(rigid.velocity.y, -jumpPower, jumpPower);       // vel -jumpPower ~ jumpPower까지 제한
                                                                                    //float velNormalized = (vel + jumpPower) / (jumpPower * 2);              // vel 정규화 ( 0 ~ 1)
                                                                                    //float angle = (velNormalized * pitchMaxAngle * 2) - pitchMaxAngle;      // 속도와 각도 매칭
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
    //    if (rigid.velocity.y < 0.0f)   // 속도가 0보다 작을 때 : 떨어질 때
    //    {
    //        anim.SetBool("onJump", false);
    //    }
    //}
    void OnFly(InputAction.CallbackContext context)
    {
        rigid.velocity = Vector2.zero;                  // 이전 속도값 초기화 (떨어지는 속도 무시)
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        //Velocity.y 범위 -10 ~ 10
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
