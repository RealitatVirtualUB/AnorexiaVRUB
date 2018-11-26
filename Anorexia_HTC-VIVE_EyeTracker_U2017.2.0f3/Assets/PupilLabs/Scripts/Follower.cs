using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {
    public RectTransform canvasTransform;
    public RectTransform rectTransform;
    public Camera _camera;
    public Transform following;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*
        Vector3 pos = this.following.transform.position;  // get the game object position
        Vector3 viewportPoint = _camera.WorldToViewportPoint(pos);  //convert game object position to VievportPoint
        // Vector3 screenPoint = _camera.WorldToScreenPoint(pos);  //convert game object position to VievportPoint

        // set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
        this.rectTransform.anchorMin = viewportPoint;
        this.rectTransform.anchorMax = viewportPoint;
        */
        //then you calculate the position of the UI element
        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

        Vector2 ViewportPosition = _camera.WorldToViewportPoint(this.following.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * canvasTransform.sizeDelta.x) - (canvasTransform.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * canvasTransform.sizeDelta.y) - (canvasTransform.sizeDelta.y * 0.5f)));
        
        //now you can set the position of the ui element
        this.rectTransform.anchoredPosition = WorldObject_ScreenPosition;
    }
}
