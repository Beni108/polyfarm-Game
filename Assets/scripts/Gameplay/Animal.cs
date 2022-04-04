using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using croptype;
public class Animal : MonoBehaviour,IDropHandler
{
    public AnimalScriptableObject animalType;


    private void Awake()
    {
        Image thisimage = this.GetComponent<Image>();
        thisimage.sprite = animalType.sprite;
        thisimage.alphaHitTestMinimumThreshold = 0.1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool eat(CropScriptableObject fruit)
    {
        if(animalType.eatsShape==fruit.shape)
        {
            generatePoints(fruit);
            return true;
        }
        Debug.Log(animalType.name + " didnt eat fruit");
        return false;
    }
    //public bool eat(CropScriptableObject fruit)
    //{

    //    switch (animalType.eatsShape)
    //    {
    //        case AnimalScriptableObject.Shape.CIRCLE:
    //            if (fruit.shape == CropScriptableObject.Shape.CIRCLE)
    //            {
    //                generatePoints(fruit);
    //                return true;
    //            }
    //            break;
    //        case AnimalScriptableObject.Shape.CUBE:
    //            if (fruit.shape == CropScriptableObject.Shape.CUBE)
    //            {
    //                generatePoints(fruit);
    //                return true;
    //            }
    //            break;
    //        case AnimalScriptableObject.Shape.TRINGLE:
    //            if (fruit.shape == CropScriptableObject.Shape.TRINGLE)
    //            {
    //                generatePoints(fruit);
    //                return true;
    //            }
    //            break;
    //    }
    //    Debug.Log(animalType.name + " didnt eat fruit");
    //    return false;
    //}
    public void generatePoints(CropScriptableObject fruit)
    {
        Debug.Log(animalType.name + " ate fruit");
        ScoreManager.instance.AddScore(fruit.blueScore,fruit.redScore,fruit.yellowScore,fruit.greenScore,fruit.purpleScore);
    }
    
    public void OnDrop(PointerEventData eventData)
    {
            
        Debug.Log("Ondrop");
        
            if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<Crop>() != null)
            {
            //if (  eventData.pointerDrag.GetComponent<Crop>().touching(thisColldier))
            //{
            GameObject fruit = eventData.pointerDrag;
            IAction eatingCommand = new EatUi(fruit, ScoreManager.instance.GetScore()); ;
            Debug.Log(animalType.name + " ate fruit, collided " + fruit);
            GameManger.instance.ExecuteCommand(eatingCommand);
            //if (eat(eventData.pointerDrag.GetComponent<Crop>().cropOS))
            //{
            //    eventData.pointerDrag.GetComponent<Crop>().setLocation();
            //    eventData.pointerDrag.GetComponent<Crop>().DestorySelf();
            //}
            //}
        }
        
    }
   
}
