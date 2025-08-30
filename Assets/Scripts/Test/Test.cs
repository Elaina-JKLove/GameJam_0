using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
    }
}
