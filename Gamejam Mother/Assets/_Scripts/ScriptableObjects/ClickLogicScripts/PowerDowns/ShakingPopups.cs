using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Clicklogics/PowerDowns/Shaking")]
public class ShakingPopups : OnClickLogic
{
    public float TimeToShake = 3;
    public float MoveSpeed;

    public override IEnumerator RunClickCoroutine()
    {
        var allPopups = GameController.GetActivePopups();
        float time = 0;
        while (time < TimeToShake)
        {
            time += Time.deltaTime;
            allPopups.ForEach(a => Shake(a.transform));
            
            yield return null;
        }
    }

    private void Shake(Transform pTransform)
    {
        pTransform.GetComponent<RectTransform>().Translate(Random.Range(-1f, 1f) * MoveSpeed, Random.Range(-1f, 1f) * MoveSpeed,
            Random.Range(-1f, 1f) * MoveSpeed);

    }
}
