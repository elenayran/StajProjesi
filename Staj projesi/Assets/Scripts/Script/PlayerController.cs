using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    //public Vector3 jump;
    public bool isStace;
    [SerializeField] Vector3 rightJump;
    [SerializeField] Vector3 leftJump;
    public FireManager fireManager;
    public GameObject loseText;
    public startMenu StartMenu;
    public GameObject CompleteText;



    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody>();
        isAlive = true;
        isRight = true;
        isGrounded = true;
        //jump = new Vector3(0.0f, 2.0f, 0.0f);
        rightJump = new Vector3(-1.0f, 2.0f, 0.0f);
        leftJump = new Vector3(1.0f, 2.0f, 0.0f);

        //isWall = true;
        isStace = true;
        loseText.SetActive(false);
        CompleteText.SetActive(false);
        

        
        
    }

    void Update()
    {

        if (!StartMenu.isGameStarted)
        {
            return;
        }

        if (!isStace)
        {
            return;

        }
        if (!isAlive)
        {
            return;
        }

        if (isGrounded  )
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
            //RightJump();

        }
        else if (other.gameObject.tag == "rightWall")
        {
            Debug.Log("duvarla temas etti");
            isRight = false;
            TurnLeft();
            ////LeftJump();


        }
        else if (other.gameObject.tag == "wall")
        {
            Stace();
            //isWall = true;

        }

        else if ((other.gameObject.tag == "Fire"))
        {
            EndDeathAnim();
            Debug.Log("fire içinde");
        }
      
        
        else if (other.gameObject.tag == "Recruit")
        {

        }
        else if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("obsracle aktif");
            myAnimator.SetTrigger("Stumble");
        }

        


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="fireSmall")
        {
            EndDeathAnim();
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
            StartCoroutine(Menu());
           
    }
    public void Stace()
    {
        myAnimator.SetBool("Run", false);
        Debug.Log("player konumu wall");
        isStace = false;
        fireManager.isStop = false;
        StartCoroutine(Complete());
       
        

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
                    Debug.Log(isRight);
                    if (isRight)
                    {
                        //LeftJump();
                        myAnimator.SetTrigger("Jump");
                        Debug.Log("zýplýyorrrrrrrr");
                        isGrounded = false;
                        myBody.AddForce(leftJump * jumpPower, ForceMode.Impulse);

                    }
                    if (!isRight)
                    {
                        //RightJump();
                        myAnimator.SetTrigger("Jump");
                        Debug.Log("zýplýyorrrrrrrr");
                        isGrounded = false;
                        myBody.AddForce(rightJump * jumpPower, ForceMode.Impulse);
                    }
                    //myBody.AddForce(jump * jumpPower, ForceMode.Impulse);
                    //myBody.velocity +=transform.forward*3f;
                    

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

    public IEnumerator Menu()
    {
        loseText.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        loseText.SetActive(false);
        StartMenu.isGameStarted = false;
        restart();
    }
    public IEnumerator Complete()
    {
        Debug.Log("complete içinde");
        CompleteText.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        CompleteText.SetActive(false);
        StartMenu.isGameStarted = false;
        Stagecomplete();


    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    public void Stagecomplete()
    {
        int x = SceneManager.GetActiveScene().buildIndex + 1;
        int y = (SceneManager.sceneCountInBuildSettings);
        SceneManager.LoadScene(x % y);     
    }
}
