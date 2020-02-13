using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Cord 
{
    public int x;
    public int y;

    public Cord(int x,int y)
    {
        this.x = x;
        this.y = y;
    }

    public string str()
    {
        return $"({x},{y})";
    }


    public static bool operator == (Cord a,Cord b)
    {
        return (a.x == b.x && a.y ==b.y);
    }

    public static bool operator != (Cord a,Cord b)
    {
        return !(a.x == b.x && a.y ==b.y);
    }

    public static bool InLine(Cord a,Cord b)
    {
        Cord delta = a - b;
        int dx = Math.Abs( delta.x );
        int dy = Math.Abs( delta.y );
        return (dx == 0 || dy == 0);
    }

    public static bool InDiagonal(Cord a,Cord b)
    {
        Cord delta = a - b;
        int dx = Math.Abs( delta.x );
        int dy = Math.Abs( delta.y );
        return (dx == dy);
    }

    public static Cord operator + (Cord a,Cord b)
    {
        return new Cord(a.x + b.x , a.y + b.y);
    }

    public static Cord UnitaryDiference(Cord str,Cord end)
    {
        Cord res = end - str;
        res.x = (res.x > 1) ? 1 : (res.x < -1) ? -1 : res.x ;
        res.y = (res.y > 1) ? 1 : (res.y < -1) ? -1 : res.y ;
        return res;
    }

    public static Cord operator * (Cord cord,int num)
    {
        return Multiply(cord,num);
    }

    public static Cord operator * (int num,Cord cord)
    {
        return Multiply(cord,num);
    }

    private static Cord Multiply(Cord cord,int num)
    {
        return new Cord(cord.x * num , cord.y * num);
    }


    public static Cord operator - (Cord a,Cord b)
    {
        return new Cord(a.x - b.x , a.y - b.y);
    }

}
