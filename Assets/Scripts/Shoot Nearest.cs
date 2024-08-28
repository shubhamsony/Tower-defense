using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootNearest : MonoBehaviour
{
    private Transform EnemyParent;
    private Transform Misc;

    [SerializeField] private float[] shotDelay;
    private int shotPointer = 0;
    [SerializeField] private float distance = 100f;
    private float bulletSpeed = 20f;
    private float counter = 0f;

    [SerializeField] private Transform Nozzle;
    [SerializeField] private GameObject Bullet;

    private Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        EnemyParent = GlobalReferencesCamera.EnemyParent;
        Misc = GlobalReferencesCamera.Misc;
        originalPos = transform.position;
        bulletSpeed = GlobalReferencesCamera.bulletSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counter += Time.fixedDeltaTime;
        if (counter > shotDelay[shotPointer])
        {
            //Do not reset counter here so the tower shoots instantly whenever things are in range and cooldown is done.
            //Reset counter in the shoot nearest function
            //counter = 0f;
            shotPointer = (shotPointer + 1) % shotDelay.Length;
            EnemyMove[] enemies = EnemyParent.GetComponentsInChildren<EnemyMove>();

            if (enemies != null)
            {
                float dist = Mathf.Infinity;
                int closestindex = 0;

                // Finds the closest enemy
                for (int i = 0; i < enemies.Length; i++)
                {

                    float t = Vector3.Distance(transform.position, enemies[i].transform.position);

                    if (dist > t)
                    {
                        dist = t;
                        closestindex = i;
                    }

                }

                if (dist < distance)
                {
                    GameObject closestEnemy = enemies[closestindex].gameObject;
                    shootNearest(closestEnemy);
                }
            }
        }


    }

    void shootNearest(GameObject closestEnemy)
    {
        counter = 0f;
        Vector3 t = closestEnemy.transform.position;
        //animate the shot
        transform.LookAt(new Vector3(t.x, transform.position.y, t.z));
        //shoot the bullet at the enemy and give it a target.

        Bullet newBullet = Instantiate(Bullet, Nozzle.position, Quaternion.identity, Misc).GetComponent<Bullet>();

        newBullet.target = closestEnemy;
        newBullet.speed = bulletSpeed;
    }
}
