using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Clicklogics/PowerDowns/RotateOverTimePopups")]
public class RotateOverTimePopups : OnClickLogic
{
    public int NumerToRotate = 7;
    public float RotateSpeed = 2f;

    public float TimeToRotate = 3;

    public static bool IsRotating;

    public override IEnumerator RunClickCoroutine()
    {
        if (IsRotating) yield break;
        //Initialization
        var tmpPopups = GameController.GetActivePopups();
        List<PopUpWindowManager> popups;
        List<int> directions = new List<int>();
        for (int i = 0; i < tmpPopups.Count(); i++)
        {
            directions.Add((Random.Range(0, 2) == 1 ? 1 : -1));
        }

        if (tmpPopups.Count <= NumerToRotate)
        {
            popups = tmpPopups.Select(a => a.GetComponent<PopUpWindowManager>()).Reverse().ToList();
        }
        else
        {
            popups = tmpPopups.GetRange(tmpPopups.Count - NumerToRotate, NumerToRotate).Select(a => a.GetComponent<PopUpWindowManager>()).Reverse().ToList();
        }

        //Rotation logic
        int size = popups.Count();
        float time = 0;
        while (time < TimeToRotate)
        {
            time += Time.deltaTime;
            for (int i = 0; i < size; i++)
            {
                if (popups[i] != null)
                {
                    Rotate(popups[i].transform, directions[i]);
                }
            }
            yield return null;
        }

        popups.Where(a => a != null).ToList().ForEach(a => a.transform.rotation = Quaternion.identity);
        IsRotating = false;
    }

    private void Rotate(Transform pTransform, int pDirection)
    {
        pTransform.Rotate(new Vector3(0, 0, RotateSpeed * pDirection));
    }
}
