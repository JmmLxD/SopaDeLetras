using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WordDisplayer : MonoBehaviour
{
    public Text palabraUI;
    List<PalabraDeSopa> palabras;
    public Button left;
    public Button right;
    int idx = 0;

    void Start()
    {
        palabras = GameManager.instance.sopaActiva.palabrasActivas;
        GameManager.instance.onSelectionTest += setWord;
        setWord();
    }

     void OnDestroy()
    {
        GameManager.instance.onSelectionTest -= setWord;
    }

    void Update()
    {

    }


    public void next()
    {
        if(idx == palabras.Count - 1)
            return;            
        if(idx == 0)
            left.GetComponent<Image>().color = new Color(0.157f,0.157f,0.157f,1f);
        idx++;
        if(idx == palabras.Count - 1)
            right.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f,0.8f);
        setWord();
    }

    public void setWord(bool pass = true,int palIdx = 0)
    {
        if(palabras[idx].finded)
            palabraUI.color = Color.green;
        else
            palabraUI.color = Color.black;
        palabraUI.text = palabras[idx].palabra;
    }


    public void prev()
    {
        if(idx == 0)
            return;            
        if(idx == palabras.Count - 1)
            right.GetComponent<Image>().color = new Color(0.157f,0.157f,0.157f,1f);
        idx--;
        if(idx == 0)
            left.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f,0.8f);
        setWord();
    }
}
