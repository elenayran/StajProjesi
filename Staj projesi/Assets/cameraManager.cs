using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    public Transform playerTransform;
   
    [SerializeField] float minY, maxY;

    //public float cameraSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(playerTransform.position.y, minY, maxY), transform.position.z);

    }
}
