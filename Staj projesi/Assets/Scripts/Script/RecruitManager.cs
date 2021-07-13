using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RecruitManager : MonoBehaviour
{
    public GameObject text;
    public int value;
    private Animator animator;   
    public GameObject Canvas;
    
  
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //Panel.SetActive(false);
      
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
            StartCoroutine(Wait());
        }
    }
    public IEnumerator Wait()
    {
        GameObject t = Instantiate(text, transform.position, Quaternion.identity, null);       
        yield return new WaitForSecondsRealtime(0.2f);
        Destroy(gameObject);   
    }
}
