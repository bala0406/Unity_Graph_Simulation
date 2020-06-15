using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{

    public Transform pointPrefab;

    [Range(10, 100)]
    public int resolution = 10;
    
    Transform[] points;

    
    public GraphFunctionName function;

    static GraphFunction[] functions = { SineFunction, MultiSineFunction };

    private void Awake()
    {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one / 5f;
        Vector3 position;
        position.z = 0;
        points = new Transform[resolution];
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            position.y = position.x * position.x * position.x;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    void Start()
    {

    }


    void Update()
    {
        float time = Time.time;
        GraphFunction graphFunction = functions[(int)function];


        if (function == 0)
        {
            graphFunction = SineFunction;
        }
        else
        {
            graphFunction = MultiSineFunction;
        }

        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = graphFunction(position.x, time);
            point.localPosition = position;
        }
    }

    static float SineFunction(float x, float t)
    {
        return Mathf.Sin(Mathf.PI * (x + t));
    }

    static float MultiSineFunction(float x, float t)
    {
        float y = Mathf.Sin(Mathf.PI * (x + t));
        y += Mathf.Sin(2f * Mathf.PI * (x + 2f * t)) / 2f;
        y *= 2f / 3f;
        return y;
    }
}
