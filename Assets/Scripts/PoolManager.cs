using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리팹 보관할 변수
    public GameObject[] prefabs;

    // 풀 담당을 하는 리스트들
    List<GameObject>[] pools;

    private void Awake()
    {
        // 프리펩의 개수만큼 풀 초기화
        pools = new List<GameObject>[prefabs.Length];

        // 각 풀들 초기화
        for(int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 풀(index)의 비활성화 된 게임 오브젝트에 접근
        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                // 해당 오브젝트를 select에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //  없다면(선택한 풀에 비활성화 된 게임 오브젝트가 없다면)
        if (!select)
        {
            // 새롭게 생성하고 select에 할당하고 부모를 해당 오브젝트로 지정
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select); // 추가한 오브젝트를 리스트에 등록
        }

        return select; // 반환
    }
}