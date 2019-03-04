using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    string pacientID = "";
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
        int i;
        if (int.TryParse(newId.text, out i))
        {
            pacientID = newId.text;
            Debug.Log("id is valid: " + i);
        }

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

    public void ChargePacient()
    {
        if(pacientID != "")
        {
            InGameData.PacientId = pacientID;
            InGameData.ImcIncrement = imcIncrement;
            InGameData.Sn = numOfSession;
            //Debug.Log(  "pacient id: "+InGameData.PacientId+
            //            " imc incremented desired: "+ InGameData.ImcIncrement+
            //            " session selected: "+InGameData.Sn);
            SceneManager.LoadScene(2);
        }
        else Debug.Log("invalid value");
    }

    public void CreateNewPacient()
    {
        if (pacientID != "")
        {
            InGameData.PacientId = pacientID;
            InGameData.IsClinic = clinicPacient;
            Debug.Log("new pacient created. pacient id: " + InGameData.PacientId + " isclinic ? "+ InGameData.IsClinic);
            SceneManager.LoadScene(1);
        }
        else Debug.Log("incorrect Id");
    }
}


