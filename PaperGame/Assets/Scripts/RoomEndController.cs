using System.Collections.Generic;
using UnityEngine;

public class RoomEndController : MonoBehaviour
{
    [SerializeField] private List<GameObject> endRooms;
    [SerializeField] private Transform parent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject end = Instantiate(endRooms[Random.RandomRange(0,endRooms.Count)],new Vector2(transform.position.x+44.7f,3.94f),Quaternion.identity);
        end.transform.parent = parent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
