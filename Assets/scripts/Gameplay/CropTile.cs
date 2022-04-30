using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CropTile : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler,IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private GameObject Tile1;
    [SerializeField]
    private GameObject Tile2;
    [SerializeField]
    private GameObject soil;
    [SerializeField]
    private GameObject selection;
 
   


    [SerializeField] private Canvas canvas;

    private GameObject fruit;
    public CropScriptableObject cropOS;
    public String TileID;
    private bool draggable = true;

    private RectTransform rectTransform;
    private Vector3 originalRect;
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    private void Awake()
    {

        canvas = transform.root.Find("Canvas").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        Tile1.SetActive(true);
        Tile2.SetActive(false);
        selection.SetActive(false);
        //soil.SetActive(false);
        fruit = soil.transform.Find("fruit").gameObject;
        setFruit(cropOS);
       
     
    }
    public void switchTile()
    {
        Tile1.SetActive(!Tile1.activeSelf);
        Tile2.SetActive(!Tile2.activeSelf);
    }
    public void setFruit(CropScriptableObject s )
    {
        //originalRect = rectTransform.anchoredPosition;
        if(fruit==null) fruit = soil.transform.Find("fruit").gameObject;
        fruit.SetActive(true);
        cropOS = s;
        Image thisimage = fruit.GetComponent<Image>();
        thisimage.sprite = cropOS.itemSprite;
        thisimage.alphaHitTestMinimumThreshold = 0.1f;
        calcPivot(cropOS);
        rectTransform = fruit.GetComponent<RectTransform>();
        originalRect = rectTransform.anchoredPosition;
    }
    public Vector2 getOriginalPosition()
    {
        return originalRect;
    }
    public void fruiteaten()
    {
        fruit.SetActive(false);
    }
    public void fruitUNeaten()
    {
        fruit.SetActive(true);
    }
    public void setID(String id)
    {
        TileID=id;
    }
    public void setNewFruit(CropScriptableObject s, String id)
    {
        soil.SetActive(false);
        if (s != null)
        {
            soil.SetActive(true);
            setFruit(s);

        }
        if(s==null)
        {
            cropOS = null;
           
        }
       
        setID(id);
    }
    public void evolve(CropScriptableObject targetCrop)
    {
        IAction evolvingCommand = new EvolveUi(this.gameObject, targetCrop); ;
        Debug.Log(this.transform + " fruit has evolved");
        if (draggable == false) evolvingCommand.ExecuteCommand();
        else
        GameManger.instance.ExecuteCommand(evolvingCommand);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selection.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selection.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       switch (eventData.button)
        {
            case PointerEventData.InputButton.Right:
                if (fruit.activeSelf && soil.activeSelf)
                {
                    if (draggable == false) EditorHandler.instance.ActivatePanel(this.transform.gameObject);
                    else
                        GameManger.instance.ActivatePanel(this.transform.gameObject);
                }
                break;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (fruit.activeSelf && soil.activeSelf && draggable) {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                canvasGroup.blocksRaycasts = false;
                fruit.transform.SetParent(canvas.transform);
                Debug.Log("beginDrag");
            }

            else eventData.pointerDrag = null;
        }
        else eventData.pointerDrag = null;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        fruit.transform.SetParent(soil.transform);
        rectTransform.anchoredPosition = originalRect;
        canvasGroup.blocksRaycasts = true;
        Debug.Log("endDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            PointerEventData pointerData = (PointerEventData)eventData;
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerData.position, canvas.worldCamera, out position);
            fruit.transform.position = canvas.transform.TransformPoint(position);
        }
    }
    public void calcPivot(CropScriptableObject s)
    {
        Sprite sprite = s.itemSprite;

        Vector2 pivot = sprite.pivot / sprite.rect.size;
        RectTransform thisrec = fruit.GetComponent<RectTransform>();
        //Vector3 deltaposition = thisrec.pivot - pivot;
        var offset = pivot - thisrec.pivot;
        //deltaposition.Scale(thisrec.rect.size);
        //deltaposition.Scale(thisrec.localScale);

        offset.Scale(thisrec.rect.size);
        var worldpos = thisrec.position + thisrec.TransformVector(offset);

        //fruit.GetComponent<RectTransform>().pivot =pivot;
        thisrec.pivot = pivot;
        thisrec.position = worldpos;
        //fruit.GetComponent<RectTransform>().localPosition -= deltaposition;




    } 
    public void undraggable()
    {
        draggable = false;
    }
}
