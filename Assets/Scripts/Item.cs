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
        // 초기화
        icon = GetComponentsInChildren<Image>()[2];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    private void LateUpdate()
    {
        // 레벨 텍스트 초기화
        textLevel.text = "Lv." + (level + 1);
    }

    // 레벨 업
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

        // 레벨 업 제한
        if (level == data.damages.Length)
            GetComponent<Button>().interactable = false;
    }
}
