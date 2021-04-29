using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public List<Item> Collected = new List<Item>();
    public GameObject DisplayHolder;
    [HideInInspector]public List<StoreItem> ItemDisplay = new List<StoreItem>();

    // Start is called before the first frame update
    void Start()
    {
        //Adds the slots into an array untill all the slots are used up
        for (int i = 0; i < DisplayHolder.transform.childCount; i++)
        {
            ItemDisplay.Add(DisplayHolder.transform.GetChild(i).GetComponent<StoreItem>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectItem(Item NewItem)
    {
        Collected.Add(NewItem);
        NewItem.collected = true;
        NewItem.GetComponent<Renderer>().enabled = false;
        NewItem.GetComponent<Collider>().enabled = false;
        foreach(StoreItem holder in ItemDisplay)
        {
            if(holder.Used == false)
            {
                holder.StoredItem = NewItem;
                NewItem.holder = holder;
                holder.Used = true;
                holder.GetComponent<Image>().color = Color.green;
                return;
            }
        }
    }

    public void RemoveFromList(StoreItem Holder, Item RemoveItem, bool ItemUsed)
    {
        Collected.Remove(RemoveItem);
        if (!ItemUsed)
        {
            RemoveItem.GetComponent<Renderer>().enabled = true;
            RemoveItem.GetComponent<Collider>().enabled = true;
        }
        else
        {
            Destroy(RemoveItem.gameObject);
        }
        Holder.StoredItem = null;
        Holder.Used = false;
        Holder.GetComponent<Image>().color = new Color(255, 255, 255);
    }
}
