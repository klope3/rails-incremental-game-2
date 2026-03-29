using System.Collections.Generic;
using UnityEngine;

public class WorldObjectMover : MonoBehaviour
{
    private List<Rigidbody> rigidbodies;

    private void Awake()
    {
        rigidbodies = new List<Rigidbody>();
    }

    private void Update()
    {
        
    }
}
