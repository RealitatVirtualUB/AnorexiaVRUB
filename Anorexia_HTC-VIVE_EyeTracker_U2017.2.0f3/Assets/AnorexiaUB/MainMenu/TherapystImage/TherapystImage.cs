using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TherapystImage : MonoBehaviour {
    public RectTransform canvasTransform;
    public RectTransform rectTransform;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate() {
        this.rectTransform.sizeDelta = new Vector2(this.canvasTransform.rect.height, this.canvasTransform.rect.height);
    }
}
