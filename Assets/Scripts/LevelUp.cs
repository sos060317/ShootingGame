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

        // 배열 생성
        int[] ran = new int[3];

        // 중복을 제거하고 요소를 무작위로 선택
        Random random = new Random();
        var uniqueRandomNumbers = ran.OrderBy(x => random.Next()).Take(3);

        Debug.Log("Unique Random Numbers:");
        foreach (var number in uniqueRandomNumbers)
        {
            Debug.Log(number);
        }
    }
}
