using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public List<Item> Collected = new List<Item>();
    public GameObject DisplayHolder;
    private List<Image> ItemDisplay = new List<Image>();

    // Start is called before the first frame update
    void Start()
    {

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
    }
}
