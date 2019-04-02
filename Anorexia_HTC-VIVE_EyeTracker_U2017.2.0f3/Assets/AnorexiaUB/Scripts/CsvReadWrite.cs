using UnityEngine;
using MiniJSON;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class CsvReadWrite : MonoBehaviour{

    //list of exel data
    private List<List<string>> bodyPartsData = new List<List<string>>();
    private List<string> columnData = new List<string>();

    private string bodyPart = "";
    private int columnCounter = 0;

    public void Save()
    {
        string[][] output = new string[bodyPartsData.Count][];
        Debug.Log(bodyPartsData.Count);
        for (int i = 0; i < output.Length; i++)
        {
            output[i] = bodyPartsData[i].ToArray();
            Debug.Log(bodyPartsData[i].Count +  " counter column");
        }

        int length = output.GetLength(0);
        string delimiter = ",";
        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++) sb.AppendLine(string.Join(delimiter, output[index]));

#if UNITY_EDITOR
        string filePath = System.IO.Directory.GetCurrentDirectory() + "/CSV/";
        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath); // returns a DirectoryInfo object

        Debug.Log(filePath);
        //change id of the avatar
        //string fileName = subjectIdField.text + ".txt";
        string fileName = InGameData.PacientId + "_session"+ InGameData.Sn + ".csv";
#else
        string filePath = System.IO.Directory.GetCurrentDirectory() + "/CSV/";
        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath); // returns a DirectoryInfo object
        string fileName = InGameData.PacientId + "_session"+ InGameData.Sn + ".csv";
#endif
        Debug.Log("creating document");
        StreamWriter outStream = System.IO.File.CreateText(filePath + fileName);
        Debug.Log("adding data");
        outStream.WriteLine(sb);
        outStream.Close();
    }

    public void SetupBodyPartData(string bodyPartName){
        bodyPart = bodyPartName;
        if (columnData.Count != 0) AddBodyPartData();
        ResetDataColumn();
        columnData.Add(bodyPart);
        Debug.Log("body part setted: " + bodyPart);
    }

    public void AddData(float data){
        columnData.Add(data.ToString());
        Debug.Log("added data: "+ data + " " + columnData[columnData.Count-1]);
    }

    public void ResetDataColumn(){columnData.Clear();}

    public void AddBodyPartData(){
        bodyPartsData.Add(columnData);
        columnCounter++;
    }
}
