using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RecruitManager : MonoBehaviour
{
    public GameObject text;
    public int value;
    private Animator animator;
    public float speed;
   
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
            
            //text.SetActive(true);
           
            StartCoroutine(Wait());
        
        }

    }
   
    public IEnumerator Wait()
    {
            GameObject t =(GameObject) Instantiate(text, Canvas.transform);
       
            yield return new WaitForSecondsRealtime(0.5f);
            Destroy(gameObject);
            t.SetActive(false);       
    }
}
