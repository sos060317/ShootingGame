using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // 초기화
    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if (per >= 0)
        {
            rigid.velocity = dir * 15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적과 충돌체크 및 남은 관통력 체크
        if (!collision.CompareTag("Enemy") || per == -100)
            return;

        per--; // 한번 적과 충돌될때 관통력 감소

        // 모든 관통력 소모시 풀로 돌아감
        if (per < 0)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || per == -100)
            return;

        gameObject.SetActive(false);
    }
}
