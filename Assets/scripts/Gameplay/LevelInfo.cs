using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo 
{
    // animals 0- circle   1- cube    2-tringle;
    public bool[] animals=new bool[3] { false, false, false };
    public string[,] cubefruits = new string[2, 6];
    public string[,] circlefruits = new string[2, 6];
    public string[,] tringlefruit = new string[2, 6];
    public int[] goal = new int[5];

  // circle cube tringle
    public LevelInfo(string[] cirf,string[] cf,string[] tf,int[] g)
    {
        setstring(cf, this.cubefruits,1);
        setstring(cirf, this.circlefruits, 0);
        setstring(tf, tringlefruit, 2);
        goal = (int[])g.Clone();

    }
    
    public void setstring(string[] s,string[,] sm,int animalnum)
    {
        for ( int i=0; i<2; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                sm[i, j] = s[i * 6 + j];
                if (animals[animalnum]==false && !s[i*6+j].Equals(""))
                {
                    animals[animalnum] = true;
                }
            }
        }
    }
}
