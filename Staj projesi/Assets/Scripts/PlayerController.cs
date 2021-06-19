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
    public bool isObstacle;

    public bool isRight;
    [SerializeField] float jumpPower;



    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody>();
        isAlive = true;
        isRight = true;
        isGrounded = true;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        if (isGrounded)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
            myAnimator.SetBool("Run", true);
        }
        else
        {
            myAnimator.SetBool("Run", false);
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
                    myBody.velocity = new Vector3(myBody.velocity.x+5, jumpPower);
                    //myBody.AddForce(Vector3.up * jumpPower * Time.deltaTime);

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
