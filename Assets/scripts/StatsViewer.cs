using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Newtonsoft.Json;
public class StatsViewer : MonoBehaviour
{
    [SerializeField]
    private GameObject STATcontainer;


    [SerializeField]
    private Text Played;
    [SerializeField]
    private Text Undo;
    [SerializeField]
    private Text Failed;

    [SerializeField]
    private Text LevelsPlayed;

    string[] stats;
    // Start is called before the first frame update
    void Start()
    {
        STATcontainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SelectFile()
    {

        string path = SFB.StandaloneFileBrowser.OpenFilePanel("Select player data json file", "","",false)[0];
        Debug.Log(path);
        if (path == null || path=="") return;
        STATcontainer.SetActive(true);
        stats = SaveSystem.readPlayerstatistics(path);
        Debug.Log("loaded player data");
        Undo.text = stats[0];
        Failed.text = stats[1];
        Played.text = stats[2];
        string AllLevelText = "";
        int[] allLevel = JsonConvert.DeserializeObject<int[]>(stats[3]);
        AllLevelText = AllLevelText + "Custom Levels Played: " + allLevel[0] + "\n";
        for (int i=1;i<allLevel.Length; i++)
        {
            AllLevelText = AllLevelText + "Level " + i + " Played: " + allLevel[i]+"\n";
        }
        //AllLevelText= AllLevelText.Replace("X", "\n");
        LevelsPlayed.text = AllLevelText;
    }

}
