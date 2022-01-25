using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKuleTakip : MonoBehaviour
{

    public float speed = 20.0f;
    public float minDist = 1f;
    public Transform target;
    void Start()
    {
        if (target == null)
        {
            if (GameObject.FindWithTag ("EnemyKule") != null)
            {
                target = GameObject.FindWithTag("EnemyKule").GetComponent<Transform>();
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        transform.LookAt(target);

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance> minDist)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
