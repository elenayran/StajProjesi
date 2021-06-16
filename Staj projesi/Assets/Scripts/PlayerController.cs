using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private float mySpeedX;
    [SerializeField] float speed;
    

    private Rigidbody myBody;
    private Vector3 defaultLocalScale;
    private Animator myAnimator;



    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody>();
        
       
    }

    void Update()
    {
        //Debug.Log(Input.GetAxis("Horizontal"));
        /*mySpeedX = Input.GetAxis("Horizontal")*/;
        //myAnimator.SetFloat("Speed", Mathf.Abs(mySpeedX));
        //myBody.velocity = new Vector2(mySpeedX * speed, myBody.velocity.y);




        //if (mySpeedX > 0)
        //{
        //    transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        // }

        // else if (mySpeedX < 0)
        //{
        //    transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        //}
        transform.position += transform.forward * Time.deltaTime * speed;
        myAnimator.SetTrigger("Run");



    }
        public void PlayerInput(GlobalVariables.TouchTypes touchtype, float x) {
        Debug.Log(touchtype);
        switch (touchtype)
        {
            case GlobalVariables.TouchTypes.SWIPE_UP:
                Debug.Log("Zýpladý!");
              
             
                break;
            case GlobalVariables.TouchTypes.SWIPE_LEFT:
                Debug.Log("Sola gitti!");
                if (transform.rotation == Quaternion.Euler(0, 90, 0))
                {
                    Debug.Log("Sola bakýyor");
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                    

                }
                
                break;
            case GlobalVariables.TouchTypes.SWIPE_RIGHT:
                Debug.Log("Saða gitti!");
                if (transform.rotation == Quaternion.Euler(0, -90, 0))
                {
                    Debug.Log("Saða bakýyor");
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                  
                   

                }
                break;
        }
    }
}
