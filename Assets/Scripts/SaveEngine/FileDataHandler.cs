using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class FileDataHandler
{
    private string DataPath;
    private string DataSaveName;
    private bool UseEncryption = false;
    private readonly string EncryptionCodeWord ="OrPitaStudiosCEOIsTheSoloDiv";

    public FileDataHandler(string DataPath, string DataSaveName, bool Encryption = true)
    {
        this.DataPath = DataPath;
        this.DataSaveName = DataSaveName;
        this.UseEncryption = Encryption;
    }

    public Data load()
    {
        string fullpath = Path.Combine(DataPath, DataSaveName);
        Data loadedData = null;
        if (File.Exists(fullpath))
        {

            try
            {
                string datatoload = "";
                using(FileStream stream = new FileStream(fullpath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        datatoload = reader.ReadToEnd();
                    }
                }


                if (UseEncryption)
                    datatoload = EncryptDecrypt(datatoload);

                loadedData = JsonUtility.FromJson<Data>(datatoload);

            }
            catch (Exception)
            {

            }


        }
        return loadedData;
    }

    public void save(Data data)
    {
        string fullpath = Path.Combine(DataPath, DataSaveName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullpath));

            string datatoStore = JsonUtility.ToJson(data, true);

            if (UseEncryption)
                datatoStore = EncryptDecrypt(datatoStore);

            using( FileStream stream  = new FileStream(fullpath, FileMode.Create))
            {
                using( StreamWriter Writer = new StreamWriter(stream))
                {
                    Writer.Write(datatoStore);
                }
            }

        }
        catch (Exception)
        {

        }

    }
   
    public bool isSaved()
    {
        if(File.Exists(Path.Combine(DataPath, DataSaveName))) { return true; }
        else { return false; }
    }

    private string EncryptDecrypt( string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ EncryptionCodeWord[i % EncryptionCodeWord.Length]);
        }
        return modifiedData;
    }


}
