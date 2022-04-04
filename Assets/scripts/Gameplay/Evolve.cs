using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evolve : IAction
{
    private GameObject evolvedfruit;
    private CropScriptableObject sourceFruit;
    private CropScriptableObject evolvesInto;

    public Evolve(GameObject fruit,CropScriptableObject target)
    {
        evolvedfruit = fruit;
        sourceFruit = evolvedfruit.GetComponent<NewCrop>().cropOS;
        evolvesInto = target;
    }

  public void ExecuteCommand()
    {

        evolvedfruit.GetComponent<NewCrop>().setFruit(evolvesInto);
        evolvedfruit.GetComponent<NewCrop>().refreshFruit();

    }

    public void UndoCommand()
    {
        evolvedfruit.GetComponent<NewCrop>().setFruit(sourceFruit);
        evolvedfruit.GetComponent<NewCrop>().refreshFruit();
    }

    // Start is called before the first frame update
    
}
