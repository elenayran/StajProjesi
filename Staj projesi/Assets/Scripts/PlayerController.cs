using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float mySpeedX;
    [SerializeField] float speed;

    private bool canDoubleJump;
    private Rigidbody myBody;
   
    private Animator myAnimator;
    public bool isGrounded;
    public bool isWall;
    public bool isAlive;
    [SerializeField] float jumpPower;


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody>();
        isAlive = true;


    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        if (isGrounded==true && isWall==false)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
            myAnimator.SetTrigger("Run");

        }
        else if ((isGrounded==false && isWall==true) ||(isGrounded==false && isWall==false) || (isGrounded==true && isWall==true))
        {

            
        }
        


    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;

        }
        else if (other.gameObject.tag == "wall")
        {
            isWall = true;
        }

        else if (other.gameObject.tag == "Fire")
        {
            EndDeathAnim();
        }
        else if (other.gameObject.tag == "Recruit") 
        {

        }

    }

    public void EndDeathAnim()
    {
        myAnimator.SetTrigger("Die");
        Debug.Log("playar öldü");
        isAlive = false;




    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
            myAnimator.SetTrigger("Slide");
        }

        else if (other.gameObject.tag=="wall")
        {
            isWall = false;

        }
    }
        public void PlayerInput(GlobalVariables.TouchTypes touchtype, float x) {
        Debug.Log(touchtype);
        switch (touchtype)
        {
            case GlobalVariables.TouchTypes.SWIPE_UP:
                Debug.Log("Zýpladý!"); 
                if (isGrounded)
                {
                    myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                    //myBody.AddForce(Vector3.up * jumpPower* Time.deltaTime);

                    myAnimator.SetTrigger("Jump");
                    Debug.Log("zýplýyorrrrrrrr");
                    isGrounded = false;
                    
                }
              
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
