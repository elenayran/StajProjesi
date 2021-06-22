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
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothspeed);
        transform.position = smoothedPosition;
        
        transform.position = new Vector3(Mathf.Clamp(target.position.x, minX, maxX), target.position.y + 3f, transform.position.z);



    }

}