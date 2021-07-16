using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startMenu : MonoBehaviour
{
    public bool isGameStarted;
    public Transform GameStartButton;
    private Button Quitbutton;
    private void Start()
    {
        Quitbutton = GetComponent<Button>();
        //Quitbutton.onClick.AddListener(QuitButton);
        
    }


    public void startButton()
    {
        isGameStarted = true;
        GameStartButton.gameObject.SetActive(false);
    }

   


}
