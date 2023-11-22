using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_follow : MonoBehaviour
{
    public Transform objectToFollow;
    public Vector3 offset;

    void Update()
    {
        if (objectToFollow != null)
        {
            transform.position = objectToFollow.position + offset;
        }
    }
}
