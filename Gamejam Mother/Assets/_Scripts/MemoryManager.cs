using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryManager : MonoBehaviour 
{
    public float MemoryUsed { get; private set; }

    private MemoryUIManager MemoryUIManager;

	private void Start() 
	{
        MemoryUIManager = FindObjectOfType<MemoryUIManager>();
	}
	
	private void Update() 
	{
		
	}

    public void AddMemoryUsage(float pPercentage)
    {
        MemoryUsed = Mathf.Clamp(MemoryUsed += pPercentage, 0, 100);
        MemoryUIManager.UpdateMemoryUI(MemoryUsed);

        if (MemoryUsed >= 100)
        {
            GameController.instance.LoseGame();
        }
    }

    public void RemoveMemoryUsage(float pPercentage)
    {
        MemoryUsed = Mathf.Clamp(MemoryUsed -= pPercentage, 0, 100);
        MemoryUIManager.UpdateMemoryUI(MemoryUsed);
    }
}
