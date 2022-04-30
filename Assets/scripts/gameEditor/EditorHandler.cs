using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EditorHandler : MonoBehaviour
{
    public static EditorHandler instance;

    [SerializeField]
    private Text savedAsText;

    [SerializeField]
    private GameObject evolutionpanel;
    [SerializeField]
    private GameObject gridPanel;
    [SerializeField]
    private GameObject croptile;
    [SerializeField]
    private PointEditor bluePoints;
    [SerializeField]
    private PointEditor redPoints;
    [SerializeField]
    private PointEditor yellowPoints;
    [SerializeField]
    private PointEditor greenPoints;
    [SerializeField]
    private PointEditor purplePoints;

    
    private InfoPanel OnStart;
    private InfoPanel OnUndo;
    private InfoPanel OnOver;

    [SerializeField]
    private PanelButton onStartButton;
    [SerializeField]
    private PanelButton onUndoButton;
    [SerializeField]
    private PanelButton onOverButton;

    [SerializeField]
    private GameObject panelEditorOrigin;

    [SerializeField]
    private Button circleButton;
    [SerializeField]
    private Button cubeButton;
    [SerializeField]
    private Button tringleButton;


    private CropScriptableObject[] allfruit;

    private GameObject[] firstRowFruits;
    private GameObject[] secondRowFruits;
    private GameObject[] thirdRowFruits;

    private int firstRowCount = 0;
    private int secondRowCount = 0;
    private int thirdRowCount = 0;

    private GameObject[] animals;
    private LevelInfo thislevel = null;
    public Dictionary<string, CropScriptableObject> IDtoCropSO;
    private int fruitsInScene = 0;
    private void Awake()
    {
        savedAsText.text = "";
        IDtoCropSO = new Dictionary<string, CropScriptableObject>();
        IDtoCropSO.Add("", null);

        instance = this;

        allfruit = Resources.LoadAll<CropScriptableObject>("ScriptableObjects");
        foreach (CropScriptableObject obj in allfruit)
        {
            IDtoCropSO.Add(obj.ID, obj);
        }

        
        animals = GameObject.FindGameObjectsWithTag("Animal");
        foreach (GameObject a in animals)
        {
            a.SetActive(false);
        }
        Canvas.ForceUpdateCanvases();
        GameObject templateTile = Instantiate(croptile, gridPanel.transform);

        firstRowFruits = new GameObject[6];
        secondRowFruits = new GameObject[6];
        thirdRowFruits = new GameObject[6];
        bool switchcolor = false;
        for (int i = 0; i < 3; i++)
        {
            
                for (int k = 0; k < 6; k++)
                {
                    GameObject generatetile = Instantiate(templateTile, gridPanel.transform);
                    string IDintile = "";
                    var matrixIndex = (i).ToString() + k.ToString();
                    CropTile generatecroptile = generatetile.GetComponent<CropTile>();
                    generatecroptile.setNewFruit(IDtoCropSO[IDintile], matrixIndex);
                    generatecroptile.undraggable();
                    if (switchcolor)
                    {
                        generatecroptile.switchTile();
                    }
                    switchcolor = !switchcolor;
                    switch (i)
                    {
                    case 0:
                        firstRowFruits[k] = generatetile;
                        break;
                    case 1:
                        secondRowFruits[k] = generatetile;
                        break;
                    case 2:
                        thirdRowFruits[k] = generatetile;
                        break;
                    }
                    Button B=generatetile.transform.Find("deleteButton").GetComponent<Button>();
                    addDeleteButton(B);
                    B.gameObject.SetActive(false);

                }

            
            switchcolor = !switchcolor;
        }
        Destroy(templateTile);


    }

    private void addDeleteButton(Button B)
    {
        B.onClick.AddListener( delegate { DeleteTile(B.gameObject.transform.parent.gameObject); });
        B.gameObject.GetComponent<Canvas>().sortingLayerName = "UI";
    }
  
    
    private void Start()
    {

        Debug.Log("scene loaded");
    }

    private void Update()
    {
        //if(firstRowCount<6)
        //{
        //    circleButton.interactable = true;
        //}
    
        //if (secondRowCount < 6)
        //{
        //    cubeButton.interactable = true;
        //}
     
        //if (thirdRowCount < 6)
        //{
        //    tringleButton.interactable = true;
        //}
      
    }

    public void OpenPanelEditor(PanelButton PB)
    {
        GameObject newPanel= Instantiate(panelEditorOrigin, panelEditorOrigin.transform.parent);
        newPanel.SetActive(true);
        newPanel.GetComponent<PanelEditor>().selectpanel(PB);
    }
    public void ClosePanelEditor(GameObject P)
    {
        Destroy(P);
    }

    public void ActivatePanel(GameObject crop)
    {
        evolutionpanel.GetComponent<EvolutionPanel>().Setup(crop);
    }
  
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void goMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void fruiteaten()
    {
        fruitsInScene--;

    }
    public void updateAnimals()
    {
      foreach (GameObject a  in animals)
        {
            int fruitCount=0;
            switch(a.GetComponent<Animal>().animalType.eatsShape)
            {
                case croptype.Shape.CIRCLE:
                    fruitCount = firstRowCount;
                    break;
                case croptype.Shape.CUBE:
                    fruitCount = secondRowCount;
                    break;
                case croptype.Shape.TRINGLE:
                    fruitCount = thirdRowCount;
                    break;
            }
            if(fruitCount>0)
            {
                a.SetActive(true);
            }
            else
            {
                a.SetActive(false);
            }
        }
    }
 
    public void addCircle()
    {
        addFruitToRow(firstRowFruits,"B0",circleButton,firstRowCount);
        firstRowCount++;
        updateAnimals();
    }
    public void addCube()
    {
        addFruitToRow(secondRowFruits, "R0", cubeButton,secondRowCount);
        secondRowCount++;
        updateAnimals();
    }
    public void addTringle()
    {
        addFruitToRow(thirdRowFruits, "Y0", tringleButton,thirdRowCount);
        thirdRowCount++;
        updateAnimals();
    }
    public void addFruitToRow(GameObject[] row,string ID,Button button,int count)
    {
        for (int i=0;i<6;i++)
        {
            if(row[i].GetComponent<CropTile>().cropOS==null)
            {
                Debug.Log("added fruit");
                row[i].GetComponent<CropTile>().setNewFruit(IDtoCropSO[ID], row[i].GetComponent<CropTile>().TileID);
                row[i].transform.Find("deleteButton").gameObject.SetActive(true);
                if (i==5)
                {
                    button.interactable = false;
                    
                }
               
                return;
            }
        }
    }

    public void DeleteTile(GameObject deletedtile)
    {
        //Debug.Log("tile"+deletedtile.GetComponent<CropTile>().TileID);
        string selectedID = deletedtile.GetComponent<CropTile>().TileID;
        
        GameObject[] row=null;
        int rowcount = 0;
        var firstNum = char.GetNumericValue(selectedID[0]);
        switch (firstNum)
        {
            case 0:
                row = firstRowFruits;
                firstRowCount--;
                rowcount = firstRowCount;
                break;
            case 1:
                row = secondRowFruits;
                secondRowCount--;
                rowcount = secondRowCount;
                break;
            case 2:
                row=thirdRowFruits;
                thirdRowCount--;
                rowcount=thirdRowCount;
                break;
        }
        Debug.Log("deleted tile " + selectedID + " current fruit in row "+rowcount);
        int k = (int)char.GetNumericValue(selectedID[1]);
        
        for (int i=k;i<6;i++)
        {
            if (i!=5 && row[i].GetComponent<CropTile>().cropOS != null)
            {
                row[i].GetComponent<CropTile>().setNewFruit(row[i+1].GetComponent<CropTile>().cropOS, row[i].GetComponent<CropTile>().TileID);
            }
            else
            {
                if(row[i].GetComponent<CropTile>().cropOS == null)
                {
                    row[i-1].transform.Find("deleteButton").gameObject.SetActive(false);
                }
                row[i].GetComponent<CropTile>().setNewFruit(null, row[i].GetComponent<CropTile>().TileID);
                row[i].transform.Find("deleteButton").gameObject.SetActive(false);
                break;
            }
        }
        updateAnimals();
    }

    public void SaveLevel()
    {
        string[] circleRow = new string[6];
        string[] cubeRow = new string[6];
        string[] tringleRow = new string[6];
        convertRowtoString(circleRow,firstRowFruits);
        convertRowtoString(cubeRow,secondRowFruits);
        convertRowtoString(tringleRow, thirdRowFruits);
        int[] newGoal = new int[5];
        newGoal[0] = bluePoints.getPointInt();
        newGoal[1] = redPoints.getPointInt();
        newGoal[2] = yellowPoints.getPointInt();
        newGoal[3] = greenPoints.getPointInt();
        newGoal[4] = purplePoints.getPointInt();
        Debug.Log(onStartButton.Tpanel.PanelsContent+" "+ onStartButton.Tpanel.popUpTimes);
        LevelInfo customLevel = new LevelInfo(circleRow,cubeRow,tringleRow,newGoal,onStartButton.Tpanel,onUndoButton.Tpanel,onOverButton.Tpanel);
        SaveSystem.saveLevel(customLevel);
        string levelname = SaveSystem.saveLevel(customLevel);
        savedAsText.text = "Saved File as - " + levelname;
       
    }
    public void convertRowtoString(string[] stringRow,GameObject[] cropRow)
    {
        for(int i=0;i<stringRow.Length;i++)
        {
            CropTile tile = cropRow[i].GetComponent<CropTile>();
            if (tile.cropOS!=null)
            {
                stringRow[i] = tile.cropOS.ID;

            }
            else
            {
                stringRow[i] = "";
            }
        }
    }
}


