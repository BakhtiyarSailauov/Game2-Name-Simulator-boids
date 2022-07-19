using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neighborhood : MonoBehaviour
{
    [Header("Set Dynamicale")]
    public List<Boid> neigbors;
    private SphereCollider coll;

    private void Start()
    {
        neigbors = new List<Boid>();
        coll = GetComponent<SphereCollider>();
        coll.radius = Spawner.S.neighborDist / 2;
    }

    private void FixedUpdate()
    {
        if (coll.radius != Spawner.S.neighborDist/2)
        {
            coll.radius = Spawner.S.neighborDist / 2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Boid b = other.GetComponent<Boid>();
        if (b!=null)
        {
            if (neigbors.IndexOf(b) == -1)
            {
                neigbors.Add(b);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Boid b = other.GetComponent<Boid>();
        if (b != null)
        {
            if (neigbors.IndexOf(b) != -1)
            {
                neigbors.Remove(b);
            }
        }
    }

    public Vector3 avgPos 
    {
        get 
        {
            Vector3 avg = Vector3.zero;
            if (neigbors.Count == 0)
            {
                return avg;
            }

            for (int i = 0; i < neigbors.Count; i++)
            {
                avg += neigbors[i].pos;
            }
            avg /= neigbors.Count;

            return avg;
        }
    }

    public Vector3 avgVel 
    {
        get
        {
            Vector3 avg = Vector3.zero;
            if (neigbors.Count == 0)
            {
                return avg;
            }
            for (int i = 0; i < neigbors.Count; i++)
            {
                avg += neigbors[i].rigid.velocity;
            }
            avg /= neigbors.Count;

            return avg;
        }
    }

    public Vector3 avgClosePos 
    {
        get 
        {
            Vector3 avg = Vector3.zero;
            Vector3 delta;
            int nearCount = 0;
            for (int i = 0; i < neigbors.Count; i++)
            {
                delta = neigbors[i].pos - transform.position;
                if (delta.magnitude <=Spawner.S.collDist)
                {
                    avg += neigbors[i].pos;
                    nearCount++;
                }
            }
            if (nearCount == 0) return avg;
            avg /= nearCount;
            return avg;
        }
    }
}
