using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //tile position
    [SerializeField] private Vector2 _position;
    public Vector2 position
    {
        get { return _position; }
    }
}