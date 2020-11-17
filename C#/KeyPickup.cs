using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : BasePickup
{

    public GameManager gm;
    // Start is called before the first frame update

    protected override void DoPickup(GameObject go)
    {
        gm.hasKey = true;
        base.DoPickup(go);
    }
}
