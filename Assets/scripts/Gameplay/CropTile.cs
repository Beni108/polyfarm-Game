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
    private int TileID;


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

        rectTransform = fruit.GetComponent<RectTransform>();
        originalRect = rectTransform.anchoredPosition;
        setFruit(cropOS);
        calcPivot(cropOS);
    }
    public void switchTile()
    {
        Tile1.SetActive(Tile1.activeSelf);
        Tile2.SetActive(Tile2.activeSelf);
    }
    public void setFruit(CropScriptableObject s )
    {
        //originalRect = rectTransform.anchoredPosition;
        cropOS = s;
        Image thisimage = fruit.GetComponent<Image>();
        thisimage.sprite = cropOS.itemSprite;
        thisimage.alphaHitTestMinimumThreshold = 0.1f;
    }

    public void setID(int id)
    {
        TileID=id;
    }
    public void setNewFruit(CropScriptableObject s, int id)
    {

        if (s != null)
        {
            soil.SetActive(true);
            setFruit(s);

        }
       
        setID(id);
    }
    public void evolve(CropScriptableObject targetCrop)
    {
        IAction evolvingCommand = new EvolveUi(this.gameObject, targetCrop); ;
        Debug.Log(this.transform + " fruit has evolved");
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
                GameManger.instance.ActivatePanel(this.transform.gameObject);
                break;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            canvasGroup.blocksRaycasts = false;
           
            Debug.Log("beginDrag");
        }

        else eventData.pointerDrag = null;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      // rectTransform.anchoredPosition = originalRect;
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
        Bounds bounds = sprite.bounds;
        var pivotX = bounds.center.x / bounds.extents.x / 2 + 0.5f;
        var pivotY = bounds.center.y / bounds.extents.y / 2 + 0.5f;
        var pixelsToUnits = sprite.textureRect.width / bounds.size.x;
        fruit.GetComponent<RectTransform>().pivot = new Vector2(pivotX,pivotY);

    }
}
