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
    [SerializeField] public List<List<GameObject>> player1Tiles = new List<List<GameObject>>();
    [SerializeField] public List<List<GameObject>> player2Tiles = new List<List<GameObject>>();
    [SerializeField] private GameObject player1Battlefield;
    [SerializeField] private GameObject player2Battlefield;
    [SerializeField] private GameObject prefab;
    [SerializeField] Vector2Int size;
    private void Start()
    {
        for (int i = 0; i < size.x; i++)
        {
            player1Tiles.Add(new List<GameObject>());
            for (int j = 0; j < size.y; j++)
            {
                player1Tiles[i].Add(PrefabUtility.InstantiatePrefab(prefab) as GameObject);
                player1Tiles[i][j].transform.parent = player1Battlefield.transform;
                player1Tiles[i][j].transform.localPosition = new Vector3(2 * j, 0, 2 * i);
            }
        }
        for (int i = 0; i < size.x; i++)
        {
            player2Tiles.Add(new List<GameObject>());
            for (int j = 0; j < size.y; j++)
            {
                player2Tiles[i].Add(PrefabUtility.InstantiatePrefab(prefab) as GameObject);
                player2Tiles[i][j].transform.parent = player2Battlefield.transform;
                player2Tiles[i][j].transform.localPosition = new Vector3(2 * j, 0, 2 * i);
            }
        }
        //Instantiate(prefab, firstTiles[Random.Range(0, firstTiles.Length - 1)].transform.position + Vector3.up, Quaternion.identity);
    }
    public static bool TileCoordinates(GameObject tile, out Vector2Int coordinate, int playerNumber)
    {
        if (Singleton == null)
        {
            Debug.LogWarning($"{typeof(TileManager)} does not exist in the current context\nReturning Vector2Int.zero");
            coordinate = Vector2Int.zero;
            return false;
        }
        switch (playerNumber)
        {
            case 1:
                for (int i = 0; i < Singleton.player1Tiles.Count; i++)
                {
                    if (Singleton.player1Tiles[i].Contains(tile))
                    {
                        for (int j = 0; j < Singleton.player1Tiles[i].Count; j++)
                        {
                            if (Singleton.player1Tiles[i][j] == tile)
                            {
                                coordinate = new Vector2Int(i, j);
                                return true;
                            }
                        }
                    }
                }
                break;
            case 2:
                for (int i = 0; i < Singleton.player2Tiles.Count; i++)
                {
                    if (Singleton.player2Tiles[i].Contains(tile))
                    {
                        for (int j = 0; j < Singleton.player2Tiles[i].Count; j++)
                        {
                            if (Singleton.player2Tiles[i][j] == tile)
                            {
                                coordinate = new Vector2Int(i, j);
                                return true;
                            }
                        }
                    }
                }
                break;
            default:
                Debug.LogWarning("Player number must be 1 or 2");
                break;
        }
        //Debug.LogWarning("GameObject not a saved tile\nReturning Vector2Int.zero");
        coordinate = Vector2Int.zero;
        return false;
    }
}
