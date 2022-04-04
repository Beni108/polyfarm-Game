using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using croptype;

[CreateAssetMenu(menuName = "ScriptableObjects/AnimalScriptableObject")]

public class AnimalScriptableObject : ScriptableObject 
{
  
    public Sprite sprite;
    public string animalName;
    public Shape eatsShape;
  
}

