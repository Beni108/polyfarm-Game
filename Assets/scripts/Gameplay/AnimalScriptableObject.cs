using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "ScriptableObjects/AnimalScriptableObject")]
public class AnimalScriptableObject : ScriptableObject 
{
    public enum Shape
    {
        CUBE,
        CIRCLE,
        TRINGLE
    }
    public Sprite sprite;
    public string animalName;
    public Shape eatsShape;
  
}
