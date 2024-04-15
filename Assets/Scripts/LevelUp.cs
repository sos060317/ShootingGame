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
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);
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

        // �迭 ����
        int[] ran = new int[3];

        // �ߺ��� �����ϰ� ��Ҹ� �������� ����
        Random random = new Random();
        var randomItems = items.OrderBy(x => random.Next()).Take(3);

        foreach (var item in randomItems)
        {
            // �ִ� ������ ��� �Һ� ���������� ��ü
            if (item.level == item.data.damages.Length)
                items[4].gameObject.SetActive(true);
            else
                item.gameObject.SetActive(true);

        }
    }
}
