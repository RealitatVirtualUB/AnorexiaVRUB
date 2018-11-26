using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelColliders : MonoBehaviour {

    public MeshBaker armsBaker;
    public MeshBaker chestBaker;
    public MeshBaker legsBaker;
    public Toggle armsButton;
    public Toggle chestButton;
    public Toggle legsButton;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnArmsActive(){
        armsBaker.EnableMesh();
        chestBaker.DisableMesh();
        legsBaker.DisableMesh();

    }

    public void OnChestActive(){
        armsBaker.DisableMesh();
        chestBaker.EnableMesh();
        legsBaker.DisableMesh();
    }

    public void OnLegsActive(){
        armsBaker.DisableMesh();
        chestBaker.DisableMesh();
        legsBaker.EnableMesh();
    }

    public void DisableAll(){
        armsButton.isOn = false;
        chestButton.isOn = false;
        legsButton.isOn = false;
        armsBaker.DisableMesh();
        chestBaker.DisableMesh();
        legsBaker.DisableMesh();
    }

}
