using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

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
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if(level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;
        }

        level++;

        // 레벨 업 제한
        if (level == data.damages.Length)
            GetComponent<Button>().interactable = false;
    }
}
