using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] float speed;

   
    private Rigidbody myBody;

    private Animator myAnimator;
    public bool isGrounded;
    public bool isAlive;
    public bool isWall;

    public bool isRight;
    [SerializeField] float jumpPower=2.0f;
    public Vector3 jump;
    public bool isStace;
   



    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody>();
        isAlive = true;
        isRight = true;
        isGrounded = true;
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        //isWall = true;
        isStace = true;

    }

    void Update()
    {
        if (!isStace)
        {
            return;

        }
        if (!isAlive)
        {
            return;
        }

        if (isGrounded )
        {
            transform.position += transform.forward * Time.deltaTime * speed;
            myAnimator.SetBool("Run", true);
            Debug.Log("zeminde koþuyorrrrrrrr");
        }
        else
        {
            myAnimator.SetBool("Run", false);
            Debug.Log("koþma durdu");
        }
    }

 
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;

        }
        else if (other.gameObject.tag == "leftWall")
        {
            Debug.Log("duvarla temas etti");
            isRight = true;
            TurnRight();

        }
        else if (other.gameObject.tag == "rightWall")
        {
            Debug.Log("duvarla temas etti");
            isRight = false;
            TurnLeft();
        }
        else if (other.gameObject.tag=="wall")
        {
            Stace();
            //isWall = true;

        }

        else if (other.gameObject.tag == "Fire")
        {
            EndDeathAnim();
        }
        else if (other.gameObject.tag == "Recruit")
        {

        }
        else if (other.gameObject.tag == "Obstacle")
        {

        }



    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }

    public void EndDeathAnim()
    {
        myAnimator.SetTrigger("Die");
        Debug.Log("playar öldü");
        isAlive = false;
    }
    public void Stace()
    {
        myAnimator.SetBool("Run", false);
        Debug.Log("player konumu wall");
        isStace = false;
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
            myAnimator.SetBool("Run", false);
        }
    }

    public void PlayerInput(GlobalVariables.TouchTypes touchtype, float x)
    {
        Debug.Log(touchtype);



        switch (touchtype)
        {
            case GlobalVariables.TouchTypes.SWIPE_UP:
                Debug.Log("Zýpladý!");
                if (isGrounded)
                {
                    myBody.AddForce(jump *jumpPower, ForceMode.Impulse);
                   
                    //myBody.velocity = new Vector3(myBody.velocity.x, jumpPower);
                    ////myBody.AddForce(Vector3.up * jumpPower * Time.deltaTime);

                    myAnimator.SetTrigger("Jump");
                    Debug.Log("zýplýyorrrrrrrr");
                    isGrounded = false;

                }
                break;
            case GlobalVariables.TouchTypes.SWIPE_LEFT:

               
                  if (isRight)
                  {
                    isRight = false;
                    Debug.Log("Sola döndürüldü");
                    TurnLeft();
                  }
               
                break;
            case GlobalVariables.TouchTypes.SWIPE_RIGHT:
                Debug.Log("Saða gitti!");
               
                    if (!isRight)
                    {
                    isRight = true;
                    Debug.Log("Saða döndürüldü");
                    TurnRight();
                    }
                                
                break;


        }
    }

    public void TurnLeft()
    {
        transform.rotation = Quaternion.Euler(0, -90, 0);

        
        
    }

    public void TurnRight()
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
       
    }
}
