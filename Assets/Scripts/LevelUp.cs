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
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
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

    private void Next()
    {
        foreach(Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        // 배열 생성
        int[] ran = new int[3];

        // 중복을 제거하고 요소를 무작위로 선택
        Random random = new Random();
        var randomItems = items.OrderBy(x => random.Next()).Take(3);

        foreach (var item in randomItems)
        {
            // 최대 레벨일 경우 소비 아이템으로 교체
            if (item.level == item.data.damages.Length)
                items[4].gameObject.SetActive(true);
            else
                item.gameObject.SetActive(true);

        }
    }
}
