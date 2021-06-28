using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallManager : MonoBehaviour
{
   
    private Rigidbody myBody;
   
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        //myBody.velocity = RandomVector(0f, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag== "Player")
        {
            MoveObje();           
            Destroy(gameObject,5);
        }
    }
    public void MoveObje()
    {
        myBody.AddForce(new Vector3(0, 300, -3000));
        myBody.useGravity = true;
        Debug.Log("move objete girdi");

    }

}
