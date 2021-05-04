using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Inventory : MonoBehaviour
{

    public List<Item> Collected = new List<Item>();
    public GameObject DisplayHolder;
    [HideInInspector]public List<StoreItem> ItemDisplay = new List<StoreItem>();
    public Button UseButton;
    private Text UseButtonText;
    private int NumOfKeys;
    private Door DoorToUnlock;

    // Start is called before the first frame update
    void Start()
    {
        //Adds the slots into an array untill all the slots are used up
        for (int i = 0; i < DisplayHolder.transform.childCount; i++)
        {
            ItemDisplay.Add(DisplayHolder.transform.GetChild(i).GetComponent<StoreItem>());
        }
        UseButtonText = UseButton.GetComponentInChildren<Text>();
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
                if (NewItem.ItemName == "Key")
                {
                    NumOfKeys += 1;
                    DisplayNumberOfKeys();
                }
                return;
            }
        }
    }

    public void RemoveFromList(StoreItem Holder, Item RemoveItem, bool ItemUsed)
    {
        Collected.Remove(RemoveItem);
        if(RemoveItem.ItemName == "Key")
        {
            NumOfKeys -= 1;
            DisplayNumberOfKeys();
        }
        
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

    public void KeyUsable(Door door)
    {
        DoorToUnlock = door;
        UseButton.interactable = true;
        UseButton.onClick.AddListener(delegate { UnlockDoor(); });
        DisplayNumberOfKeys();
    }

    public void KeyUnusable()
    {
        DoorToUnlock = null;
        UseButton.interactable = false;
        UseButtonText.text = "";
    }

    private void DisplayNumberOfKeys()
    {
        UseButtonText.text = "Use Key\n Keys left: " + NumOfKeys;
    }

    public void UnlockDoor()
    {
        if(DoorToUnlock.Unlocked == false && NumOfKeys != 0)
        {
            foreach (Item item in Collected)
            {
                if (item.ItemName == "Key")
                {
                    DoorToUnlock.Unlocked = true;
                    RemoveFromList(item.holder, item, true);
                    DoorToUnlock.GetComponent<NavMeshObstacle>().enabled = false;
                    return;
                }
            }
        }
    }
}
