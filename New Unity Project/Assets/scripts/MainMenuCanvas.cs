using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuCanvas : MonoBehaviour
{
    public Button continueBtn;

    public void Start()
    {
        if(GameManager.instance.sopaActiva == null)
        {
            continueBtn.interactable = false;
        }
    }

    public void ChangeName()
    {
        GameManager.instance.changeScene("GetUserName");
    }

    public void CloseApp()
    {
        GameManager.instance.CloseApp();
    }

    public void Borrar()
    {
        GameManager.instance.borrar();
    }

    public void StartNewGame()
    {
        GameManager.instance.NewGame();
    }

    public void ContinueGame()
    {
        GameManager.instance.ContinueGame();
    }
}
