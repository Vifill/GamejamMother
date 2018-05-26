using UnityEngine;
using System;
using System.Collections;

public class OnClickLogic : ScriptableObject
{
    public bool UsesCoroutine = false;

    public virtual void Initialize() { }

    public virtual void RunClickLogic() { }

    public virtual IEnumerator RunClickCoroutine()
    {
        yield return null;
    }
}