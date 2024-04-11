using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public RuntimeAnimatorController[] animCon;

    [HideInInspector]
    public Health health;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    private void Awake()
    {
        health = GetComponent<Health>();

        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    private void OnEnable()
    {
        //anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }

    private void Start()
    {
        health.onDie += DieAtion;
    }

    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        // 플레이어 이동 인풋
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        // 플레이어 이동
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        anim.SetFloat("Speed", inputVec.magnitude);

        // 플레이어 플립
        if(inputVec.x != 0)
        {
            spriter.flipX = inputVec.x > 0;
        }
    }

    private void DieAtion()
    {
        for(int index = 2; index < transform.childCount; index++)
        {
            transform.GetChild(index).gameObject.SetActive(false);
        }

        anim.SetTrigger("Dead");
        GameManager.instance.GameOver();

        health.onDie -= DieAtion;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            health.TakeDamage(Time.deltaTime * 10);
        }
    }
}
