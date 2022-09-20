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
}
