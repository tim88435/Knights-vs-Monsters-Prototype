using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    [SerializeField] public List<List<GameObject>> tiles = new List<List<GameObject>>();
    [SerializeField] private GameObject prefab;
    [SerializeField] Vector2Int size;
    private void Start()
    {
        for (int i = 0; i < size.x; i++)
        {
            tiles.Add(new List<GameObject>());
            for (int j = 0; j < size.y; j++)
            {
                tiles[i].Add(PrefabUtility.InstantiatePrefab(prefab) as GameObject);
                tiles[i][j].transform.position = new Vector3(2 * j, 0, 2 * i);
            }
        }
        //Instantiate(prefab, firstTiles[Random.Range(0, firstTiles.Length - 1)].transform.position + Vector3.up, Quaternion.identity);
    }
    public static Vector2Int TileCoordinates(GameObject tile)
    {
        if (Singleton == null)
        {
            Debug.LogWarning($"{typeof(TileManager)} does not exist in the current context\nReturning Vector2Int.zero");
            return Vector2Int.zero;
        }
        for (int i = 0; i < Singleton.tiles.Count; i++)
        {
            if (Singleton.tiles[i].Contains(tile))
            {
                for (int j = 0; j < Singleton.tiles[i].Count; j++)
                {
                    if (Singleton.tiles[i][j] == tile)
                    {
                        return new Vector2Int(i, j);
                    }
                }
            }
        }
        Debug.Log("GameObject not a saved tile\nReturning Vector2Int.zero");
        return Vector2Int.zero;
    }
}
