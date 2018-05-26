using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Clicklogics/PowerUps/FreeRamDownload")]
public class FreeRamDownload : OnClickLogic 
{
    public float MemoryToClear;
    private MemoryManager MemoryManager;

    public override void Initialize()
    {
        MemoryManager = FindObjectOfType<MemoryManager>();
    }

    public override void RunClickLogic()
    {
        MemoryManager.RemoveMemoryUsage(MemoryToClear);
    }
}
