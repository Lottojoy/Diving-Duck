using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftMove : MonoBehaviour
{
    public float speed = 5f;
    private float leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    private void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;

        
    }
}
