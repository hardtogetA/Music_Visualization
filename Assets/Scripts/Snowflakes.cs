using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowflakes : MonoBehaviour
{
    private float timeFactor;
    private float timeStep;

    public GameObject snowflakePrefab;
    public Transform geometry;

    private void Start()
    {
        timeStep = (1f / Music_Visualization.instance.totVol) < 1.8f ? (1f / Music_Visualization.instance.totVol) : 1.8f;
        timeFactor = Time.time + timeStep;
    }

    void Update()
    {
        if(Time.time > timeFactor)
        {
            timeStep = (1f / Music_Visualization.instance.totVol) < 1.8f ? (1f / Music_Visualization.instance.totVol) : 1.8f;
            Instantiate(snowflakePrefab, transform);
            timeFactor += timeStep;
        }
    }

}
