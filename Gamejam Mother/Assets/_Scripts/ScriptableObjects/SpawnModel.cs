using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "Configs/Spawn/SpawnModel")]
[Serializable]
public class SpawnModel : ScriptableObject
{
    public float SpawnRate;
    public List<PopupChanceModel> PopupChanceModel;
}

[Serializable]
public class PopupChanceModel
{
    public GameObject PopupPrefab;
    public float ChanceOfSpawn;
}