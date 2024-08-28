using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Chronological spacing between spawns
    [SerializeField] float spaceTime = 3f;
    [SerializeField] float enemySpeed = 10f;

    private Transform EnemyParent;

    [SerializeField] EnemyMove[] Enemies;
    [SerializeField] GameObject[] Path;
    [SerializeField] Vector2[] Timing;

    // Keeps track of which enemies have been spawned
    private int pathPointer = 0;
    // Records the time between spawns;
    private float timeBetween = 0;
    // Time till next spawn
    private float timeTill = 0;

    private bool reachedEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        timeTill = (Timing[pathPointer][0] + 1) * spaceTime;
        EnemyParent = GlobalReferencesCamera.EnemyParent;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!reachedEnd)
        {
            //Spawn enemies whenever time is up, use a pointer variable to prevent old enemies from being spawned again
            timeBetween += Time.fixedDeltaTime;

            if (timeBetween > timeTill)
            {
                timeBetween = 0;

                //spawn the enemy
                //print("Spawned!");
                EnemyMove spawnedObj = Instantiate(Enemies[(int)Timing[pathPointer][1]], Path[0].transform.position, Quaternion.identity, EnemyParent);

                spawnedObj.Path = Path;
                spawnedObj.speed = enemySpeed;

                //increments the pointer
                pathPointer++;
                if (pathPointer == Timing.Length)
                    reachedEnd = true;
                else
                    timeTill = (Timing[pathPointer][0] + 1) * spaceTime;
                //gets the new ETA
            }
        }
        else
        {
            GlobalReferencesCamera.startupScript.noMoreEnemies = true;
        }
    }
}
