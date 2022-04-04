using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using croptype;
using System.Linq;
using System;

public class EvolutionPanel : MonoBehaviour
{
    public CropScriptableObject originCrop;
    public CropScriptableObject currentCrop;
    [SerializeField]
    private GameObject evoButton;
    public Text title;


    private GameObject cropObj;
    private GameObject releventTree;
    private GameObject currentSelected;
    private Color selectedColor = new Color32(236,228,41,255);
    private Color originColor = new Color32(225, 255, 255, 255);
    void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        evoButton.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Setup(GameObject crop)
    {

        transform.gameObject.SetActive(true);
        cropObj = crop;
        originCrop = crop.GetComponent<CropTile>().cropOS;
        currentCrop = originCrop;
        setText(System.Enum.GetName(typeof(Shape),originCrop.shape));
        setButtons(originCrop);
    }
    public void evolveCropOSonClick(GameObject cropButton)
    {
        currentSelected.GetComponent<EvolutionButton>().unselectChangeColor();
        currentSelected = cropButton;
        currentSelected.GetComponent<EvolutionButton>().selectedChangeColor();
        currentCrop = currentSelected.GetComponent<EvolutionButton>().cropOS;

    }
    public void setButtons(CropScriptableObject crop)
    {
        if (releventTree != null) releventTree.SetActive(false);
        switch (crop.shape)
        {
            case Shape.CUBE:
                releventTree = transform.Find("background").Find("cubeTree").gameObject;
                break;
            case Shape.CIRCLE:
                releventTree = transform.Find("background").Find("circleTree").gameObject;
                break;
            case Shape.TRINGLE:
                releventTree = transform.Find("background").Find("tringleTree").gameObject;
                break;
        }
        
        releventTree.SetActive(true);
        Button[] allbuttons = releventTree.GetComponentsInChildren<Button>();
        List<CropScriptableObject> allowedevolution=getAllowedEvolutions(crop);
        foreach (Button b in allbuttons)
        {
            b.gameObject.GetComponent<EvolutionButton>().SetOriginPanel(this);
         
            if (!allowedevolution.Contains(b.gameObject.GetComponent<EvolutionButton>().cropOS))
            {
                b.interactable = false;
            }
            else
            {
                b.interactable = true;
                if(b.gameObject.GetComponent<EvolutionButton>().cropOS==crop)
                {
                    b.GetComponent<EvolutionButton>().selectedChangeColor();
                    currentSelected = b.gameObject;
                }
            }
        }
    }

    private List<CropScriptableObject> getAllowedEvolutions(CropScriptableObject crop)
    {
        List<CropScriptableObject> cropList=new List<CropScriptableObject>();
        cropList.Add(crop);
        if(crop.evolvesInto !=null)
        {
            foreach (CropScriptableObject c in crop.evolvesInto)
            {
                cropList.AddRange(getAllowedEvolutions(c));
             }
        }
        return cropList;
    }
    public void evovleIntoSelected()
    {
        if (currentCrop !=originCrop) {
            cropObj.GetComponent<CropTile>().evolve(currentCrop);
            close();
        }
    }
    public void setText(string S)
    {
        title.text = S + " EVOLUTION";
    }
    public void close()
    {
        currentSelected.GetComponent<EvolutionButton>().unselectChangeColor();
        releventTree.SetActive(false);
        transform.gameObject.SetActive(false);
    }
}
