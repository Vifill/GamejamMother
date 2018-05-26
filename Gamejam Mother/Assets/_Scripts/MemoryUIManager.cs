using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryUIManager : MonoBehaviour 
{
    public Image BarFill;
    public TextMeshProUGUI PercentageText;
    public float LerpSpeed = 5;

    private Coroutine CurrentCoroutine = null;
    private float TargetPercentage;
    private float CurrentPercentage = 0;

	private void Start() 
	{
        BarFill.fillAmount = CurrentPercentage;
        PercentageText.text = CurrentPercentage + "%";
	}
	
	private void Update() 
	{
		
	}

    public void UpdateMemoryUI(float pMemory)
    {
        TargetPercentage = pMemory;
        //PercentageText.text = pMemory + "%";
        //BarFill.fillAmount = pMemory / 100;
        if (CurrentCoroutine == null)
        {
            CurrentCoroutine = StartCoroutine(BarLerp());
        }
    }

    private IEnumerator BarLerp()
    {
        while (CurrentPercentage < TargetPercentage)
        {
            //CurrentPercentage = Mathf.Lerp(CurrentPercentage, TargetPercentage, LerpSpeed * Time.deltaTime);
            CurrentPercentage += LerpSpeed * Time.deltaTime;
            BarFill.fillAmount = CurrentPercentage/100;
            PercentageText.text = Mathf.RoundToInt(CurrentPercentage) + "%";
            yield return null;
        }
        CurrentCoroutine = null;
    }
}
