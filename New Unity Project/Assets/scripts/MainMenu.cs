using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button continueBtn;
    void Start()
    {
        if(GameManager.instance.sopaActiva == null)
        {
            continueBtn.interactable = false;
        }
    }

    void Update()
    {
        
    }
}
