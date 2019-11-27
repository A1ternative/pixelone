using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEmitter : MonoBehaviour
{
    [SerializeField] private Buff buff; //не будет отображаться на компоненте в юнити, пока класс не будет сериализован

    private void OnTriggerEnter2D(Collider2D col)
    {
        var reciever = GameManager.Instance.buffRecieverContainer[col.gameObject];
        reciever.AddBuff(buff);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        var reciever = GameManager.Instance.buffRecieverContainer[col.gameObject];
        reciever.RemoveBuff(buff);
    }
}

[System.Serializable]  // означает что ниже описанный класс может быть сериализован
public class Buff
{
    public BuffType type;
    public float additiveBonus;
    public float multipleBonus;
}

public enum BuffType : byte
{
    Damage, Force, Armor
}
