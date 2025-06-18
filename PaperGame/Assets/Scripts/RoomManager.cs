using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> rooms;
    private List<GameObject> roomsCreated;
    [SerializeField] private float roomDistances;
    [SerializeField] private float roomMovingSpeed;
    private Rigidbody2D rb;
    public bool isPlaying;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        roomsCreated = new List<GameObject>();
        roomsCreated.Add(this.gameObject.transform.GetChild(0).gameObject);
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
            rb.linearVelocity = new Vector2(-roomMovingSpeed, 0);
        else
            rb.linearVelocity = Vector2.zero;
        GenerateRandomRooms();
    }
    void GenerateRandomRooms()
    {
        if (roomsCreated.Count < 3)
        {
            if (roomsCreated.Count == 2)
            {
                GameObject room = Instantiate(rooms[Random.RandomRange(0, rooms.Count)], new Vector2(roomsCreated[1].transform.position.x + roomDistances, roomsCreated[1].transform.position.y), Quaternion.identity);
                room.transform.SetParent(this.gameObject.transform);
                roomsCreated.Add(room);
            }
            else
            {
                for (int i = 1; i < 3; i++)
                {
                    GameObject room = Instantiate(rooms[Random.RandomRange(0, rooms.Count)], new Vector2(roomsCreated[i - 1].transform.position.x + roomDistances, roomsCreated[i - 1].transform.position.y), Quaternion.identity);
                    room.transform.SetParent(this.gameObject.transform);
                    roomsCreated.Add(room);
                }
            }
        }
        if (roomsCreated[0].transform.position.x < -60)
        {
            Destroy(roomsCreated[0]);
            roomsCreated.RemoveAt(0);
        }
            
    }
}
