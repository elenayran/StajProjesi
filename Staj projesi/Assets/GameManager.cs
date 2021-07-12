using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int currentRecruit;
    public Text recruitText;

    public RecruitManager[] recruitManagers;
    // Start is called before the first frame update
    void Start()
    {
        recruitManagers = FindObjectsOfType<RecruitManager>();
        recruitText.text = "Recruit: " + currentRecruit + "/" + recruitManagers.Length;
    }

    public void AddRecruit(int recruitAdd)
    {
        currentRecruit += recruitAdd;

        recruitText.text = "Recruit: " + currentRecruit ;

    }
}
