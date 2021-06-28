using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int currentRecruit;
    public Text recruitText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddRecruit(int recruitAdd)
    {
        currentRecruit += recruitAdd;

        recruitText.text = "Recruit: " + currentRecruit + "/35";

    }
}
