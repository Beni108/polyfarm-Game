using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllLevels
{
    private static AllLevels instance = null;
  
    public static AllLevels Instance
    {
        get
        {
            if(instance==null)
            {
                instance = new AllLevels();
               
            }
            return instance;
        }
    }

    private List<LevelInfo> levelList;
    // FIRST  array is circles : Codes B+x+x
    // SECOND  array is cubes : Codes R+x+x
    // THIRD  array is tringle : Codes Y+x+x
    LevelInfo level1 = new LevelInfo(
        new string[12]{"B0","","","","","","","","","","",""},
        new string[12] { "R0", "", "", "", "", "", "", "", "", "", "", "" },
        new string[12] { "", "", "", "", "", "", "", "", "", "", "", "" },
        new int[5] {1,1,0,0,0}
        );
    public AllLevels()
    {
        levelList = new List<LevelInfo>();
        levelList.Add(level1);
        instance = this;
    }
    public LevelInfo getLevel(int i)
    {
        return levelList[i - 1];
    }
}
