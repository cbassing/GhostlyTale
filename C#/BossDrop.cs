using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrop : BasePickup
{

    public GameManager gm;
    protected override void DoPickup(GameObject go)
    {
        gm.bossBeaten = true;
        base.DoPickup(go);
    }
}
