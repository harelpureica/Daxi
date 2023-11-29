using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataSizer : MonoBehaviour
{
    [Serializable]
    public class GameSaveData
    {
        public int UnlockedLevels;
        public int Gums;
        public int Planks;
        public int Shields;
        public int Hearts;
    }

    private void Start()
    {
        // Create an instance of your game's saved data
        GameSaveData savedData = new GameSaveData()
        {
            UnlockedLevels = 10,
            Gums= 10,
            Planks= 10,
            Shields= 10,
            Hearts= 10,            
        };
       
        try
        {
            int dataSize = CalculateDataSize(savedData);
            Debug.Log("Size of saved data: " + dataSize + " bytes");
        }
        catch (Exception ex)     
        {
            Debug.LogException(ex);
        }
        // Calculate the size of the saved data
      

      
    }

    private int CalculateDataSize(object data)
    {
        // Serialize the data to a memory stream
        using (MemoryStream memoryStream = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, data);

            // Return the size of the serialized data
            return (int)memoryStream.Length;
        }
    }
}
