using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // ������ ������ ����
    public GameObject[] prefabs;

    // Ǯ ����� �ϴ� ����Ʈ��
    List<GameObject>[] pools;

    private void Awake()
    {
        // �������� ������ŭ Ǯ �ʱ�ȭ
        pools = new List<GameObject>[prefabs.Length];

        // �� Ǯ�� �ʱ�ȭ
        for(int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ������ Ǯ(index)�� ��Ȱ��ȭ �� ���� ������Ʈ�� ����
        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                // �ش� ������Ʈ�� select�� �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //  ���ٸ�(������ Ǯ�� ��Ȱ��ȭ �� ���� ������Ʈ�� ���ٸ�)
        if (!select)
        {
            // ���Ӱ� �����ϰ� select�� �Ҵ��ϰ� �θ� �ش� ������Ʈ�� ����
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select); // �߰��� ������Ʈ�� ����Ʈ�� ���
        }

        return select; // ��ȯ
    }
}