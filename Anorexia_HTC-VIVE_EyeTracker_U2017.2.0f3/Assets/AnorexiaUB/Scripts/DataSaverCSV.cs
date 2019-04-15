using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class DataSaverCSV : MonoBehaviour {
    private List<string[]> rowData = new List<string[]>();
    private List<string> temporalData = new List<string>();
    private string pacientID = "";
    private int sessionNumber = 0;
    private string dataPath = "";

	// Use this for initialization
	void Start () {
        SetPacientInfo(InGameData.PacientId, InGameData.Sn);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPacientInfo(string id, int sn)
    {
        pacientID = id;
        sessionNumber = sn;
    }

    public void AddData(string value)
    {
        temporalData.Add(value);
    }

    public void AppendData()
    {
        string[] row = new string[temporalData.Count];
        for (int i = 0; i < temporalData.Count; i++) row[i] = temporalData[i];
        rowData.Add(row);
        temporalData.Clear();
    }

    private bool SearchPacientFile()
    {
        string filePath = getFolderPath();
        filePath += pacientID + "_session" + sessionNumber + ".csv";
        dataPath = filePath;
        if (System.IO.File.Exists(filePath))
        {
            Debug.Log("patient already make this session. would you rewrite this data ?");
            return true;
        }
        else {
            Debug.Log("new session added. preparing to save data");
            return false;
        }
    }

    public void SaveData()
    {
        TestInputData();
        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ";";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
        {
            //for(int i =0; i < output[index].Length;i++)if(i != output[index].Length -1) d += output[index][i] + ",";
            string d = string.Join(delimiter, output[index]);
            Debug.Log("daata " + d);
            sb.AppendLine(d);
        }
            

        if (!SearchPacientFile())
        {
            
        }
        StreamWriter outStream = System.IO.File.CreateText(dataPath);
        outStream.WriteLine(sb);
        outStream.Close();
        //string filePath = getFolderPath();
    }

    private void TestInputData()
    {
        AddData("Name");
        AddData("ID");
        AddData("Income");
        AppendData();

        //// You can add up the values in as many cells as you want.
        for (int i = 0; i < 10; i++)
        {
            AddData("Sushanta" + i); // name
            AddData("" + i); // ID
            AddData("$" + UnityEngine.Random.Range(5000, 10000)); // Income
            AppendData();
        }
    }

    // Following method is used to retrive the relative path as device platform
    private string getFolderPath()
    {
#if UNITY_EDITOR
        string filePath = System.IO.Directory.GetCurrentDirectory() + "/CSV_Data/";
        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

        filePath += pacientID + "/";
        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

        return filePath;
#elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
#else
        string filePath = System.IO.Directory.GetCurrentDirectory() + "/CSV_Data/";
        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

        filePath += pacientID + "/";
        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
        return filePath;
#endif
    }
}
