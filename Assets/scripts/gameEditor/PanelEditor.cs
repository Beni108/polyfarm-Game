using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelEditor : MonoBehaviour
{
    private PanelButton PB;
    [SerializeField]
    private GameObject title;
    [SerializeField]
    private GameObject onceButton;
    [SerializeField]
    private GameObject AlwaysButton;
    [SerializeField]
    private GameObject customAmountButton;

    [SerializeField]
    private InputField inputText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void selectpanel(PanelButton P)
    {
        PB=P;
        title.GetComponent<Text>().text = PB.title;
        if (PB.Tpanel == null)
        {
            PB.Tpanel = new TextForPanel();
            PB.Tpanel.popUpTimes = 1;
            onceButton.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            switch (PB.Tpanel.popUpTimes)
            {
                case 1:
                    onceButton.GetComponent<Toggle>().isOn = true;
                    break;
                case -1:
                    AlwaysButton.GetComponent<Toggle>().isOn = true;
                    break;
            }
            if(PB.Tpanel.popUpTimes>1)
            {
                customAmountButton.GetComponent<Toggle>().isOn = true;
                InputField amountfield = customAmountButton.transform.Find("InputField").gameObject.GetComponent<InputField>();
                amountfield.text = PB.Tpanel.popUpTimes.ToString();
            }
        }
      ;
        if(PB.Tpanel.PanelsContent!=null)
        {
            inputText.text = PB.Tpanel.PanelsContent;
        }
    }
    public void CustomAmountSwitcher(bool B)
    {
        GameObject amountfield = customAmountButton.transform.Find("InputField").gameObject;
        if (B)
        {
            amountfield.GetComponent<InputField>().interactable = true;

        }
        else
        {

            amountfield.GetComponent<InputField>().text = "";
            amountfield.GetComponent<InputField>().interactable = false;
        }
    }
    public void exitpanel()
    {
        EditorHandler.instance.ClosePanelEditor(this.gameObject);

    }
    public void savePanel()
    {
        PB.Tpanel.PanelsContent = inputText.text;
        if (onceButton.GetComponent<Toggle>().isOn==true)
        {
            PB.Tpanel.popUpTimes = 1;
        }
        if (AlwaysButton.GetComponent<Toggle>().isOn == true)
        {
            PB.Tpanel.popUpTimes = -1;
        }
        if (customAmountButton.GetComponent<Toggle>().isOn == true)
        {
            GameObject amountfield = customAmountButton.transform.Find("InputField").gameObject;
            InputField amount = customAmountButton.transform.Find("InputField").gameObject.GetComponent<InputField>();
            PB.Tpanel.popUpTimes = int.Parse(amount.text);
        }
        EditorHandler.instance.ClosePanelEditor(this.gameObject);
    }
}
