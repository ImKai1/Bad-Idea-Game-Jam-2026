using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonExample : MonoBehaviour
{
    string path;

    List<string> list; //replace with settings class (or something) that saves the data.


    private void Start()
    {
        path = Application.persistentDataPath + "/settings.json";
        LoadData();
        

        list = new List<string>();
        list.Add("Hello");
    }

    public void SetData()
    {
        // implement later, call to update the temporary settings before actually saving
    }

    public void SaveData() // call function to save data to the json
    {
        string json = JsonConvert.SerializeObject(list, Formatting.Indented);
        File.WriteAllText(path, json);
    }
    public void LoadData() // Load the settings from the json
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            list = JsonConvert.DeserializeObject<List<string>>(json);
        }
        else
        {
            // Make new settings file if doesn't exist yet
        }
    }
}