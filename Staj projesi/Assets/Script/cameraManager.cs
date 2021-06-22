using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    
    [SerializeField] float minX, maxX;
    public Transform target;
    public float smoothspeed = 0.125f;
    public Vector3 offset;
    public float followAhead;


    private void FixedUpdate()
    {
        
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition , smoothspeed);
        transform.position = smoothedPosition;
        //transform.LookAt(target);

        transform.position = new Vector3(Mathf.Clamp(target.position.x, minX, maxX), target.position.y+3f , transform.position.z) ;

        //if (target.transform.localScale.x > 0f)
        //{
        //    offset = new Vector3(offset.x + followAhead, offset.y, offset.z);
        //}
        //else
        //{
        //    offset = new Vector3(offset.x - followAhead, offset.y, offset.z);
        //}

    }
    //public Transform playerTransform;

    //[SerializeField] float minY, maxY;

    ////public float cameraSpeed;


    //// Update is called once per frame
    //void Update()
    //{
    //    transform.position = new Vector3(transform.position.x, Mathf.Clamp(playerTransform.position.y, minY, maxY), transform.position.z);

    //}
}
