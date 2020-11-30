using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScript : MonoBehaviour
{
    // Script for TestGameplay
    // Start is called before the first frame update

    /*
    Process:
    When user touches the checkpoint:
        - activated save function
        - change the state of the checkpoint
        - change the image

    When game starts:
        - load the scene and saved files
        - add player states
    */
    //Config Variables
    
    public GameObject scoreTxtObject, player, tileObject;

    //Variables to Save
    private int score = 0;
    private Vector3 coords;
    private string checkpointTag;

    //Save path
    private string savePath;

    void Start()
    {
        savePath = Application.persistentDataPath + "saved_data.soda";
        Load();
    }

    //adds score
    public void AddScore(int amount){
        score += amount;
    }

    public void SetCheckpoint(Vector3 checkpointCoords, string name){
        coords = checkpointCoords;
        checkpointTag = name;
        Save();
    }

    //save game
    public void Save(){
        coords += new Vector3(0, 1, 0);

        //I/O
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        PlayerData playerData = new PlayerData();
        playerData.score = score;
        playerData.coords = new float[] {coords[0], coords[1], coords[2]};
        playerData.checkpointTag = checkpointTag;

        //save part
        bf.Serialize(file, playerData);
        file.Close();
    }

    public void Load(){
        if (File.Exists(savePath)){
            //I/O
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);

            //load part
            PlayerData playerData = (PlayerData)bf.Deserialize(file);
            file.Close();

            score = playerData.score;
            float[] c = playerData.coords;
            coords = new Vector3(c[0], c[1], c[2]);
            player.transform.position = coords;

            /*
            Sample Hierarchy of GameObject Tile
                Tile
                    -> Obstacles
                    -> Checkpoint1
                        -> CheckpointScript

            Process:
                - Find the child gameobject using the name
                - Call the ChangeState() of the child script
            */

            //name of the loaded checkpoint
            checkpointTag = playerData.checkpointTag;

            //gets the list its children
            Transform[] childrenObj = tileObject.GetComponentsInChildren<Transform>();

            foreach(Transform obj in childrenObj){
                //if name matches with checkpointTag, change the state
                if (obj.name == checkpointTag){
                    CheckpointScript objScript = obj.GetComponent<CheckpointScript>();
                    objScript.ChangeState();
                    break;
                }
            }
        }
    }


    // Update is called once per frame
    void LateUpdate()
    {
        Text scoreTxt = scoreTxtObject.GetComponent<Text>();
        scoreTxt.text = "Score: " + score;
    }
}

[System.Serializable]
class PlayerData{
    public int score;
    public float[] coords;

    //checkpoint data
    public string checkpointTag;
}