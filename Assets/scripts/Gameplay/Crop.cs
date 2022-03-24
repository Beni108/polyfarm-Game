using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Crop : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    [SerializeField] private Canvas canvas;
  
   public CropScriptableObject croptype;

    private Collider2D thisColldier;
    private RectTransform rectTransform;
    private Vector3 originalRect;
    private CanvasGroup canvasGroup;
    

    private void Awake()
    {
        thisColldier = GetComponent<Collider2D>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalRect = transform.position;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (thisColldier.OverlapPoint(mousePosition))
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                canvasGroup.blocksRaycasts = false;
                Debug.Log("beginDrag");
            }
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
        //rectTransform.anchoredPosition = originalRect;
        canvasGroup.blocksRaycasts = true;
        Debug.Log("endDrag");
    }
    public void turnRaycaston()
    {
        canvasGroup.blocksRaycasts = !canvasGroup.blocksRaycasts;
        ;

    }
    public void OnPointerDown(PointerEventData eventData)
    {

        Debug.Log("OnPointerDown");
    }

    private void OnMouseDown()
    {
        Debug.Log("onMousedown");
    }
    public void setLocation()
    {
        originalRect = transform.position;
    }
    public void DestorySelf()
    {
        Destroy(gameObject);
    }
    public bool touching(Collider2D col)
    {
        return thisColldier.IsTouching(col);
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
