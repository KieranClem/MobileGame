using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent Nav;
    public Inventory items;
    
    // Start is called before the first frame update
    void Start()
    {
        Nav = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
           if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
           {
                if(hit.collider.gameObject.tag == "Floor")
                {
                    Nav.destination = hit.point;
                }
                else if(hit.collider.gameObject.tag == "Item" && hit.collider.GetComponent<Item>().CanAccess == true)
                {
                    Nav.destination = hit.point;
                }
           }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            items.CollectItem(other.GetComponent<Item>());
        }
    }
}
