using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Crop : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    [SerializeField] private Canvas canvas;
   public CropScriptableObject croptype;

    private RectTransform rectTransform;
    private Vector3 originalRect;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalRect = transform.position;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            canvasGroup.blocksRaycasts = false;
            Debug.Log("beginDrag");
        }

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
     
    public void DestorySelf()
    {
        Destroy(gameObject);
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
