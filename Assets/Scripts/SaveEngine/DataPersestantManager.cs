using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersestantManager : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] bool useEncription = false;

    private Data GameData;
    private List<IDataPersestant> dataPersestantsObjects;
    private FileDataHandler DataHandler;

    public static DataPersestantManager instace { get; private set; }

    private void Awake()
    {
        if (instace != null)
            Debug.LogError("found more then one Data Persestant Manager in the scene");

        instace = this;

        this.DataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncription);
        this.dataPersestantsObjects = getAllDataPersestantObjects();
    }

    private void Start()
    {
        loadGame();
    }


    public void NewGame()
    {
        this.GameData = new Data();
    }

    public void saveGame()
    {

        foreach (IDataPersestant dataPersestant in dataPersestantsObjects)
        {
            dataPersestant.SaveData(ref GameData);
        }

        DataHandler.save(GameData);

    }


    public void loadGame()
    {
        this.GameData = DataHandler.load();

        if (this.GameData == null)
            NewGame();

        foreach (IDataPersestant dataPersestant in dataPersestantsObjects)
        {
            dataPersestant.loadData(GameData);
        }
    }

    public bool isSaved()
    {
        return DataHandler.isSaved();
    }

    private void OnApplicationQuit()
    {
        saveGame();
    }

    private List<IDataPersestant> getAllDataPersestantObjects()
    {
        IEnumerable<IDataPersestant> dataPersestants = FindObjectsByType<MonoBehaviour>(sortMode: FindObjectsSortMode.None).OfType<IDataPersestant>();

        return new List<IDataPersestant>(dataPersestants);
    }

}
