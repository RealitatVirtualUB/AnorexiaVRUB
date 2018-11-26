using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeformP : MonoBehaviour {
	public Slider slider;
	public float ax = 1f;
	public float ay = 1f;
	public float az = 1f;
    public InputField inputField;
    public Mid mid;

	public float x() {
		return this.ax * this.slider.value;
	}

	public float y() {
		return this.ay * this.slider.value;
	}

	public float z() {
		return this.az * this.slider.value;
	}
}
