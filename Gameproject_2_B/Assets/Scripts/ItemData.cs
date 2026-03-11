using UnityEngine;
using System;

using UnityEditor.Experimental.GraphView;

[Serializable]
public class ItemData
{
    public int id;
    public string itemName;
    public string description;
    public string nameEng;
    public string itemTypeString;

    [NonSerialized]
    public ItemType itemType;
    public int price;
    public int power;
    public int level;
    public bool isStackable;
    public string iconPath;

    //문자열을 열거형으로 변환 하는 메서드

    public void IntalizeEnums()
    {

        if (Enum.TryParse(itemTypeString, out ItemType parseType))
        {
            itemType = parseType;
        }
        else


        {
            Debug.LogError($"아이템 '{itemName} 에 유효하지 않은 아이템 타입 : {itemTypeString}");
            //기본값 설정 
            itemType = ItemType.Consumable;

        }

    }  
}
