using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // библиотека что бы воспользоваться делигатом

public class BuffReciever : MonoBehaviour
{
    private List<Buff> buffs;
    public List<Buff> Buffs
    {
        get { return buffs; }        
    }

    public Action OnBuffsChanged; // делигат без параметров
    //что бы передавать параметры в делегат нужно объявить Action<int>, тогда TestMethod(int a) - будет иметь соответствуюший аргумент

    // Start is called before the first frame update
    private void Start()
    {
        GameManager.Instance.buffRecieverContainer.Add(gameObject, this); //
        buffs = new List<Buff>();
    }
    
    public void AddBuff(Buff buff)
    {
        if (!buffs.Contains(buff))
            buffs.Add(buff);

        if (OnBuffsChanged != null) // при измеении листа бафом, т.е. при Add или Remove ЕСЛИ cушествуют какие то методы на которые подписан делигат...
            OnBuffsChanged(); // ... то мы его выполняем
        
    }

    public void RemoveBuff(Buff buff)
    {
        if (buffs.Contains(buff))
            buffs.Remove(buff);

        if (OnBuffsChanged != null) // при измеении листа бафом, т.е. при Add или Remove ЕСЛИ cушествуют какие то методы на которые подписан делигат...
            OnBuffsChanged(); // ... то мы его выполняем
        // делигат у нас подписывается в Player скрипте
    }

}
