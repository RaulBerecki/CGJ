using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject sign;
    [SerializeField] private CheckVisibility checkVisibility;
    List<GameObject> signList;
    [SerializeField] private bool isDown, isUp, isMiddle,isDeleted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDeleted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(checkVisibility.isVisible && !isDeleted)
        {
            for(int i = 0; i < signList.Count; i++)
            {
                Destroy(signList[i]);
            }
            isDeleted = true;
        }    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isUp)
            {
                GameObject gameObject = Instantiate(sign, new Vector2(2.2f,-2.3f), Quaternion.identity);
                signList.Add(gameObject);
                gameObject = Instantiate(sign,new Vector2(2.2f,0), Quaternion.identity);
                signList.Add(gameObject);
            }
            if (isDown)
            {
                GameObject gameObject = Instantiate(sign, new Vector2(2.2f, 2.3f), Quaternion.identity);
                signList.Add(gameObject);
                gameObject = Instantiate(sign, new Vector2(2.2f, 0), Quaternion.identity);
                signList.Add(gameObject);
            }
            if (isMiddle)
            {
                GameObject gameObject = Instantiate(sign, new Vector2(2.2f, -2.3f), Quaternion.identity);
                signList.Add(gameObject);
                gameObject = Instantiate(sign, new Vector2(2.2f, 2.3f), Quaternion.identity);
                signList.Add(gameObject);
            }
        }
    }
}
