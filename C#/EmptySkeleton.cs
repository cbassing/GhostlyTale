using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySkeleton : MonoBehaviour, IKillable
{
    public int skeletonID;
    public void Destroyer()
    {
        Destroy(this.gameObject);
    }



}
