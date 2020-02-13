using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SopaDeLetras
{
    public char[,] grid;
    public List<string> words;
    private int wordAmount;
    public int size;
    public int level;




    public List<PalabraDeSopa> palabrasActivas;
    System.Random random;




    public char GetLetter(int x,int y)
    {
        return grid[y,x];
    }

    public bool IsSolved()
    {
        foreach(PalabraDeSopa pal in palabrasActivas)
        {
            if(!pal.finded)
                return false;
        }
        return true;
    }

    public int Check(string word)
    {
        int idx = -1;
        int i = 0;
        foreach(PalabraDeSopa pal in palabrasActivas)
        {
            if(pal.palabra == word )
            {
                idx = i;
                pal.finded = true;
            }
            i++;
        }
        return idx;
    }

    public SopaDeLetras(GameData data)
    {
        grid = data.grid;
        size = data.level + 7;
        wordAmount = level * 2 + 6; 
        palabrasActivas = data.palabrasActivas;
    }

    public SopaDeLetras(int level)
    {
        wordAmount = level * 2 + 6; 
        this.level = level;
        this.size = level + 7;
        palabrasActivas = new List<PalabraDeSopa>();
        random = new System.Random();
        words = new List<string>();
        grid = new char[size,size];

        List<string> palDisp = GameManager.instance.posiblesPalabras;

        for(int i = 0; i < wordAmount ; i++)
        {
            int idx = random.Next(0,palDisp.Count);
            string word = palDisp[idx];
            if(words.Contains(word))
                i--;
            else
                words.Add(word);
        }
        FillGrid();
    }

    
    

    private void FillGrid()
    {
        int cont = 0;
        char[,] oGrid = new char[size,size];
        Cord[] directions = new Cord[8];
        directions[0] = new Cord(0,-1);
        directions[1] = new Cord(1,-1);
        directions[2] = new Cord(1,0);
        directions[3] = new Cord(1,1);
        directions[4] = new Cord(0,1);
        directions[5] = new Cord(-1,1);
        directions[6] = new Cord(-1,0);
        directions[7] = new Cord(-1,-1);
        Cord dir;
        Cord start;
        Cord endPos;
        int len;
        bool[,] takedGrid = new bool[size,size];
    
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                char ch =  RandomChar();
                grid[i,j] = ch;

                takedGrid[i,j] = false;
            }
        }
        foreach(string word in words)
        {
            len = word.Length;
            while(true)
            {
                cont++;
                if(cont >= 10000)
                {
                    Debug.Log("PANIC PANIC PANIC PANIC PANIC PANIC");
                    return;
                }
                dir = directions[random.Next(0,8)];
                start = new Cord(random.Next(0,size),random.Next(0,size));

                endPos = start + dir * (len - 1);
                if( validPos(endPos) && fillWord(word,start,endPos,dir,takedGrid))
                    break;
            }
        }
    }

    private bool fillWord(string word,Cord start,Cord end,Cord dir,bool[,] tGrid)
    {
        Cord pos = start;

        for(int i = 0;true;i++)
        {
            if(tGrid[pos.y,pos.x] == true
            && grid[pos.y,pos.x] != word[i] )
            {
                return false;
            }


            if( pos == end )
                break;
            pos += dir;
            
        }
        pos = start;
        palabrasActivas.Add( new PalabraDeSopa(word,start,dir,false) );
        for(int i = 0;true;i++)
        {

            grid[pos.y,pos.x] = word[i];
            tGrid[pos.y,pos.x] = true;
            if( pos == end )
                break;
            pos += dir;
        }
        return true;
    }



    private bool validPos(Cord pos)
    {
        return ( pos.x >= 0 && pos.x < size && pos.y >= 0 && pos.y < size );
    }

    private char RandomChar()
    {
        char[] letters = "abcdefghijklmnñopqrstuvwxyz".ToCharArray();
        return letters[  random.Next(0,letters.Length)];
    }
}

[Serializable]
public class PalabraDeSopa
{
    public string palabra;
    public Cord pos;
    public Cord dir;
    public bool finded;
    public PalabraDeSopa(string palabra,Cord pos,Cord dir,bool finded)
    {
        this.palabra = palabra;
        this.pos = pos;
        this.dir = dir;
        finded = false;
    }
}

[Serializable]
public class GameData
{
    public int level;
    public string playerName;
    public char[,] grid;
    public bool hasSopa;
    public List<PalabraDeSopa> palabrasActivas;

    public GameData(SopaDeLetras sopa,string playerName)
    {
        this.playerName = playerName == null ? "" : playerName;
        if(sopa != null)
        {
            this.level = sopa.level;
            this.grid = sopa.grid;
            this.palabrasActivas = sopa.palabrasActivas;
            hasSopa = true;
        }
        else
            hasSopa = false;

    }

    public string print()
    {
        string str = $"{level}";
        foreach(PalabraDeSopa pal in palabrasActivas)
        {
            str += $"{ pal.palabra },{ pal.pos.str() },{ pal.dir.str() },{ pal.finded }";
        } 

        return str;
    }
}