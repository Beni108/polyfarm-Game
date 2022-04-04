using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InstantiateCommand : IAction
{
    private GameObject objectChanged;
    private Vector2 originalLocation;
    private int[] score;
    public InstantiateCommand(GameObject g,Vector2 v,int[] s)
    {
        this.objectChanged = g;
        this.originalLocation = v;
        score = new int[5];
        s.CopyTo(score,0);
    }
    public void ExecuteCommand()
    {
        
    }

    public void UndoCommand()
    {
        throw new System.NotImplementedException();
    }


}
