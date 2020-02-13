using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    public Button button;
    public GameObject reavealBtn;
    public SopaDisplayer displayer;
    public Text levelDisplay;
    public Text playerNameDisplay;

    void Start()
    {
        levelDisplay.text = "Level :" + (GameManager.instance.level + 1);
        playerNameDisplay.text = "Jugador :" + GameManager.instance.playerName;
    }



    public void revealBtnClick()
    {
        Destroy(reavealBtn);
        displayer.revealWord();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
