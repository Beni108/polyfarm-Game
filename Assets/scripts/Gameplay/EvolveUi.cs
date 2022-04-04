using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolveUi : IAction
{
    private GameObject evolvedfruit;
    private CropScriptableObject sourceFruit;
    private CropScriptableObject evolvesInto;

    public EvolveUi(GameObject fruit, CropScriptableObject target)
    {
        evolvedfruit = fruit;
        sourceFruit = evolvedfruit.GetComponent<CropTile>().cropOS;
        evolvesInto = target;
    }

    public void ExecuteCommand()
    {

        evolvedfruit.GetComponent<CropTile>().setFruit(evolvesInto);
      

    }

    public void UndoCommand()
    {
        evolvedfruit.GetComponent<CropTile>().setFruit(sourceFruit);
       
    }
}
