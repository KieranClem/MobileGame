using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    private Quaternion MoveAngle;
    private bool Unlocked;
    
    // Start is called before the first frame update
    void Start()
    {
        MoveAngle = this.transform.rotation * Quaternion.AngleAxis(-90, Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        if (Unlocked)
        {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, MoveAngle, 1f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Inventory PlayerInven = other.GetComponent<Inventory>();
            foreach(Item item in PlayerInven.Collected)
            {
                if(item.ItemName == "Key")
                {
                    Unlocked = true;
                    PlayerInven.RemoveFromList(item.holder, item, true);
                    this.GetComponent<NavMeshObstacle>().enabled = false;
                    return;
                }
            }
        }
    }
}
