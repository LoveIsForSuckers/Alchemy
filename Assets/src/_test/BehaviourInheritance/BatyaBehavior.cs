using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatyaBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Batya Start");

        protectedLoad();
	}

    virtual protected void protectedLoad()
    {
        Debug.Log("Batya protectedLoad");
    }
}
