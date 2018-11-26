using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackArea : MonoBehaviour {
    public string areaName;
    public bool recordable = true;

    void Start() {
        if (string.IsNullOrEmpty(this.areaName)) {
            this.areaName = this.gameObject.name;
        }
    }
}
