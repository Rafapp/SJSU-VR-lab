using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class CSVDataWriter : MonoBehaviour
{
    private List<string[]> rowData = new List<string[]>();

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Save();
    }
    
    void Save(){

        // Creating First row of titles manually..
        string[] rowDataTemp = new string[3];
        rowDataTemp[0] = "Student ID";
        rowDataTemp[1] = "Tensile Score";
        rowDataTemp[2] = "Poisson Score";
        rowData.Add(rowDataTemp);

        for(int i = 0; i < 10; i++){
            rowDataTemp = new string[3];
            rowDataTemp[0] = UnityEngine.Random.Range(0, 999999999).ToString(); // Student ID
            rowDataTemp[1] = UnityEngine.Random.Range(0, 10).ToString(); // Tensile Score
            rowDataTemp[2] = UnityEngine.Random.Range(0,10).ToString(); // Poisson Score
            rowData.Add(rowDataTemp);
        }

        string[][] output = new string[rowData.Count][];

        for(int i = 0; i < output.Length; i++){
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();
        
        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));
        
        
        string filePath = getPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    // Following method is used to retrive the relative path as device platform
    private string getPath(){
        #if UNITY_EDITOR
        return Application.dataPath +"/CSV/"+"Saved_data.csv";
        #elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
        #elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
        #else
        return Application.dataPath +"/"+"Saved_data.csv";
        #endif
    }
}
