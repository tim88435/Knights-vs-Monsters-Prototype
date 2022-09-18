using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingUnit : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime);
    }
}
