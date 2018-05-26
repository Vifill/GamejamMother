using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Configs/Spawn/SpawnModelConfig")]
public class SpawnModelConfig : ScriptableObject
{
    public List<SpawnModelTimerModel> TimeModels;
}

[Serializable]
public class SpawnModelTimerModel
{
    public SpawnModel SpawnModel;
    public float TimeToNextModel;
}
