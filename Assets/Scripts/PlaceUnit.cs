using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceUnit : MonoBehaviour
{
    public GameObject selected;//use button to reference something into this variable
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
                Vector3 position = hit.collider.gameObject.transform.position;//move object to the object that the raycast hit
                position.y += selected.GetComponentInChildren<MeshRenderer>().bounds.size.y / 2;//add half of unit/tower height
                selected.transform.position = position;
                if (Input.GetMouseButtonDown(0))
                {
                    MonoBehaviour[] scripts = selected.GetComponentsInChildren<MonoBehaviour>();//get every script on the root gameobject and turn it on
                    foreach (MonoBehaviour script in scripts)
                    {
                        script.enabled = true;
                    }
                    Debug.Log(TileManager.TileCoordinates(hit.collider.gameObject));
                    selected = null;
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
        }
        else
        {
            Destroy(selected);
            selected = null;
        }
    }
}

