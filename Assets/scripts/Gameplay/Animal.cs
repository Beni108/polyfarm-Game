using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Animal : MonoBehaviour,IDropHandler
{
    public AnimalScriptableObject animalType;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool eat()
    {
        return false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Ondrop");
    }
}
