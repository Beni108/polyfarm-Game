using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public static GameManger instance;



    [SerializeField]
    private GameObject evolutionpanel;
    [SerializeField]
    private GameObject victoryPanel;
    [SerializeField]
    private GameObject gameOverpanel;
    [SerializeField]
    private GameObject gridPanel;
    [SerializeField]
    private GameObject croptile;
  

    private CropScriptableObject[] allfruit;
    private Stack<IAction> historyStack = new Stack<IAction>();
    private LevelInfo thislevel=null;
    public Dictionary<string, CropScriptableObject> IDtoCropSO;
    private int fruitsInScene=0;
    private void Awake()
    {
        IDtoCropSO = new Dictionary<string, CropScriptableObject>();
        IDtoCropSO.Add("",null);

        instance = this;

        allfruit = Resources.LoadAll<CropScriptableObject>("ScriptableObjects");
        foreach (CropScriptableObject obj in allfruit)
        {
            IDtoCropSO.Add(obj.ID, obj);
        }

        thislevel = AllLevels.Instance.getLevel(PlayerPrefs.GetInt("levelSelected"));
        Debug.Log("loaded Level "+PlayerPrefs.GetInt("levelSelected"));
        ScoreManager scoreBoard = GameObject.Find("Goal Board").GetComponent<ScoreManager>();
        scoreBoard.SetGoal(thislevel.goal);

        GameObject[] animals;
        animals = GameObject.FindGameObjectsWithTag("Animal");
        foreach (GameObject a in animals)
        {
            int animalbool=0;
            switch (a.GetComponent<Animal>().animalType.eatsShape)
            {
                case croptype.Shape.CUBE:
                    animalbool = 1;
                    break;
                case croptype.Shape.CIRCLE:
                    animalbool = 0;
                    break;
                case croptype.Shape.TRINGLE:
                    animalbool = 2;
                    break;
            }
            a.SetActive(thislevel.animals[animalbool]);
        }
     
        Canvas.ForceUpdateCanvases();
        GameObject templateTile = Instantiate(croptile, gridPanel.transform);
  
        //templateTile.GetComponent<CropTile>().field = gridPanel.transform.parent.gameObject;
        // calcsize(templateTile);
    


        bool switchcolor = false;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 6; k++)
                {
                    GameObject generatetile = Instantiate(templateTile, gridPanel.transform);

                    string IDintile = "";
                    switch (i)
                    {
                        case 0:
                            IDintile = thislevel.circlefruits[j, k]; ;
                            break;
                        case 1:
                            IDintile = thislevel.cubefruits[j, k]; ;
                            break;
                        case 2:
                     
                            IDintile = thislevel.tringlefruit[j, k];
                            break;

                    }
                    var matrixIndex = (i * 3 + j).ToString() + k.ToString();
                    CropTile generatecroptile = generatetile.GetComponent<CropTile>();
                    generatecroptile.setNewFruit(IDtoCropSO[IDintile], matrixIndex);
                    if(!IDintile.Equals(""))
                    {
                        fruitsInScene++;
                    }
                    if (switchcolor)
                    {
                        generatecroptile.switchTile();
                    }
                    switchcolor = !switchcolor;

                }
                switchcolor = !switchcolor;
            }
        }
        Destroy(templateTile);

    }
    private void updateGrid(GameObject newcroptile)
    {
      
        float heightGrid = gridPanel.GetComponent<RectTransform>().rect.height;
        Debug.Log(heightGrid);
        float idealHeight = heightGrid / 6;
        Debug.Log(idealHeight);
        float tileHeight = newcroptile.GetComponent<RectTransform>().rect.height;
        float scaled = idealHeight / tileHeight;
        newcroptile.GetComponent<RectTransform>().localScale = new Vector3(scaled, scaled, scaled);
        gridPanel.GetComponent<GridLayoutGroup>().cellSize=new Vector2 (idealHeight,idealHeight);

    }
    //private IEnumerator loadGrid()
    //{
    //    Canvas.ForceUpdateCanvases();
    //    GameObject templateTile = Instantiate(croptile, gridPanel.transform);
      
    //    //templateTile.GetComponent<CropTile>().field = gridPanel.transform.parent.gameObject;
    //    // calcsize(templateTile);
    //    //yield return StartCoroutine(calcsize(templateTile));


    //    bool switchcolor = false;
    //    for (int i = 0; i < 3; i++)
    //    {
    //        for (int j = 0; j < 2; j++)
    //        {
    //            for (int k = 0; k < 6; k++)
    //            {
    //                GameObject generatetile = Instantiate(templateTile, gridPanel.transform);

    //                string IDintile = "";
    //                switch (i)
    //                {
    //                    case 0:
    //                        IDintile = thislevel.circlefruits[j, k]; ;
    //                        break;
    //                    case 1:
    //                        IDintile = thislevel.cubefruits[j, k]; ;
    //                        break;
    //                    case 2:
    //                        break;
    //                        IDintile = thislevel.tringlefruit[j, k]; ;

    //                }
    //                var matrixIndex = (i * 3 + j).ToString() + k.ToString();
    //                CropTile generatecroptile = generatetile.GetComponent<CropTile>();
    //                generatecroptile.setNewFruit(IDtoCropSO[IDintile], matrixIndex);
    //                if (switchcolor)
    //                {
    //                    generatecroptile.switchTile();
    //                }
    //                switchcolor = !switchcolor;

    //            }
    //            switchcolor = !switchcolor;
    //        }
    //    }
    //    Destroy(templateTile);
    //}
    //private IEnumerator calcsize(GameObject newcroptile)
    //{
    //    yield return 0;
    //    float heightGrid = gridPanel.GetComponent<RectTransform>().rect.height;
   
    //    float idealHeight = heightGrid / 6;
    //    Debug.Log(idealHeight);
    //    float tileHeight = newcroptile.GetComponent<RectTransform>().rect.height;
    //    float scaled = idealHeight / tileHeight;
    //    newcroptile.GetComponent<RectTransform>().localScale = new Vector3(scaled, scaled, scaled);
    //    RectTransform rt = newcroptile.GetComponent<RectTransform>();
    //    Debug.Log(rt.rect.height);
   
    //    gridPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(idealHeight, idealHeight);
 


    //}
    private void Start()
    {

        Debug.Log("scene loaded");
    }

    private void Update()
    {
        
    }
    public void ExecuteCommand(IAction action)
    {
        action.ExecuteCommand();
        historyStack.Push(action);
    }
    public void UndoCommand()
    {
        if(victoryPanel.activeSelf || gameOverpanel.activeSelf)
        {
            victoryPanel.SetActive(false);
            gameOverpanel.SetActive(false);
        }
        if(historyStack.Count>0)
        {
            historyStack.Pop().UndoCommand();
        }
    }
    public void ActivatePanel(GameObject crop)
    {
        evolutionpanel.GetComponent<EvolutionPanel>().Setup(crop);
    }
    public void ActivateVictory()
    {
        victoryPanel.SetActive(true);
        Debug.Log(AllLevels.Instance.maxlevel);
        int  levelReached = PlayerPrefs.GetInt("levelSelected");
        if (levelReached >= AllLevels.Instance.maxlevel)
        {
            levelReached = AllLevels.Instance.maxlevel;
            victoryPanel.transform.Find("background/ROW/NextButton").gameObject.SetActive(false);
        }
        else
            levelReached++;
        if(levelReached>=PlayerPrefs.GetInt("level"))
        {
            PlayerPrefs.SetInt("level", levelReached);
        }
        
       
    }
    public void checkGameOver()
    {
       if(fruitsInScene<=0)
        {
            gameOverpanel.SetActive(true);
        }
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
    public void nextlevel()
    {
        PlayerPrefs.SetInt("levelSelected",PlayerPrefs.GetInt("levelSelected")+1);
        ResetScene();
    }
    public void undofruiteaten()
    {
        fruitsInScene++;
    }
}
