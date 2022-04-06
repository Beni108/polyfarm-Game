using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatUi : IAction
{
    private GameObject thistile;
    private CropScriptableObject fruitType;
    private Vector2 originalLocation;
    private int[] score;
    public EatUi(GameObject tile, int[] s)
    {
        this.thistile = tile;
        this.fruitType = tile.GetComponent<CropTile>().cropOS;
        this.originalLocation = tile.GetComponent<CropTile>().getOriginalPosition();
        score = s;
    }
    public void ExecuteCommand()
    {
        CropScriptableObject fruit = fruitType;
        GameManger.instance.fruiteaten();
        ScoreManager.instance.AddScore(fruit.blueScore, fruit.redScore, fruit.yellowScore, fruit.greenScore, fruit.purpleScore);
        thistile.GetComponent<CropTile>().fruiteaten();
    }

    public void UndoCommand()
    {
        //GameObject fruitPrefab = Resources.Load("Prefab/fruit") as GameObject;
        //GameObject soil = thistile.transform.Find("soil").gameObject;
        //GameObject newFruit = GameObject.Instantiate(fruitPrefab,soil.transform);
        //newFruit.GetComponent<RectTransform>().anchoredPosition = originalLocation;
        GameManger.instance.undofruiteaten();
        this.thistile.GetComponent<CropTile>().fruitUNeaten();
        ScoreManager.instance.setScore(score);
    }

}
