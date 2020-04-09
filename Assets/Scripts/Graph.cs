using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    const float PI = Mathf.PI;
    public Transform pointPrefab;
    [Range(10, 100)]
    public int resolution = 10;
    Transform[] points;
    public GraphFunctionName function;
    static GraphFunction[] functions = {
        SineFunction, Sine2DFunction, MultiSineFunction, MultiSine2DFunction, Ripple, Cylinder, Sphere, Torus
    };

    void Awake() {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one /5f * step;
        points = new Transform[resolution * resolution];
        for(int i = 0; i < points.Length; i++) {
            Transform point = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }  
    }

    void Update() {
        float t = Time.time;
        GraphFunction f = functions[(int)function];
        float step = 2f / resolution;
        for(int i = 0, z = 0; z <resolution; z++) {
            float v = (z + 0.5f) * step - 1f;
            for (int x = 0; x < resolution; x++, i++) {
                float u = (x + 0.5f) * step - 1f;
                points[i].localPosition = f(u, v, t);
            }
        }

    }

    static Vector3 SineFunction(float x, float z, float t) {
        //return Mathf.Sin(PI * (x + t));
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(PI * (x + t));
        p.z = z;
        return p;
    }

    static Vector3 MultiSineFunction(float x, float z, float t) {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(PI * (x + t));
        p.y += Mathf.Sin(2f * PI * (x + 2f * t)) /2f;
        p.y *= 2f / 3f;
        p.z = z;
        return p;
    }

    static Vector3 Sine2DFunction(float x, float z, float t) {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(PI * (x + t));
        p.y += Mathf.Sin(PI * (z + t));
        p.y *= 0.5f;
        p.z = z;
        return p;
    }

    static Vector3 MultiSine2DFunction(float x, float z, float t) {
        Vector3 p;
        p.x = x;
        p.y = 4f * Mathf.Sin(PI * (x + z + t * 0.5f));
        p.y += Mathf.Sin(PI * (x + t));
        p.y += Mathf.Sin(2f * PI * (z + 2f * t)) * 0.5f;
        p.y *= 1f / 5.5f;
        p.z = z;
        return p;
    }

    static Vector3 Ripple(float x, float z, float t) {
        Vector3 p;
        float d = Mathf.Sqrt(x * x + z * z);
        p.x = x;
        p.y = Mathf.Sin(PI * (4f * d - t));
        p.y /= 1f + 10f * d;
        p.z = z;
        return p;
    }

    static Vector3 Cylinder(float u, float v, float t) {
        Vector3 p;
        float r = .8f + Mathf.Sin(PI * (6f * u + 2f * v + t)) * 0.2f;
        p.x = r * Mathf.Sin(PI * u);
        p.y = v;
        p.z = r * Mathf.Cos(PI * u);
        return p;
    }

    static Vector3 Sphere(float u, float v, float t) {
        Vector3 p;
        float r = 0.8f + Mathf.Sin(PI * (6f * u + t)) * 0.1f;
        r += Mathf.Sin(PI * (4f * v + t)) * 0.1f;
        float s = r * Mathf.Cos(PI * 0.5f * v);
        p.x = s * Mathf.Sin(PI * u);
        p.y = r * Mathf.Sin(PI * 0.5f * v);
        p.z = s * Mathf.Cos(PI * u);
        return p;
    }

    static Vector3 Torus(float u, float v, float t) {
        Vector3 p;
        float r1 = 0.65f + Mathf.Sin(PI * (6f * u + t)) * 0.1f;
        float r2 = 0.2f + Mathf.Sin(PI * (4f * v + t)) * 0.05f;
        float s = r2 * Mathf.Cos(PI * v) + r1;
        p.x = s * Mathf.Sin(PI * u);
        p.y = r2 * Mathf.Sin(PI * v);
        p.z = s * Mathf.Cos(PI * u);
        return p;
    }
}
