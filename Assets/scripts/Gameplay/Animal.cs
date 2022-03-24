using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Animal : MonoBehaviour,IDropHandler
{
    public AnimalScriptableObject animalType;

    private Collider2D thisColldier;

    private void Awake()
    {
        thisColldier = GetComponent<Collider2D>();
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
        
        switch (animalType.eatsShape)
        {
            case AnimalScriptableObject.Shape.CIRCLE:
                if (fruit.shape == CropScriptableObject.Shape.CIRCLE)
                {
                    generatePoints(fruit);
                    return true;
                }
                break;
            case AnimalScriptableObject.Shape.CUBE:
                if (fruit.shape == CropScriptableObject.Shape.CUBE)
                {
                    generatePoints(fruit);
                    return true;
                }
                break;
            case AnimalScriptableObject.Shape.TRINGLE:
                if (fruit.shape == CropScriptableObject.Shape.TRINGLE)
                {
                    generatePoints(fruit);
                    return true;
                }
                break;
        }
        Debug.Log(animalType.name + " didnt eat fruit");
        return false;
    }
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
            eventData.pointerDrag.GetComponent<Crop>().turnRaycaston();
            Debug.Log(  eventData.pointerDrag.GetComponent<Crop>().touching(thisColldier));
            if (  eventData.pointerDrag.GetComponent<Crop>().touching(thisColldier))
            {
                if (eat(eventData.pointerDrag.GetComponent<Crop>().croptype))
                {
                    // eventData.pointerDrag.GetComponent<Crop>().setLocation();
                    eventData.pointerDrag.GetComponent<Crop>().DestorySelf();
                }
            }
            }
        
    }
   
}
