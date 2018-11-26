using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawForwardRay : MonoBehaviour {

    void OnDrawGizmos(){
        // Draws a 10 unit long red line in front of the object
        Gizmos.color = Color.green;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 10;
        Gizmos.DrawRay(transform.position, direction);
        Debug.DrawRay(transform.position, direction, Color.green);

    }
}
