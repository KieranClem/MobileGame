using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent nav;
    private bool QuickTimeActive;
    private bool QuickTimeSuccessful;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        nav = this.GetComponent<NavMeshAgent>();
        Vector3 firstDestination = new Vector3(this.transform.position.x + 5, this.transform.position.y, this.transform.position.z);
        nav.SetDestination(firstDestination);
        QuickTimeActive = false;
        QuickTimeSuccessful = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!nav.pathPending)
        {
            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                if (nav.hasPath || nav.velocity.sqrMagnitude == 0f)
                {
                    TurnAround();
                }
            }
        }

        //QTE System
        if (QuickTimeActive)
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                QuickTimeActive = false;
                QuickTimeSuccessful = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, 10))
        {
            if(hit.collider.tag == "Player")
            {
                nav.SetDestination(hit.transform.position);
            }
        }
    }

    void TurnAround()
    {
        transform.Rotate(Vector3.back);
        nav.destination = (transform.forward * 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(QTESystem(other.GetComponent<PlayerMovement>()));
        }
    }

    IEnumerator QTESystem(PlayerMovement Player)
    {
        Player.Nav.isStopped = true;
        QuickTimeActive = true;
        yield return new WaitForSeconds(2f);
        QuickTimeActive = false;
        if (QuickTimeSuccessful)
        {
            Player.Nav.isStopped = false;
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Lost");
        }
    }
}
