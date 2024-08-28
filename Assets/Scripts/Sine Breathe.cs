using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;

public class SineBreathe : MonoBehaviour
{
    [SerializeField] private float speed;
    private float ypos;
    private float sine;
    [SerializeField] private float range;
    [SerializeField] private float rot;
    // Start is called before the first frame update
    void Start()
    {
        ypos = transform.position.y;
        sine = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 t = transform.position;
        sine = (Time.deltaTime * speed + sine)%(Mathf.PI * 2);
        transform.position = new Vector3(t.x, ypos + Mathf.Sin(sine) * range, t.z);

        transform.eulerAngles += new Vector3(0, rot * Time.deltaTime, 0);
    }
}
