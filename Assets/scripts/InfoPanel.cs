using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    [SerializeField]
    private Text inpaneltext;

    public TextForPanel TFP;
    private int popUped = 0;
    private void Awake()
    {
      
    }
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }
    public void activate()
    {
        this.gameObject.SetActive(true);
    }
    public void popUp()
    {
        if(TFP.popUpTimes==-1)
            this.gameObject.SetActive(true);
        else
        {
            if(popUped<TFP.popUpTimes)
            {
                popUped++;
                this.gameObject.SetActive(true);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }

    }
    public void popupUniversal(string pref)
    {
        if (TFP.popUpTimes == -1)
            this.gameObject.SetActive(true);
        else
        {
            if (PlayerPrefs.GetInt(pref) < TFP.popUpTimes)
            {
                PlayerPrefs.SetInt(pref,PlayerPrefs.GetInt(pref)+1);
                this.gameObject.SetActive(true);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
    }
    public void setupTFP(TextForPanel T)
    {
        TFP = T;
        inpaneltext.text = TFP.PanelsContent;
    }

    public void setText(string s)
    {
        inpaneltext.text = s;
    }
}
