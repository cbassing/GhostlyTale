using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public Animator anim;

    public bool isOn = false;
    public int value;
    private void OnTriggerEnter2D(Collider2D other)
    {
        isOn = !isOn;
        if (other.CompareTag("Spirit"))
        {
            anim.SetBool("isOn", isOn);
            if (isOn)
            {
                value = 1;
            }
            else
            {
                value = 0;
            }
        }

    }

}
