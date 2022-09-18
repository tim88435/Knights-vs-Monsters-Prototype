using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    #region Singleton
    private static TileManager _singleton;
    public static TileManager Singleton
    {
        get { return _singleton; }
        set
        {
            if (_singleton == null)
            {
                _singleton = value;
            }
            else if (_singleton != value)
            {
                Debug.LogWarning($"{typeof(TileManager)} already exists in the current scene!\nRemoving Duplicate");
                DestroyImmediate(value);
            }
        }
    }
    #endregion
    private void OnValidate()
    {
        Singleton = this;//singleton stuff
    }
    [SerializeField] public Tile[] firstTiles = new Tile[6];
    [SerializeField] private GameObject prefab;
    private void Start()
    {
        Instantiate(prefab, firstTiles[Random.Range(0, firstTiles.Length - 1)].transform.position + Vector3.up, Quaternion.identity);
    }
}
