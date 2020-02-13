using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



public class GameManager : MonoBehaviour
{

    void OnApplicationQuit()
    {

    }

    public string playerName;
    public int level;
    public bool canContinue;
    public SopaDeLetras sopaActiva;
    public List<string> posiblesPalabras;

    static public GameManager instance;

    public event Action<int,int> onLetterSelected;
    public event Action onCancelSelection;
    public event Action onConfirmSelection;
    public event Action<bool,int> onSelectionTest;


    public void CloseApp()
    {
        GameData data = new GameData(sopaActiva,playerName);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(Application.persistentDataPath + "/game.sav",FileMode.Create);
        bf.Serialize(fs,data);
        fs.Close();
        Application.Quit();
    }

    
    public void selectionTest(bool okey,int idx)
    {
        if(onSelectionTest != null )
            onSelectionTest(okey,idx);
    }

    public void letterSelected(int x,int y)
    {
        if(onLetterSelected != null)
            onLetterSelected(x,y);
    }

    public void cancelSelection()
    {
        if( onCancelSelection != null)
            onCancelSelection();
    }

    public void confirmSelection()
    {
        if(onConfirmSelection != null)
        {
            onConfirmSelection();
        }
        if(sopaActiva.IsSolved())
        {
            next();
        }
    }


    public void NewGame()
    {
        level = 0;
        sopaActiva = new SopaDeLetras(level);
        changeScene("Game");
    }

    public void ContinueGame()
    {
        changeScene("Game");
    }



    public void setPlayerName(string name)
    {
        playerName = name;
    }

    public void borrar()
    {
        if(File.Exists(Application.persistentDataPath + "/game.sav"))
        {
            Debug.Log(Application.persistentDataPath + "/game.sav");
            File.Delete(Application.persistentDataPath + "/game.sav");
        }
        Application.Quit();
    }

    void Awake()
    {
        bool hasName = true;
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += onSceneLoad;
        DontDestroyOnLoad(gameObject);


        if( File.Exists(Application.persistentDataPath + "/game.sav") )
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream os = new FileStream(Application.persistentDataPath + "/game.sav", FileMode.Open );
                GameData data = bf.Deserialize(os) as GameData;

                playerName = data.playerName;
                if(playerName == "")
                    hasName = false;
                if(data.hasSopa)
                {
                    level = data.level;
                    sopaActiva = new SopaDeLetras(data);
                }
            }
            catch(Exception ex)
            {
                hasName = false;
                sopaActiva = null;
            }
        }
        else
            hasName = false;

        SetWords();

        if(!hasName)
        {
            changeScene("GetUserName");
        }


    }

    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    

    void Start()
    {

    }   

    void SetWords()
    {
        TextAsset file = Resources.Load<TextAsset>("palabras");
        string[] lines = file.text.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
        );
        for(int i =0 ; i < lines.Length ; i++)
        {
            if(lines[i].Trim() != "")
                posiblesPalabras.Add(lines[i]);
        }

    }


    public void next()
    {   
        level++;
        if(level == 5)
        {
            sopaActiva = null;
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            sopaActiva = new SopaDeLetras(level);
            SceneManager.LoadScene("Game");
        }

    }

    void onSceneLoad(Scene scene,LoadSceneMode mode)
    {

    }

    void Update()
    {
        var fps = 1.0f / Time.deltaTime;
        if(fps < 5) 
        {
            Debug.Break();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().name == "Game")
                changeScene("MainMenu");
            else if(SceneManager.GetActiveScene().name == "MainMenu")
                CloseApp();
        }
    }
}
