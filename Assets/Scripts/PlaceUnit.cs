using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceUnit : MonoBehaviour
{
    public GameObject selected;//use button to reference something into this variable
    private int selectedtype;//1 is defence, 2 is attack
    public Camera cam;
    public Vector3 point;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //find mouse position
        point = Input.mousePosition;
        if (selected != null)
        {
            selected.transform.position = cam.ScreenToWorldPoint(new Vector3(point.x, point.y, 20));//z is distance from camera
            Ray ray = cam.ScreenPointToRay(point);
            RaycastHit hit;
            //important
            //must make all tiles part of the tile layer
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, 1 << 6))//layermask = tile
            {
                if (selectedtype == 1)
                {
                    if (TileManager.TileCoordinates(hit.collider.gameObject, out Vector2Int cgoordinatesPlayer1, selectedtype))
                    {
                        Vector3 position = TileManager.Singleton.player1Tiles[cgoordinatesPlayer1.x][cgoordinatesPlayer1.y].transform.position;
                        position.y += selected.GetComponentInChildren<MeshRenderer>().bounds.size.y / 2;//add half of unit/tower height
                        selected.transform.position = position;
                        for (int i = 0; i < TileManager.Singleton.player2Tiles.Count; i++)
                        {
                            for (int j = 0; j < TileManager.Singleton.player1Tiles[i].Count; j++)
                            {
                                if (i == cgoordinatesPlayer1.x || j == cgoordinatesPlayer1.y)
                                {
                                    TileManager.Singleton.player1Tiles[i][j].GetComponent<Renderer>().material.color = Color.gray;
                                }
                                else
                                {
                                    TileManager.Singleton.player1Tiles[i][j].GetComponent<Renderer>().material.color = Color.white;
                                }
                            }
                        }
                        TileManager.Singleton.player1Tiles[cgoordinatesPlayer1.x][cgoordinatesPlayer1.y].GetComponent<Renderer>().material.color = Color.black;
                        if (Input.GetMouseButtonDown(0))
                        {
                            MonoBehaviour[] scripts = selected.GetComponentsInChildren<MonoBehaviour>();//get every script on the root gameobject and turn it on
                            foreach (MonoBehaviour script in scripts)
                            {
                                script.enabled = true;
                            }
                            Debug.Log(cgoordinatesPlayer1);
                            selected = null;
                            for (int i = 0; i < TileManager.Singleton.player1Tiles.Count; i++)
                            {
                                for (int j = 0; j < TileManager.Singleton.player1Tiles[i].Count; j++)
                                {
                                    TileManager.Singleton.player1Tiles[i][j].GetComponent<Renderer>().material.color = Color.white;
                                }
                            }
                        }
                    }
                }
                else if (selectedtype == 2)
                {
                    if (TileManager.TileCoordinates(hit.collider.gameObject, out Vector2Int coordinatesPlayer2, selectedtype))
                    {
                        //Vector3 position = hit.collider.gameObject.transform.position;//move object to the object that the raycast hit
                        Vector3 position = TileManager.Singleton.player2Tiles[coordinatesPlayer2.x][0].transform.position;
                        position.y += selected.GetComponentInChildren<MeshRenderer>().bounds.size.y / 2;//add half of unit/tower height
                        position.x += -3;
                        selected.transform.position = position;
                        for (int i = 0; i < TileManager.Singleton.player2Tiles.Count; i++)
                        {
                            for (int j = 0; j < TileManager.Singleton.player2Tiles[i].Count; j++)
                            {
                                TileManager.Singleton.player2Tiles[i][j].GetComponent<Renderer>().material.color = coordinatesPlayer2.x == i ? Color.gray : Color.white;
                            }
                        }
                        if (Input.GetMouseButtonDown(0))
                        {
                            MonoBehaviour[] scripts = selected.GetComponentsInChildren<MonoBehaviour>();//get every script on the root gameobject and turn it on
                            foreach (MonoBehaviour script in scripts)
                            {
                                script.enabled = true;
                            }
                            Debug.Log(coordinatesPlayer2);
                            selected = null;
                            for (int i = 0; i < TileManager.Singleton.player2Tiles.Count; i++)
                            {
                                for (int j = 0; j < TileManager.Singleton.player2Tiles[i].Count; j++)
                                {
                                    TileManager.Singleton.player2Tiles[i][j].GetComponent<Renderer>().material.color = Color.white;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < TileManager.Singleton.player1Tiles.Count; i++)
                {
                    for (int j = 0; j < TileManager.Singleton.player1Tiles[i].Count; j++)
                    {
                        TileManager.Singleton.player1Tiles[i][j].GetComponent<Renderer>().material.color = Color.white;
                    }
                }
                for (int i = 0; i < TileManager.Singleton.player2Tiles.Count; i++)
                {
                    for (int j = 0; j < TileManager.Singleton.player2Tiles[i].Count; j++)
                    {
                        TileManager.Singleton.player2Tiles[i][j].GetComponent<Renderer>().material.color = Color.white;
                    }
                }
            }
        }
    }
    //use this on a button, put what prefab onto the button that you want to spawn
    //should be pretty flexible and be able to spawn any unit that is set up correctly
    //also can cancel unit placement by pressing the button again
    public void SelectTower(GameObject tower)
    {
        if (selected == null)
        {
            selected = Instantiate(tower, point, Quaternion.identity);
            selectedtype = selected.TryGetComponent(out AttackingUnit _) ? 2 : 1;
        }
        else
        {
            Destroy(selected);
            selected = null;
        }
    }
}

