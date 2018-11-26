using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflection : MonoBehaviour {

    public ReflectionProbe probe;

    void Awake() {

    }

    void Update()
    {
        this.transform.position = new Vector3(
            this.transform.position.x,
            Camera.main.transform.position.y - 0.3f,
            this.transform.position.z
        );

        probe.RenderProbe();
    }
}
