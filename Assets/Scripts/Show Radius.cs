using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRadius : MonoBehaviour
{
    [SerializeField] private GameObject trailer;
    private GameObject[] trails;
    private Transform Misc;
    private float[] theta;
    [SerializeField] private float radius = 3;
    [SerializeField] private float speed = 5;
    private bool Renabled = true;

    private void Start()
    {
        trails = new GameObject[4];
        theta = new float[4];
        Misc = GlobalReferencesCamera.Misc;
        for (int i = 0; i < 4; i++)
        {
            trails[i] = Instantiate(trailer, Misc);
            theta[i] += (Mathf.PI * (i + 1)) / 2;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Renabled)
        {
            for (int i = 0; i < 4; i++)
            {
                theta[i] = (Time.fixedDeltaTime * speed + theta[i]) % (Mathf.PI * 2);
                trails[i].transform.position = new Vector3(Mathf.Sin(theta[i]) * radius, 0, Mathf.Cos(theta[i]) * radius) + transform.position;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
            Renabled = Renabled ? false : true;
    }
}
