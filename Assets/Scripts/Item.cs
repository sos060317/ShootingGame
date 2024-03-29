using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;

    Image icon;
    Text textLevel;

    private void Awake()
    {
        // �ʱ�ȭ
        icon = GetComponentsInChildren<Image>()[2];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    private void LateUpdate()
    {
        // ���� �ؽ�Ʈ �ʱ�ȭ
        textLevel.text = "Lv." + (level + 1);
    }

    // ���� ��
    public void OnClick()
    {
        switch(data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.GetComponent<Weapon>();
                    weapon.Init(data);
                }
                break;
            case ItemData.ItemType.Glove:
                break;
            case ItemData.ItemType.Shoe:
                break;
            case ItemData.ItemType.Heal:
                break;
        }

        level++;

        // ���� �� ����
        if (level == data.damages.Length)
            GetComponent<Button>().interactable = false;
    }
}