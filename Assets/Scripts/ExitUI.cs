using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitUI : MonoBehaviour
{

    public GameObject goal, panel;
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (goal.GetComponent<Goal>().exit)
        {
            ExitGame();
        }
    }
    public void ExitGame()
    {
        panel.SetActive(true);
    }
}