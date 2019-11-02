using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Entity/New Item")] // - атрибут, который позволяет создавать обьекты прямо в юнити
// fileName - имя файла, которое будет присвоено нашему экземпляру ScriptableObject
// menuName - путь к кнопке "создать наш айтем"
public class Item : ScriptableObject
{
    // [SerializeField, HideInInspector] - если требуется сохранять какие либо обьекты сцены, но скрыть их редактирование из инспектора
    [SerializeField] private int ID;
    [SerializeField] private string itemName;
    [SerializeField] private string description;
    [SerializeField] private BuffType type;
    [SerializeField] private float value;
}
