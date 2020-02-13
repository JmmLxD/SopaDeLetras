using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class SopaDisplayer : MonoBehaviour
{
    public GameObject letter;
    public SopaDeLetras sopa;
    private LetterText[,] letterDisplays;
    int size;
    bool firstLetterIsSelected;
    bool secondLetterIsSelected;
    Cord firstLetter;
    Cord secondLetter;

    void Start()
    {
        firstLetterIsSelected  = false;
        secondLetterIsSelected = false;
        firstLetter = new Cord(0,0);
        secondLetter = new Cord(0,0);
        sopa = GameManager.instance.sopaActiva;
        
        size = GameManager.instance.level + 7;
        doshit();
    }

    public void revealWord()
    {
        System.Random random = new System.Random();
        while(true)
        {
            int idx = random.Next(0,sopa.palabrasActivas.Count);
            PalabraDeSopa sp = sopa.palabrasActivas[idx];
            if(!sp.finded)
            {
                firstLetter = sp.pos;
                secondLetter = sp.pos + sp.dir * (sp.palabra.Length -1) ;
                GameManager.instance.confirmSelection();
                break;
            }

        }
    }

    public void  giveHint()
    {
        System.Random random = new System.Random();
        Cord pos;
        while(true)
        {
            int idx = random.Next(0,sopa.palabrasActivas.Count);
            if(!sopa.palabrasActivas[idx].finded)
            {
                pos = sopa.palabrasActivas[idx].pos;
                break;
            }

        }
        letterDisplays[pos.y,pos.x].animator.SetTrigger("hinted");
    }

    private void doshit()
    {
        
        RectTransform panel = GetComponent<RectTransform>();
        GridLayoutGroup layout = GetComponent<GridLayoutGroup>();
        float width = panel.rect.width;
        float height = panel.rect.height;
        float cellW = width / size - layout.spacing.x ;
        float cellH = height / size - layout.spacing.y ;
        layout.cellSize = new Vector2(cellW,cellH);
        letterDisplays = new LetterText[size,size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject nletter =  Instantiate(letter,Vector3.zero,Quaternion.identity,transform);
                letterDisplays[i,j] = nletter.GetComponentInChildren<LetterText>();
                letterDisplays[i,j].setLetter(j,i,sopa.GetLetter(j,i),size);
            }
        }

        foreach(PalabraDeSopa pal in sopa.palabrasActivas)
        {
            if(pal.finded)
                colorWord(pal);
        }

        GameManager.instance.onLetterSelected  += onLetterSelected;
        GameManager.instance.onCancelSelection += onCancelSelection;
        GameManager.instance.onConfirmSelection += onConfirmSelection;
    }

    private void colorWord(PalabraDeSopa palabra)
    {
        Cord pos = palabra.pos;
        Cord dir = palabra.dir;
        Cord end = pos + dir * (palabra.palabra.Length - 1);
        while(true)
        {
            letterDisplays[pos.y,pos.x].text.color = Color.green;
            if(pos == end)
                break;
            
            pos += dir;
        }
    }

    private void onConfirmSelection()
    {
        int idx = sopa.Check(getWord(firstLetter,secondLetter));
        GameManager.instance.selectionTest( idx != -1 ,idx);
        if(idx != -1)
        {
            colorWord(sopa.palabrasActivas[idx]);
        }
        onCancelSelection();
    }

    void OnDestroy()
    {
        GameManager.instance.onLetterSelected  -= onLetterSelected;
        GameManager.instance.onCancelSelection -= onCancelSelection;
        GameManager.instance.onConfirmSelection -= onConfirmSelection;
    }

    private void onCancelSelection()
    {
        firstLetterIsSelected  = false;
        secondLetterIsSelected = false;
        letterDisplays[secondLetter.y,secondLetter.x].animator.SetBool("selected",false);
        letterDisplays[firstLetter.y,firstLetter.x].animator.SetBool("selected",false);
        
    }

    private string getWord(Cord start,Cord end)
    {
        List<char> letters = new List<char>();
        Cord crrPos = start;
        Cord deltaPos = Cord.UnitaryDiference(start,end);
        while(true)
        {
            letters.Add(sopa.GetLetter(crrPos.x,crrPos.y));
            if(crrPos == end)
                break;
            crrPos = crrPos + deltaPos;
        }
        return  new string(letters.ToArray());
    } 




    private void onLetterSelected(int x,int y)
    {
        if(firstLetterIsSelected)
        {
            if( Cord.InDiagonal(new Cord(x,y),firstLetter)  || Cord.InLine(new Cord(x,y),firstLetter))
            {
                letterDisplays[y,x].animator.SetBool("selected",true);
                secondLetterIsSelected = true;
                secondLetter = new Cord(x,y);
            }
        }
        else if(!secondLetterIsSelected)
        {
            firstLetter = new Cord(x,y);
            firstLetterIsSelected = true;
            letterDisplays[y,x].animator.SetBool("selected",true);
        }

    }
}
