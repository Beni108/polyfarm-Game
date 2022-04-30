using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelButton : MonoBehaviour
{
    public string title;
    public TextForPanel Tpanel=null;

    [SerializeField]
    public Button DelButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void deleteButton()
    {
        Tpanel = null;
        DelButton.interactable = false;
    }
    public void OpenEditor()
    {
        DelButton.interactable = true;
        EditorHandler.instance.OpenPanelEditor(this);
    }
}
