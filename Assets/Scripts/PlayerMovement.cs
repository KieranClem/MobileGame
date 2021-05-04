using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public NavMeshAgent Nav;
    public Inventory items;
    public PuzzleHandler puzzleHandler;
    public Button UseButton;
    private Text UseButtonText;
    
    // Start is called before the first frame update
    void Start()
    {
        Nav = this.GetComponent<NavMeshAgent>();
        UseButtonText = UseButton.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
           if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
           {
                if(hit.collider.gameObject.tag == "Floor" || hit.collider.gameObject.tag == "Door")
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
        else if(other.tag == "KeyPanel" && !puzzleHandler.KeyPanelSolved)
        {
            UseButton.onClick.RemoveAllListeners();
            UseButton.onClick.AddListener(delegate { puzzleHandler.OpenKeyPanel(); });
            UseButton.interactable = true;
            UseButtonText.text = "Open Key \n Panel";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "KeyPanel")
        {
            UseButton.onClick.RemoveAllListeners();
            UseButton.interactable = false;
            UseButtonText.text = "";
        }
    }
}
