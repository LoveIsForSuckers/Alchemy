using UnityEngine;
using System.Collections;

public class SynBehavior : BatyaBehavior
{
    void Start()
    {
        Debug.Log("Syn Start");
    }

    override protected void protectedLoad()
    {
        Debug.Log("Syn protectedLoad");
    }
}
