using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Clicklogics/PowerDowns/RandomPowerDown")]
public class RandomPowerDown : OnClickLogic
{
    public List<OnClickLogic> PotentialClickLogics;

    private OnClickLogic ChosenClickLogic;

    public override void Initialize()
    {
        ChosenClickLogic = PotentialClickLogics[Random.Range(0, PotentialClickLogics.Count)];
        UsesCoroutine = ChosenClickLogic.UsesCoroutine;
    }

    public override IEnumerator RunClickCoroutine()
    {
        return ChosenClickLogic.RunClickCoroutine();
    }

    public override void RunClickLogic()
    {
        ChosenClickLogic.RunClickLogic();
    }

}
