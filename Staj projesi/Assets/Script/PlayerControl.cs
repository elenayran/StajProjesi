using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float mySpeed;
    [SerializeField] private float speed;
    private Rigidbody myBody;
    private Animator myAnimator;
    private Vector3 defaultLocalScale;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        defaultLocalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        mySpeed = Input.GetAxis("Horizontal");
        myAnimator.SetFloat("Speed", Mathf.Abs(mySpeed));
        myBody.velocity = new Vector3(mySpeed * speed, myBody.velocity.y,myBody.velocity.z);



        #region playerýn sað ve sol hareket yönine göre dönmesi

        if (mySpeed > 0)
        {
            transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        else if (mySpeed < 0)
        {
            transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }

        #endregion

    }

}

