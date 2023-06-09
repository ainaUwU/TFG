using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ItemData
{
    public string name;
    public Sprite icon;
    public int count;
    public int ID;
}

[System.Serializable]//para guardar en json
public class InventorySlot
{
    public Sprite itemIcon;
    public string itemName;
    public int itemCount;
    public int itemID;

    public static InventorySlot Instance { get; private set; }

    public void Awake()
    {
        Instance = this; //el inventario se inicializa en un awake
    }

    public InventorySlot(ItemData iData)
    {
        itemIcon = iData.icon;
        itemName = iData.name;
        itemCount = iData.count;
        itemID = iData.ID;

    }
    public void AddToStack(int amount)
    {
        itemCount += amount;
    }
}