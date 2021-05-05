using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private SaveLoadManager<MapData> saveLoad;
    private MapData mapData;
    private TransitionLoader transitionLoader;
    private string savedMapName;

    // Start is called before the first frame update
    void Start()
    {
        transitionLoader = GetComponent<TransitionLoader>();
        saveLoad = new SaveLoadManager<MapData>("map_data");

        if (mapData != null)
            savedMapName = $"{(MapName)mapData.mapLevel}_TitleCard";
        else
            savedMapName = "Level1_TitleCard";
    }

    public void ContinueLevel(){
        transitionLoader.LoadLevel(savedMapName);
    }

    
}
