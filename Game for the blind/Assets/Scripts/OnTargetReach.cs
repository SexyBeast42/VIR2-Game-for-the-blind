using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnTargetReach : MonoBehaviour
{
    public float threshold = 0.02f;
    public bool wasReached = false;

    public Transform target;

    public UnityEvent OnReached;

    public void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < threshold && !wasReached)
        {
            OnReached.Invoke();
            wasReached = true;
        }
        else if (distance >= threshold)
        {
            wasReached = false;
        }
    }
}
