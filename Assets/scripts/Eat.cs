using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : IAction
{
    private GameObject fruitEaten;
    private CropScriptableObject fruitType;
    private Vector2 originalLocation;
    private int[] score;
    public Eat(GameObject fruit, int[] s)
    {
        this.fruitEaten = fruit;
        this.fruitType = fruitEaten.GetComponent<NewCrop>().cropOS;
        this.originalLocation = fruit.GetComponent<NewCrop>().getOriginalPosition();
        score = s;
    }
    public void ExecuteCommand()
    {
       CropScriptableObject fruit = fruitType;
       ScoreManager.instance.AddScore(fruit.blueScore, fruit.redScore, fruit.yellowScore, fruit.greenScore, fruit.purpleScore);
       UnityEngine.Object.Destroy(fruitEaten);
    }

    public void UndoCommand()
    {
        GameObject fruitPrefab = Resources.Load("Prefab/fruit") as GameObject;
        GameObject newFruit=GameObject.Instantiate(fruitPrefab);
        newFruit.GetComponent<NewCrop>().setNewfruit(this.fruitType, this.originalLocation);
        newFruit.transform.SetParent(GameObject.Find("GameHandler").transform);
        ScoreManager.instance.setScore(score);
    }

}
