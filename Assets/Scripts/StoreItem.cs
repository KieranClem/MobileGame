using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem : MonoBehaviour
{
    [HideInInspector] public Item StoredItem;
    public bool Used;
    public Inventory inven;

    private void Start()
    {
        inven = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void DropItem()
    {
        if (Used != false)
        {
            inven.RemoveFromList(this, StoredItem, false);
        }
    }
}
