using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    public static GameManger instance;



    [SerializeField]
    private GameObject evolutionpanel;
    private CropScriptableObject[] allfruit;
    private Stack<IAction> historyStack = new Stack<IAction>();
    private LevelInfo thislevel=null;
    public Dictionary<string, CropScriptableObject> IDtoCropSO;
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
        thislevel = AllLevels.Instance.getLevel(1);

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
      
    }
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
        if(historyStack.Count>0)
        {
            historyStack.Pop().UndoCommand();
        }
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

}
