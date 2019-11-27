using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemBase))]
public class ItemBaseEditor : Editor
{
    private ItemBase itemBase;

    private void Awake()
    {
        itemBase = (ItemBase)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("New Item")) // кнопка создается в условном операторе
            itemBase.CreateItem();          // создаем айтем в базе, которую инициализировали в Awake

        if (GUILayout.Button("Remove Item"))
            itemBase.RemoveItem();

        if (GUILayout.Button("<="))
            itemBase.PrevItem();

        if (GUILayout.Button("=>"))
            itemBase.NextItem();

        GUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }
}
