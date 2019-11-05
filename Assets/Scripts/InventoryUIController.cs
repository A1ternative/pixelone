using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] Cell[] cells;
    [SerializeField] private int cellCount;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Transform rootParent;


    // Start is called before the first frame update
    void Init()
    {
        cells = new Cell[cellCount];
        for (int i = 0; i < cellCount; i++)
        {
           cells[i] = Instantiate(cellPrefab, rootParent);
            
        } // весь этот блок инициализирует префабы ячеек инвентаря   
        cellPrefab.gameObject.SetActive(false); //если будет в цикле, то не будет выключаться при загрузке сцены
    }

    private void OnEnable() // метод срабатывает при подключении обьекта (есть ещё OnDisable), но может сработать и раньше старта 
    {
        if (cells == null || cells.Length <= 0)
            Init();
        var inventory = GameManager.Instance.inventory;
        for (int i = 0; i < inventory.Items.Count; i++)
        {
            if (i < cells.Length)
                cells[i].Init(inventory.Items[i]);
        }
    }
}
