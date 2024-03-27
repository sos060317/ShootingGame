using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    int level;
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>(); // 자식의 모든 Transform
    }

    private void Update()
    {
        // 스폰 및 스폰 딜레이
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);

        // 레벨에 따라 스폰 시간 설정
        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        // 랜덤 스폰포인트에서 스폰
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<EnemyBase>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    // 적 스탯
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}