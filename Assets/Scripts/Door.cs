using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Quaternion MoveAngle;
    public bool Unlocked;
    
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
        if (other.tag == "Player" && !Unlocked)
        {
            other.GetComponent<Inventory>().KeyUsable(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Inventory>().KeyUnusable();
        }
    }
}
