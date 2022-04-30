using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelInfo 
{
    // animals 0- circle   1- cube    2-tringle;
    public bool[] animals=new bool[3] { false, false, false };
    public string[,] cubefruits = new string[2, 3];
    public string[,] circlefruits = new string[2, 3];
    public string[,] tringlefruit = new string[2, 3];
    public int[] goal = new int[5];

    public TextForPanel Onstart=null;
    public TextForPanel OnUndo=null;
    public TextForPanel OnOver=null;

    


    // circle cube tringle
    public LevelInfo(string[] cirf,string[] cf,string[] tf,int[] g)
    {
        setstring(cf, this.cubefruits,1);
        setstring(cirf, this.circlefruits, 0);
        setstring(tf, this.tringlefruit, 2);
        goal = (int[])g.Clone();

    }
    public LevelInfo(string[] circlefruits, string[] cubefruits, string[] tringlefruit, int[] goal,TextForPanel Onstart,TextForPanel OnUndo,TextForPanel OnOver)
    {
        Debug.Log(circlefruits);
        setstring(cubefruits, this.cubefruits, 1);
        setstring(circlefruits, this.circlefruits, 0);
        setstring(tringlefruit, this.tringlefruit, 2);
        this.goal = (int[])goal.Clone();
        this.Onstart = Onstart;
        this.OnUndo = OnUndo;
        this.OnOver = OnOver;

    }

    [Newtonsoft.Json.JsonConstructor]
    public LevelInfo(bool[] animals,string[,] circlefruits, string[,] cubefruits, string[,] tringlefruit, int[] goal, TextForPanel Onstart, TextForPanel OnUndo, TextForPanel OnOver)
    {
        this.animals = animals;
        this.cubefruits = cubefruits;
        this.circlefruits = circlefruits;
        this.tringlefruit = tringlefruit;

        this.goal = (int[])goal.Clone();
        this.Onstart = Onstart;
        this.OnUndo = OnUndo;
        this.OnOver = OnOver;

    }

    public void setPanels(TextForPanel OS, TextForPanel OU, TextForPanel OV)
    {
        Onstart = OS;
        OnUndo = OU;
        OnOver = OV;
    }
    public void setstring(string[] s,string[,] sm,int animalnum)
    {
        for ( int i=0; i<2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                sm[i, j] = s[i * 3 + j];
                if (animals[animalnum]==false && !s[i*3+j].Equals(""))
                {
                    animals[animalnum] = true;
                }
            }
        }
    }
}
