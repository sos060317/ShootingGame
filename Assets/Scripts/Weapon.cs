using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
                break;
            default:
                break;
        }

        if (Input.GetButtonDown("Jump"))
            LevelUp(20, 5);
    }
    
    // 무기별 초기화
    public void Init()
    {
        switch(id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                break;
        }
    }

    // 근접 무기 설정
    private void Batch()
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;
            
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1); // -1은 관통력 무한(근접 무기)
        }
    }

    // 레벨 업(무기 데미지, 개수 추가)
    private void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if(id == 0)
            Batch();
    }
}
