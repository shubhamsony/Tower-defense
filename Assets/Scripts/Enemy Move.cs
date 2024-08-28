using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyMove : MonoBehaviour
{
    [HideInInspector] public GameObject[] Path;
    [HideInInspector] public float speed;
    [HideInInspector] public int pathPointer = 0;
    [SerializeField] private GameObject downgrade;

    private bool reachedEnd = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!reachedEnd)
        {
            
            Vector3 destination = Path[pathPointer].transform.position;
            Vector3 destVec = (destination - transform.position);

            float sped = speed * Time.fixedDeltaTime;

            // Only moves if the destination isnt close enough.
            // This allows the enemy to snap to each location.
            if (destVec.magnitude > sped)
                transform.position += destVec.normalized * sped;
            else
            {
                transform.position = destination;
                pathPointer++;
                //if length is 2, when its at index 1 (the final index) itll reach 2 and therefore end.
                if (pathPointer == Path.Length)
                    reachedEnd = true;
            }
        }
        else
        {
            //run on reaching end
            GlobalReferencesCamera.TakeDamage();
            Destroy(gameObject);
        }
    }

    public void Downgrade ()
    {
        if (downgrade != null)
        {
            EnemyMove newDowngrade = Instantiate(downgrade,transform.position,Quaternion.identity,GlobalReferencesCamera.EnemyParent).GetComponent<EnemyMove>();
            newDowngrade.speed = speed;
            newDowngrade.pathPointer = pathPointer;
            newDowngrade.Path = Path;

            BlowUp();

        }
        else
            BlowUp();
    }

    void BlowUp()
    {
        GlobalReferencesCamera.UpdateScore();
        Destroy(gameObject);
    }
}
