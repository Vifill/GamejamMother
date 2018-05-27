using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour 
{
    public int Points { get; private set; }

    public void AddPoint()
    {
        Points++;
    }
}
