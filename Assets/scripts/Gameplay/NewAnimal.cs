using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewAnimal : MonoBehaviour
{
    public AnimalScriptableObject animalType;

    private Collider2D thisCollder;
    private bool collided=false;
    private GameObject collidedObj;
    // Start is called before the first frame update
    private void Awake()
    {

        SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();
        thisSprite.sprite = animalType.sprite;
        refreshCollider();
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && collided && collidedObj.tag=="Crop")
        {
            eat(collidedObj);
        }
    }
    private void OnMouseUp()
    {

    }
    private void refreshCollider()
    {
        thisCollder=gameObject.AddComponent<PolygonCollider2D>();
        thisCollder.isTrigger = true;

    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log(animalType.animalName+" tries to eat "+collision.gameObject.GetComponent<NewCrop>().cropOS.name);
    //    Debug.Log(Input.GetMouseButton(0));
    //    if (!Input.GetMouseButton(0))
    //    {
    //        if (eat(collision.gameObject.GetComponent<NewCrop>().cropOS))
    //        {
    //            Destroy(collision.gameObject);
    //        }

    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collided = true;
        collidedObj = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collided = false;
        collidedObj = null;
    }

    public void eat(GameObject gameobjectFruit)
    {
        CropScriptableObject fruit = gameobjectFruit.GetComponent<NewCrop>().cropOS;
        if (fruit.shape==animalType.eatsShape)
        {
            IAction eatingCommand = new Eat(gameobjectFruit, ScoreManager.instance.GetScore()); ;
            Debug.Log(animalType.name + " ate fruit, collided "+collided);
            GameManger.instance.ExecuteCommand(eatingCommand);
            //generatePoints(fruit);
            //Debug.Log(animalType.name + " ate fruit");
            return;
        }
        Debug.Log(animalType.name + " didnt eat fruit "+collided);
    }
    public void generatePoints(CropScriptableObject fruit)
    {
        ScoreManager.instance.AddScore(fruit.blueScore, fruit.redScore, fruit.yellowScore, fruit.greenScore, fruit.purpleScore);
    }

}
