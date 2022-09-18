using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //tile position
    [SerializeField] private Vector2Int _position;
    public Vector2Int Position
    {
        get { return _position; }
    }
}