using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    //the gameobjects the holds the pages
    public GameObject pageHolder;
    //the prefab of level button
    public GameObject levelPrefab;
    //the canvas 
    public GameObject thisCanvas;

    //next button
    public GameObject nextButton;
    //back button
    public GameObject backButton;

    public int numberOfLevels = 50;

    //all the panel object that are pages of levels
    private List<GameObject> pagePanels = new List<GameObject>();
    //sizes of page holder
    private Rect panelDimensions;
    //sizes of level button
    private Rect iconDimensions;
    //number of levels in each page
    private int amountPerPage;
    //number of levels made
    private int currentLevelCount;
    //spacing between level buttons
    public Vector2 iconSpacing;
    //current page [index]
    int  page=1;
    //total pages [ max length]
    int totalPages = 0;
    //the X of the page holder
    private float pageHolderX;
    //the  max level the player reached
    private int levelReached;
    // Start is called before the first frame update
    void Start()
    {
        //numberOfLevels = AllLevels.Instance.maxlevel;
        pageHolderX = pageHolder.transform.position.x;
        panelDimensions = pageHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelPrefab.GetComponent<RectTransform>().rect;
        int maxInARow = Mathf.FloorToInt((panelDimensions.width+iconSpacing.x)/(iconDimensions.width+iconSpacing.x));
        int maxinACol = Mathf.FloorToInt((panelDimensions.height+iconSpacing.y) / (iconDimensions.height+iconSpacing.y));
         amountPerPage = maxInARow * maxinACol;
         totalPages = Mathf.CeilToInt((float)numberOfLevels / amountPerPage);
        LoadPanels(totalPages);
        CheckButton();
    }
    void LoadPanels(int numberOfPanels)
    {
        GameObject panelClone = Instantiate(pageHolder) as GameObject;

        if(PlayerPrefs.HasKey("level"))
        {
            levelReached = PlayerPrefs.GetInt("level");
        }
        else
        {
            PlayerPrefs.SetInt("level", 1);
            levelReached = PlayerPrefs.GetInt("level"); ;
        }
        for (int i=1; i<=numberOfPanels;i++)
        {
            GameObject panel = Instantiate(panelClone) as GameObject;
            panel.transform.SetParent(thisCanvas.transform,false);
            panel.transform.SetParent(pageHolder.transform);
            panel.name = "page-" + i;
            panel.GetComponent<RectTransform>().localPosition = new Vector2(panelDimensions.width * (i - 1), 0);
            SetUpGrid(panel);
            int numberOfIcons = i == numberOfPanels ? numberOfLevels - currentLevelCount : amountPerPage;
            loadIcons(numberOfIcons, panel);
            pagePanels.Add(panel);
        }
        Destroy(panelClone);

    }
    //sets up the page properties
    void SetUpGrid(GameObject panel)
    {
        GridLayoutGroup grid=panel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(iconDimensions.width, iconDimensions.height);
        grid.childAlignment = TextAnchor.MiddleCenter;
        grid.spacing = iconSpacing;
    }
    //adds the level buttons to a page
    void loadIcons(int numberOfIcons, GameObject parentObject)
    {
        for(int i=1; i<=numberOfIcons;i++)
        {
            currentLevelCount++;
            GameObject icon = Instantiate(levelPrefab) as GameObject;
            icon.transform.SetParent(thisCanvas.transform, false);
            icon.transform.SetParent(parentObject.transform);
            icon.name = "level " + currentLevelCount;
            icon.GetComponentInChildren<Text>().text = ""+currentLevelCount;
            int x = new int();
            x = currentLevelCount;
            icon.gameObject.GetComponent<Button>().onClick.AddListener(delegate
            {
                levelSelected(x);
            });
            if (currentLevelCount>levelReached)
            {
                icon.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void clickNext()
    {
        page++;
        var targetPanel = pagePanels.FirstOrDefault(panel => panel.name == "page-" + page);
        float targetPositionX = pageHolderX - targetPanel.transform.position.x;
        var targetPosition = new Vector3(pageHolder.transform.position.x + targetPositionX, pageHolder.transform.position.y, 0);
        iTween.MoveTo(pageHolder, targetPosition, 0.5f);
        CheckButton();

    }
    public void clickBack()
    {
        page--;
        var targetPanel = pagePanels.FirstOrDefault(panel => panel.name == "page-" +page);
        float targetPositionX = pageHolderX - targetPanel.transform.position.x;
        var targetPosition = new Vector3(pageHolder.transform.position.x + targetPositionX, pageHolder.transform.position.y, 0);
        iTween.MoveTo(pageHolder, targetPosition, 0.5f);
        CheckButton();
    }
    private void CheckButton()
    {
       
        backButton.SetActive(page > 1);
        nextButton.SetActive(page < totalPages);
    }
     public void levelSelected(int levelIndex)
    {
        PlayerPrefs.SetInt("levelSelected", levelIndex);
        Debug.Log("level selected" + levelIndex);
        Invoke(nameof(loadGameplay),1f);
    }
    public void loadGameplay()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
