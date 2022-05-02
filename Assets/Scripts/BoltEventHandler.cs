using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoltEventHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Action<bool> OnUpdateAllowBoltChange;

    private void OnTriggerEnter(Collider other)
    {
        OnUpdateAllowBoltChange?.Invoke(true);
    }

    private void OnTriggerExit(Collider other)
    {
        OnUpdateAllowBoltChange?.Invoke(false);
    }
}
