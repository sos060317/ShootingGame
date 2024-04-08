using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items =GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next()
    {
        foreach(Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        // �迭 ����
        int[] ran = new int[3];

        // �ߺ��� �����ϰ� ��Ҹ� �������� ����
        Random random = new Random();
        var uniqueRandomNumbers = ran.OrderBy(x => random.Next()).Take(3);

        Debug.Log("Unique Random Numbers:");
        foreach (var number in uniqueRandomNumbers)
        {
            Debug.Log(number);
        }
    }
}
