using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public PlayerController player;
    private Rigidbody myBody;
    public float speed;
    public bool isStop;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        isStop = true;
    }

    // Update is called once per frame
    private void Update()
    {

        if (!isStop)
        {
            myBody.velocity = Vector3.zero;
        }
        else
        {
            transform.position += Vector3.down * speed * Time.deltaTime;

            Debug.Log("aþaðýya doðru ileriliyor");
            
        }
       
        
    }


   
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            player.EndDeathAnim();
            isStop = false;
           

        }
    }
}
