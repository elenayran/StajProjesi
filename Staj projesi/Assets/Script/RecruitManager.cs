using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitManager : MonoBehaviour
{
    public int value;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetTrigger("move");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            FindObjectOfType<GameManager>().AddRecruit(value);

            Destroy(gameObject);
        }
        
    }
}
