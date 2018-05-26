using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Clicklogics/PowerDowns/RotatePopups")]
public class RotatePopups : OnClickLogic
{
    public int NumberOfRotates = 7;
    public float TimeBetweenRotates = 0.05f;

    public float TimeToShake = 3;
    public float MoveSpeed;

    public override IEnumerator RunClickCoroutine()
    {
        var tmpPopups = GameController.GetActivePopups();
        List<PopUpWindowManager> popups;
        if (tmpPopups.Count <= NumberOfRotates)
        {
            popups = tmpPopups.Select(a => a.GetComponent<PopUpWindowManager>()).Reverse().ToList();
        }
        else
        {
            popups = tmpPopups.GetRange(tmpPopups.Count - NumberOfRotates, NumberOfRotates).Select(a => a.GetComponent<PopUpWindowManager>()).Reverse().ToList();
        }

        int size = popups.Count();
        for (int i = 0; i < size; i++)
        {
            Rotate(popups[i].transform);
            yield return new WaitForSeconds(TimeBetweenRotates);
        }
    }

    private void Rotate(Transform pTransform)
    {
        int rng = Random.Range(1, 4);
        float rotation = 0;
        switch (rng)
        {
            case 3:
                rotation = -90;
                break;
            case 2:
                rotation = 180;
                break;
            case 1:
                rotation = 90;
                break;
            default:
                break;
        }
        pTransform.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, rotation, 0);
    }
}
