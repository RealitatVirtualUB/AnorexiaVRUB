using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodePoints : MonoBehaviour {
    public Transform[] zeros;
    public Transform[] fulls;
    public Transform[] reals;

    public Slider principalSlider;

    void Start()
    {
        GameObject principalSliderObject = GameObject.Find("PrincipalSlider");
        this.principalSlider = principalSliderObject.GetComponent<Slider>();
    }

    void Update()
    {
        for (int i = 0; i < this.zeros.Length; i++)
        {
            float value = principalSlider.value;
            Vector3 distanceVector = this.fulls[i].position - this.zeros[i].position;

            this.reals[i].position = this.zeros[i].position + distanceVector * value;
        } 
    }
}
