using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCross : MonoBehaviour
{
    public static int score = 0;
    public Text textScore;
    
    // Start is called before the first frame update
    void Start()
    {
        textScore.text = "Score : " + score;
    }

    public static void AddOne()
    {
        ++score;
    }

    public static void Reset()
    {
        score = 0;
    }

    public void ResetWrapper()
    {
        Reset();
    }
}
