using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WordActions : MonoBehaviour
{
    public GameObject cancelSelBtn;
    public GameObject applyBtn;
    public Text message;
    bool firstLetter;
    bool secondLetter;
    void Start()
    {
        cancelSelBtn.SetActive(false);
        applyBtn.SetActive(false);
        GameManager.instance.onLetterSelected += onLetterSelected;
        GameManager.instance.onSelectionTest += onSelectionTest;
        firstLetter = false;
        secondLetter = false;

    }

    void OnDestroy()
    {
        GameManager.instance.onLetterSelected -= onLetterSelected;
        GameManager.instance.onSelectionTest -= onSelectionTest;
    }
    

    void onSelectionTest(bool pass,int idx)
    {
        if(pass)
            message.text = "Palabra Correcta!";
        else
            message.text = "Palabra Incorreta!";
        cancelSelBtn.SetActive(false);
        applyBtn.SetActive(false);
        firstLetter = false;
        secondLetter = false;
    }

    private void onLetterSelected(int x,int y)
    {
        if(!firstLetter && !secondLetter)
        {
            cancelSelBtn.SetActive(true);
            firstLetter = true;
            message.text = "selecciona la ultima letra de la palabra";
        }
        else if(firstLetter && !secondLetter)
        {
            secondLetter = false;
            message.text = "confirma la palabra";
            applyBtn.SetActive(true);
        }
    }

    public void confirmSelection()
    {
        GameManager.instance.confirmSelection();
    }

    public void cancelSelection()
    {
        GameManager.instance.cancelSelection();
    }



    void Update()
    {
        
    }
}
