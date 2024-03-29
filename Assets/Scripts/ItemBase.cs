﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Databases/Items")]
public class ItemBase : ScriptableObject
{
    [SerializeField, HideInInspector] private List<Item> items;

    [SerializeField] private Item currentItem;
    private int currentIndex;

    public void CreateItem()
    {
        if (items == null)
            items = new List<Item>();

        Item item = new Item();
        items.Add(item);
        currentItem = item;
        currentIndex = items.Count - 1;
    }

    public void RemoveItem()
    {
        if (items == null || currentItem == null)
            return;

        items.Remove(currentItem);
        if (items.Count > 0)
            currentItem = items[0];
        else CreateItem();
        currentIndex = 0;
    }

    public void NextItem()
    {
        if (currentIndex + 1 < items.Count)
            currentIndex++;        
    }

    public void PrevItem()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            currentItem = items[currentIndex];
        }          
    }

    public Item GetItemOfID(int id)
    {
        //foreach (var item in items)
        //{
        //    if (item.ID == id)
        //        return item;
        //}
        //return null; // если ничего не нашли возвращаем нул
        // но можно по другому

        return items.Find(t => t.ID == id);    // метод Find принимает в себя лямбда выражения
        // по листу items перебираем t элементы, если (t.ID == id) это true, то возвращаем t элемент
    }
}

[System.Serializable]
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
    [SerializeField] private Sprite icon;
    public Sprite Icon
    {
        get { return icon; }
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
