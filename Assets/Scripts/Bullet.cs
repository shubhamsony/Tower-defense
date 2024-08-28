using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public GameObject target;

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 destVec = (target.transform.position - transform.position);

            float sped = speed * Time.fixedDeltaTime;

            // Only moves if the destination isnt close enough.
            // This allows the bullet to snap to each location.
            if (destVec.magnitude > sped)
                transform.position += destVec.normalized * sped;
            else
            {
                transform.position = target.transform.position;

                // delete the target
                target.GetComponent<EnemyMove>().Downgrade();
                Destroy(gameObject);
            }
        }
        else
            Destroy(gameObject);
    }
}
