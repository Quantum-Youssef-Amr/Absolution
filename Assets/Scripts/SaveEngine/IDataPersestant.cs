using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersestant 
{

    void loadData(Data data);

    void SaveData(ref Data data);

}
