using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TouchScript : MonoBehaviour
{
    private Collider2D coll;
    private LetterText letter;
    Text text;

    void Start()
    {
        coll = GetComponent<Collider2D>();
        text = GetComponent<Text>();
        letter = GetComponent<LetterText>();
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position); 
            Collider2D touchedCollider = Physics2D.OverlapPoint(touch.position);

            

            if(touchedCollider == coll )
            {
                if( touch.phase == TouchPhase.Began)
                {
                    GameManager.instance.letterSelected(letter.x,letter.y);
                }
                    
            }       
        }
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition; 
            Collider2D touchedCollider = Physics2D.OverlapPoint(mousePos);
            if(touchedCollider == coll )
            {
                GameManager.instance.letterSelected(letter.x,letter.y);       
            }   
        }

    }
}
