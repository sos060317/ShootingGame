using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class EnemyBase : MonoBehaviour
{
    public float speed;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive = true;

    protected Health health;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    private void Awake()
    {
        health = GetComponent<Health>();

        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Debuff"))
            return;

        // �̵�
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive)
            return;

        // �ø�
        spriter.flipX = target.position.x > rigid.position.x;
    }

    private void OnEnable()
    {
        // �ʱ�ȭ
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        health.onDie += DieAtion;
        GameManager.instance.enemyAllClear += DieAtion;
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
    }

    // �ʱ�ȭ
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �Ѿ˰� �浹
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health.TakeDamage(collision.GetComponent<Bullet>().damage);
        StartCoroutine(KnockBack());

        if (health.HP > 0)
        {
            // ��Ʈ ���
            anim.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else
        {
            // �״� ���
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            GameManager.instance.kill++;
            GameManager.instance.GetExp();

            if(GameManager.instance.isLive)
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
        }
    }

    // �ڷ� �з���
    private IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    private void DieAtion()
    {
        anim.SetBool("Dead", true);
        health.onDie -= DieAtion;
        GameManager.instance.enemyAllClear -= DieAtion;
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }
}
