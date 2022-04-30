using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointEditor : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    private int score=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int getPointInt()
    {
        return int.Parse(scoreText.text);
    }
    public void increaseButton()
    {
        score++;
        updateText();

    }
    public void decreaseButton()
    {
        if (score > 0)
        {
            score--;
            updateText();
        }
    }
    private void updateText()
    {
        scoreText.text = score.ToString();
    }
}
