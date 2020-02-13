using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetterText : MonoBehaviour
{
    public Text text;
    public int x;
    public int y;
    public char ch;
    public Animator animator;

    private int renderCount;
    BoxCollider2D coll;
    void Awake()
    {
        renderCount = 0;
    }

    void Update()
    {
        if(renderCount == 1)
        {
            RectTransform rectT = gameObject.transform.parent.GetComponent<RectTransform>(); 
            
            coll.size = new Vector2(rectT.rect.width,rectT.rect.height);
        }

        if(renderCount < 2)
            renderCount++;
    }

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    public void setLetter(int x,int y,char ch,int size)
    {
        this.x = x;
        this.y = y;
        this.ch = ch;
        text.text = this.ch.ToString();
        text.fontSize = 150 - size * 4;
    }


}
