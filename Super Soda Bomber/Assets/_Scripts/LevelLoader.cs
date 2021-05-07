using UnityEngine;
using SuperSodaBomber.Events;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject continueButton;

    public Button newButton;
    [SerializeField] private VoidEvent onLevelNew;
    [SerializeField] private SceneEvent onLevelContinue;

    private SaveLoadManager<MapData> saveLoad;
    private MapData mapData;
    private SceneIndex savedMapIndex;

    // Start is called before the first frame update
    void Start()
    {
        saveLoad = new SaveLoadManager<MapData>("map_data");
        mapData = saveLoad.LoadData();

        Debug.Log(mapData == null);

        ConfigureBtnBehaviour(mapData == null);

        if (mapData != null)
            //moves scene to title card
            savedMapIndex = (SceneIndex)(mapData.mapLevel + 2);
        else{
            continueButton.SetActive(false);
            savedMapIndex = SceneIndex.Level1_Game;
        }
    }

    void ConfigureBtnBehaviour(bool isNull){
        if (isNull){
            newButton.onClick.AddListener(StartLevel);
        }
        else{
            newButton.onClick.AddListener(() => onLevelNew?.Raise());
        }
    }

    public void StartLevel(){
        TransitionLoader.UseMainMenuEvents = true;
        GameManager.current.MoveScene(savedMapIndex, false);
        onLevelContinue?.Raise(savedMapIndex);
    }
    
}
