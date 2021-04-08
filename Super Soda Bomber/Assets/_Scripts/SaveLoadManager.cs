using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager: MonoBehaviour
{
    public string savePath { get; private set; }

    //used for save/load processes
    public BinaryFormatter bf = new BinaryFormatter();
    
    //path dir
    void Awake(){
        savePath = Application.persistentDataPath + "saved_data.soda";
    }
    /// 
    /// <summary>
    /// Deletes saved data
    /// </summary>
    public void ClearData(){
        File.Delete(savePath);
        Debug.Log("data erased!");
    }
}
