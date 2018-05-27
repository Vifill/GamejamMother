using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenUIManager : MonoBehaviour 
{
    public TextMeshPro ScoreText;

    private PointsManager PointsManager;

    private void Start()
    {
        PointsManager = FindObjectOfType<PointsManager>();
    }

    private void SetScoreText()
    {

        ScoreText.text = PointsManager.Points + " single moms";
    }

    public void YesButton()
    {
        GameController.RestartGame();
    }

    public void NoButton()
    {
        GameController.QuitGame();
    }
}
