using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeMenu : MonoBehaviour {

    public enum menus
    {
        BASEMENU,
        NEWMENU,
        LOADMENU,
        NUMOFMENUS
    }

    public List<GameObject> menusInScene;
    public List<Sprite> marcks;

    //pacients information
    int pacientID = 0;
    float imcIncrement = 1;
    int numOfSession = 0;
    bool clinicPacient = true;

	// Use this for initialization
	void Start () {
        if(menusInScene.Count == (int)menus.NUMOFMENUS)
        {
            for (int i = 0; i < menusInScene.Count; i++)
            {
                if (i == 0) menusInScene[i].SetActive(true);
                else menusInScene[i].SetActive(false);
            }
        }
        else
        {
            Debug.Log("error found. number of menus aren't equal to the number of posible menus");
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeMenu(int idMenu)
    {
        for (int i = 0; i < menusInScene.Count; i++)
        {
            if (i == idMenu) menusInScene[i].SetActive(true);
            else menusInScene[i].SetActive(false);
        }
    }

    public void UpdatePacientId(Text newId)
    {
        if (int.TryParse(newId.text, out pacientID)) Debug.Log("id is valid: " + pacientID);
        else Debug.Log("invalid id. try to put other");
    }

    public void UpdateIMCincrement(Text newiIMC)
    {
        if (float.TryParse(newiIMC.text, out imcIncrement)) Debug.Log("new increment setted : " + imcIncrement);
        else Debug.Log("error to set new imc increment");
    }

    public void UpdateNumOfSession(Text newNoS)
    {
        if (int.TryParse(newNoS.text, out numOfSession)) Debug.Log("new number of session setted : " + numOfSession);
        else Debug.Log("error to set new number of session");
    }

    public void IsClinic(Image reference)
    {
        if (clinicPacient)
        {
            if (marcks != null) reference.sprite = marcks[1];
            else Debug.Log("we need to set mack sprites");
            clinicPacient = false;
        }
        else
        {
            if (marcks != null) reference.sprite = marcks[0];
            else Debug.Log("we need to set mack sprites");
            clinicPacient = true;
        }
    }
}


