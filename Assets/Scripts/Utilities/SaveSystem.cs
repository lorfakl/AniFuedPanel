using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Utilities;
using System;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Utilities.SaveOperations
{
    public enum DataCategory
    {
        User,
        Profile,
        Group,
        Inventory,
        VirtualCurrency,
        Avatar,
        Settings,
        StatisticRecord
    }

    public static class SaveSystem
    {
        public static Dictionary<DataCategory, string> FileNames = new Dictionary<DataCategory, string>();
        public static void Save<T>(T data, DataCategory c)
        {

            string name = c.ToString();
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/"
                + name + ".bruh";

            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            
            formatter.Serialize(fileStream, data);

            if(!FileNames.ContainsKey(c))
            {
                FileNames.Add(c, path);
            }
            else
            {
                FileNames[c] = path;    
            }

            fileStream.Close();
        }

        public static T Load<T>(DataCategory c) where T : class
        {
            FileStream file;
            try
            {
                string fileName = c.ToString();
                string path = Application.persistentDataPath + "/" +
                    fileName + ".bruh";
                HelperFunctions.Log("Loading file from: " + path);  
                if (File.Exists(path))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    file = new FileStream(path, FileMode.Open);

                    T data = binaryFormatter.Deserialize(file) as T;
                    file.Close();

                    if(!FileNames.ContainsKey(c))
                    {
                        FileNames.Add(c, path);
                    }    
                    return data;
                }
                else
                {
                    HelperFunctions.Log("File path: " + path + " does NOT exist...idiot");
                    //file.Close();
                    return default(T);
                }

                
            }
            catch(Exception e)
            {
                HelperFunctions.CatchException(e);
                return default(T);
            }

        }

        public static T ConvertToObject<T>(byte[] data) where T : class
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using(MemoryStream ms = new MemoryStream(data))
            {
                object obj = binaryFormatter.Deserialize(ms);
                try
                {
                    return (T)obj;
                }
                catch(Exception e)
                {
                    HelperFunctions.CatchException(e);
                    return default;
                }
            }
        }
    
        public static byte[] PrepareFileForUpload(DataCategory c)
        {
            string path = "";
            if(FileNames.ContainsKey(c))
            {
                path = FileNames[c];
            }
            else
            {
                path = Application.persistentDataPath + "/"
                + c.ToString() + ".bruh";
            }

            string fileString = "";

            if(File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        fileString = sr.ReadToEnd();
                    }
                }

                return Encoding.UTF8.GetBytes(fileString);
            }
            else
            {
                HelperFunctions.Warning("Path supplied does not exist: " + path);
                return default(byte[]);
            }
            
        }

        public static async Task<Sprite> ConvertBytesToSprite(byte[] pngByte)
        {
            Sprite pngSprite = null;
            using(MemoryStream me = new MemoryStream(pngByte))
            {
            }
            await Task.FromResult(pngSprite);
            return pngSprite;
        }
    }
}

