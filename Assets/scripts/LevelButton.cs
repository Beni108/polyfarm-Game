using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Sprite locksprite;

    public Text levelText;

    private int level = 0;

    private Button button;

    private Image image;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    public void Setup(int Level, bool isUnlock)
    {
        this.level = Level;
        levelText.text = level.ToString();

        if(isUnlock)
        {
            image.sprite = null;
            button.enabled = true;
            levelText.gameObject.SetActive(true);
        }
        else
        {
            image.sprite = locksprite;
            button.enabled = false;
            levelText.gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {

    }
}
