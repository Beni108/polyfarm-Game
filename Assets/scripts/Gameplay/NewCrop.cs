using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using croptype;

public class NewCrop : MonoBehaviour
{
    public CropScriptableObject cropOS;

    private Collider2D thisCollder;
    private bool dragging = false;
    private Vector2 offsset;
    private Vector2 originalPosition;
 
    // Start is called before the first frame update
    private void Awake()
    {
        SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();
        thisSprite.sprite = cropOS.itemSprite;
        refreshCollider();
        originalPosition = transform.position;
    }
    public void refreshFruit()
    {
        transform.position = originalPosition;
        SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();
        thisSprite.sprite = cropOS.itemSprite;
        refreshCollider();
    }
    public void refreshCollider()
    {
        Collider2D[] checkColliders = GetComponents<Collider2D>();
        if(checkColliders.Length>0 || checkColliders !=null)
        {
            foreach (Collider2D c in checkColliders)
            {
                Destroy(c);
            }
        }
        thisCollder = gameObject.AddComponent<PolygonCollider2D>();
        thisCollder.isTrigger = true;

    }
    private void OnMouseOver()
    {
    
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("clicked" + Input.GetMouseButton(0));
            offsset = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
            dragging = true;
        }
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log("clicked" + Input.GetMouseButton(1));
            Time.timeScale = 0;
            GameManger.instance.ActivatePanel(this.transform.gameObject);
       
        }
    }
    private void OnMouseUp()
    {
        dragging = false;
       transform.position = originalPosition;
    }
    public Vector2 getOriginalPosition()
    {
        return originalPosition;
    }
    public void setFruit(CropScriptableObject newtype)
    {
        cropOS = newtype;
    }
    public void setOriginalPosition(Vector2 newpos)
    {
        originalPosition = newpos;
    }
    public void setNewfruit(CropScriptableObject newtype,Vector2 newpos)
    {
        setOriginalPosition(newpos);
        setFruit(newtype);
        refreshFruit();
    }
    public void evolve(CropScriptableObject targetCrop)
    {
        IAction evolvingCommand = new Evolve(this.gameObject,targetCrop); ;
        Debug.Log(originalPosition + " fruit has evolved");
        GameManger.instance.ExecuteCommand(evolvingCommand);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dragging)
        {
            var mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition- offsset;
        }
    }
}
