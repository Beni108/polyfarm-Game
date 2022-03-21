using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text Score1;
    public Text Score2;
    public Text Score3;
    public Text Score4;
    public Text Score5;

    int score1 = 0, score2 = 0, score3 = 0, score4 = 0, score5 = 0;
    int goal1 = 0, goal2 = 0, goal3 = 0, goal4 = 0, goal5 = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
    }
    
    // Update is called once per frame
    void Update()
    {

    }
    void SetGoal(int g1, int g2, int g3, int g4, int g5)
    {
        goal1 = g1;
        goal2 = g2;
        goal3 = g3;
        goal4 = g4;
        goal5 = g5;
    }
    void SetScore(int s1, int s2, int s3, int s4, int s5)
    {
        score1 = s1;
        score2 = s2;
        score3 = s3;
        score4 = s4;
        score5 = s5;
        UpdateScore();
    }
    void AddScore(int s1, int s2, int s3, int s4, int s5)
    {
        score1 += s1;
        score2 += s2;
        score3 += s3;
        score4 += s4;
        score5 += s5;
        UpdateScore();
    }
    void UpdateScore()
    {
        Score1.text = score1.ToString() + "/" + goal1.ToString();
        Score2.text = score2.ToString() + "/" + goal2.ToString();
        Score3.text = score3.ToString() + "/" + goal3.ToString();
        Score4.text = score4.ToString() + "/" + goal4.ToString();
        Score5.text = score5.ToString() + "/" + goal5.ToString();
        CheckVictory();

    }
    void CheckVictory()
    {
        if(goal1==score1 && goal2==score2 && goal3 == score3 && goal4 == score4 && goal5 == score5)
        {
            Debug.Log("PERFECT VICTORY");
        }
        else if(goal1 <= score1 && goal2 <= score2 && goal3 <= score3 && goal4 <= score4 && goal5 <= score5)
        {
            Debug.Log("VICTORY");
        }
        else
        {
            Debug.Log("Not Victory yet");
        }
    }
}
