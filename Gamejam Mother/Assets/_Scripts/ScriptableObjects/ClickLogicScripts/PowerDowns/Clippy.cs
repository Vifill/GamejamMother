using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Clicklogics/PowerDowns/Clippy")]
public class Clippy : OnClickLogic
{
    public GameObject ClippyPrefab;
    public float TimeToRoam;
    public float MoveSpeed;

    public override IEnumerator RunClickCoroutine()
    {
        var popupSpawner = FindObjectOfType<PopupSpawner>();
        GameObject clippy = CreateClippy(popupSpawner);
        var nextPosition = popupSpawner.GetSpawnLocation(clippy.GetComponent<RectTransform>().rect);

        float time = 0;
        while (time < TimeToRoam)
        {
            time += Time.deltaTime;

            clippy.transform.localPosition = Vector3.Lerp(clippy.transform.localPosition, nextPosition, MoveSpeed * Time.deltaTime);
            if (Vector3.Distance(clippy.transform.localPosition, nextPosition) < 1)
            {
                nextPosition = popupSpawner.GetSpawnLocation(clippy.GetComponent<RectTransform>().rect);
            }
            yield return null;
        }
    }

    private GameObject CreateClippy(PopupSpawner pPopupSpawner)
    {
        var parent = GameObject.Find("ClippyLayer").transform;
        var pos = pPopupSpawner.GetSpawnLocation(ClippyPrefab.GetComponent<RectTransform>().rect);
        var popup = Instantiate(ClippyPrefab, parent);
        popup.transform.localPosition = pos;
        return popup;
    }
}
