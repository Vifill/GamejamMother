using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MomTextColorLerp : MonoBehaviour 
{
    private TextMeshProUGUI MomText;
    public float LerpTime;

	private void Start() 
	{
        MomText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(LerpColor());
	}
	
	private IEnumerator LerpColor()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            MomText.faceColor = Color.Lerp(Color.red, Color.magenta, time);

            if (time >= LerpTime)
            {
                time = 0;
            }
            yield return null;
        }
    }
}
