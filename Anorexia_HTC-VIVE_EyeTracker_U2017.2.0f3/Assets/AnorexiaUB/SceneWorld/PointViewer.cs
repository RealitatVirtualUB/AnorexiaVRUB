using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointViewer : MonoBehaviour {
    public Color color;
    public float radius;

    void OnDrawGizmos()
    {
        Gizmos.color = this.color;
        Gizmos.DrawSphere(this.transform.position, 0.035f);
    }
}
