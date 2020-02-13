using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetNameCanvas : MonoBehaviour
{
    public InputField input;
    void Start()
    {
        
    }

    public void confirmName()
    {
        GameManager.instance.setPlayerName(input.text);
        GameManager.instance.changeScene("MainMenu");
    } 

    void Update()
    {
        
    }
}
