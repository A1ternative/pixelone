using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : ScriptableObject
{
    [SerializeField] private List<Item> items;

    [SerializeField] private Item currentItem;

    public void CreateItem()
    {
        if (items == null)
            items = new List<Item>();

        Item item = new Item();
        items.Add(item);
        currentItem = item;
    }

    public void RemoteItem()
    {
        if (items == null || currentItem == null)
            return;

        items.Remove(currentItem);
        if (items.Count > 0)
            currentItem = items[0];
        else CreateItem();
    }
}

public class Item
{ 
    // [SerializeField, HideInInspector] - если требуется сохранять какие либо обьекты сцены, но скрыть их редактирование из инспектора
    [SerializeField] private int id;
    public int ID
    {
        get { return id; }
    }
    [SerializeField] private string itemName;
    public string ItemName
    {
        get { return itemName; }
    }
    [SerializeField] private string description;
    public string Description
    {
        get { return description; }
    }
    [SerializeField] private BuffType type;
    public BuffType Type
    {
        get { return type; }
    }
    [SerializeField] private float value;
    public float Value
    {
        get { return value; }
    }
}
