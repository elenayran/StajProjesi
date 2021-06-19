using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class wallManager : MonoBehaviour
{
    public float speed;
    private Rigidbody myBody;
    Collider iscollider;
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        //myBody.velocity = RandomVector(0f, 5f);




    }

    // Update is called once per frame
    void Update()
    {

    

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag== "Player")
        {

            MoveObje();
           
            Destroy(gameObject,5);
            Debug.Log("yokoldu");


        }
    }
    public void MoveObje()
    {
        myBody.AddForce(new Vector2(transform.up.x * 700, transform.up.y * 700));
    }
       
        
    

}
