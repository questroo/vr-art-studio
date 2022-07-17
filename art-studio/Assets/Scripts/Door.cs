using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IDestructable
{
    [SerializeField] private Transform brokenDoor;
    public void Destruct()
    {
        Instantiate(brokenDoor, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
