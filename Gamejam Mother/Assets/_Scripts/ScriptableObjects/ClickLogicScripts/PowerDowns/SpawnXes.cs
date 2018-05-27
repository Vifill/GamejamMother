using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Clicklogics/PowerDowns/SpawnXes")]
public class SpawnXes : OnClickLogic
{
    public float TimeBetween = 0.1f;
    public int NumberOfXes = 10;
    public GameObject XPrefab;

    public override IEnumerator RunClickCoroutine()
    {
        var parent = GameObject.Find("PopUpSpawn").transform;
        var popupSpawner = FindObjectOfType<PopupSpawner>();
        for(int i = 0; i < NumberOfXes; i++)
        {
            var pos = popupSpawner.GetSpawnLocation(XPrefab.GetComponent<RectTransform>().rect);
            var popup = Instantiate(XPrefab, parent);
            popup.transform.localPosition = pos;

            yield return new WaitForSeconds(TimeBetween);
        }
    }
}
