using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{

    [SerializeField] float minX, maxX;
    [SerializeField] float minY, maxY;

    public Transform target;
    public float smoothspeed = 0.125f;
    public Vector3 offset;
  


    private void FixedUpdate()
    {

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothspeed);
        
        transform.position = new Vector3(Mathf.Clamp(smoothedPosition.x, minX, maxX), Mathf.Clamp(smoothedPosition.y, minY, maxY), smoothedPosition.z);

    }

}