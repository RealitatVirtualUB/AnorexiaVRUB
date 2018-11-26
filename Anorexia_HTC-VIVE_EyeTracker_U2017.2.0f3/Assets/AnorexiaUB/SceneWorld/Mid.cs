using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mid : MonoBehaviour {
    public Transform r;
    public Transform l;

    public float getMid()
    {
        float value = (this.r.position - this.l.position).magnitude;
        return value;
    }
}
