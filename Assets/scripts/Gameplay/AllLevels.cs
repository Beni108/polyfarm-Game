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
    // FIRST  array is cub : Codes B+x+x
    // SECOND  array is cubes : Codes R+x+x
    // THIRD  array is tringle : Codes Y+x+x
    LevelInfo level1 = new LevelInfo(
        new string[12]{"B0","","","","","","","","","","",""},
        new string[12] { "", "", "", "", "", "", "", "", "", "", "", "" },
        new string[12] { "", "", "", "", "", "", "", "", "", "", "", "" },
        new int[5] {1,0,0,0,0}
        );
    LevelInfo level2 = new LevelInfo(
      new string[12] { "B0", "", "", "", "", "", "", "", "", "", "", "" },
      new string[12] { "R0", "", "", "", "", "", "", "", "", "", "", "" },
      new string[12] { "Y0", "", "", "", "", "", "", "", "", "", "", "" },
      new int[5] { 1, 1, 1, 0, 0 }
      );
    LevelInfo level3 = new LevelInfo(
      new string[12] { "B0", "", "", "", "", "", "", "", "", "", "", "" },
      new string[12] { "R0", "", "", "", "", "", "", "", "", "", "", "" },
      new string[12] { "", "", "", "", "", "", "", "", "", "", "", "" },
      new int[5] { 0, 3,0,0, 0 }
      );
    LevelInfo level4 = new LevelInfo(
      new string[12] { "B0", "", "", "", "", "", "", "", "", "", "", "" },
      new string[12] { "R0", "", "", "", "", "", "", "", "", "", "", "" },
      new string[12] { "Y0", "", "", "", "", "", "", "", "", "", "", "" },
      new int[5] { 1, 5, 2, 2, 0 }
      );
    LevelInfo level5 = new LevelInfo(
      new string[12] { "B0", "", "", "", "", "", "", "", "", "", "", "" },
      new string[12] { "", "", "", "", "", "", "", "", "", "", "", "" },
      new string[12] { "Y0", "", "", "", "", "", "", "", "", "", "", "" },
      new int[5] { 1, 0, 2, 1, 1 }
      );
    public int maxlevel = 0;
    public AllLevels()
    {
        levelList = new List<LevelInfo>();
        levelList.Add(level1);
        levelList.Add(level2);
        levelList.Add(level3);
        levelList.Add(level4);
        levelList.Add(level5);
        maxlevel = levelList.Count;
        instance = this;

    }
    public LevelInfo getLevel(int i)
    {
        return levelList[i - 1];
    }
}
