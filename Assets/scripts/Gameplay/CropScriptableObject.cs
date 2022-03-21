using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="ScriptableObjects/CropScriptableObject")]
public  class CropScriptableObject : ScriptableObject 
{
    public enum Shape
    {
        CUBE,
        CIRCLE,
        TRINGLE
    }

    public Sprite itemSprite;
    public Shape shape;
    public int blueScore;
    public int redScore;
    public int yellowScore;
    public int greenScore;
    public int purpleScore;
    public CropScriptableObject[] evolvesInto;
}
