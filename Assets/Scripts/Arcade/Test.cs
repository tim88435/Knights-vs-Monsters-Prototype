using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        if (ArcadeInput.P1.R2)
        {
            Debug.Log("Banana");
        }
        else if (ArcadeInput.P2.Vertical > 0.5f)
        {
            Debug.Log("Run");
        }
    }
}
