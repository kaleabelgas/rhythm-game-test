using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private void Start()
    {
        HitbarManager.OnButtonPressed += OnButtonPress;
    }

    private void OnButtonPress(Score score)
    {
        Debug.Log(score.ToString());

    }
}
