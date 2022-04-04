using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Crop : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    [SerializeField] private Canvas canvas;
    public CropScriptableObject cropOS;

    private RectTransform rectTransform;
    private Vector3 originalRect;
    private CanvasGroup canvasGroup;
    

    private void Awake()
    {
        canvas = transform.root.Find("Canvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalRect = rectTransform.anchoredPosition;

        Image thisimage = this.GetComponent<Image>();
        thisimage.sprite = cropOS.itemSprite;
        thisimage.alphaHitTestMinimumThreshold = 0.1f;

    }
    public void refreshFruit()
    {
        originalRect = rectTransform.anchoredPosition;
        Image thisimage = this.GetComponent<Image>();
        thisimage.sprite = cropOS.itemSprite;
        thisimage.alphaHitTestMinimumThreshold = 0.1f;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       // if (thisColldier.OverlapPoint(mousePosition))
      
        
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                canvasGroup.blocksRaycasts = false;
                Debug.Log("beginDrag");
            }
        
        else eventData.pointerDrag = null;
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = originalRect;
        canvasGroup.blocksRaycasts = true;
        Debug.Log("endDrag");
    }
    public void OnPointerDown(PointerEventData eventData)
    {

        Debug.Log("OnPointerDown");
    }

    public void setLocation()
    {
        originalRect = transform.position;
    }
    public void DestorySelf()
    {
        Destroy(gameObject);
    }

    public Vector2 getOriginalPosition()
    {
        return originalRect;
    }
    public void setFruit(CropScriptableObject newtype)
    {
        cropOS = newtype;
    }
    public void setNewfruit(CropScriptableObject newtype, Vector2 newpos)
    {
        setOriginalPosition(newpos);
        setFruit(newtype);
        refreshFruit();
    }
    public void setOriginalPosition(Vector2 newpos)
    {
        originalRect = newpos;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
