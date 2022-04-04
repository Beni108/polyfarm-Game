using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EvolutionButton : MonoBehaviour
{
    [SerializeField]
    public CropScriptableObject cropOS;
    [SerializeField]
    public GameObject rewardContainer;
    [SerializeField]
    public GameObject pointContainer;

    private EvolutionPanel origin;
    private ColorBlock defultCB;



    private void Awake()
    {
        defultCB=this.GetComponent<Button>().colors;
        this.transform.Find("fruit").GetComponent<Image>().sprite=cropOS.itemSprite;
        setRewardPanel(cropOS.blueScore, cropOS.redScore, cropOS.yellowScore, cropOS.greenScore, cropOS.purpleScore);

    }
    private void setRewardPanel(int s1,int s2,int s3,int s4,int s5)
    {
        Color red, blue, yellow, green, purple;
        ColorUtility.TryParseHtmlString("#FF1717", out red);
        ColorUtility.TryParseHtmlString("#1139F1", out blue);
        ColorUtility.TryParseHtmlString("#FFF217", out yellow);
        ColorUtility.TryParseHtmlString("#3BFF17", out green);
        ColorUtility.TryParseHtmlString("#8F17FF", out purple);
        var ScoreList = new List<int> { s1, s2, s3, s4, s5 };
        var colorList = new List<Color> { blue, red, yellow, green, purple };

        foreach ( var z in colorList.Zip(ScoreList,(x,y) => (Color:x,Num:y)))
        {
            
            if (z.Num>0)
            {
         
               GameObject newcontainer=Instantiate(pointContainer, rewardContainer.transform);
               GameObject point =newcontainer.transform.Find("point").gameObject;
                point.GetComponent<Image>().color = z.Color;
                for (int i=1; i<z.Num;i++)
                {
                    GameObject InstantinatedPoint= Instantiate(point, newcontainer.transform);
                    InstantinatedPoint.GetComponent<Image>().color = z.Color;
               
                }

            }
        }

    }
    public void SetOriginPanel(EvolutionPanel p)
    {
        origin = p;
    }
    public void clickedEvolve()
    {
        origin.evolveCropOSonClick(this.gameObject);
    }
    public void selectedChangeColor()
    {
        Color selectedColor = new Color32(236, 228, 41, 255);
        Button b = this.GetComponent<Button>();
        b.interactable = false;
        ColorBlock cb = b.colors;
        cb.disabledColor = selectedColor;
        b.colors = cb;
    }
    public void unselectChangeColor()
    {
        Button b = this.GetComponent<Button>();
        b.interactable = true;
        b.colors = defultCB;
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
