using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonList<T>
{
    public List<T> list;
}

public class GameManager : MonoBehaviour {

    protected static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public JsonList<int> codeList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        codeList = new JsonList<int>();

        string json = PlayerPrefs.GetString("codeList");
        if (!string.IsNullOrEmpty(json))
        {
            codeList = JsonUtility.FromJson<JsonList<int>>(json);
        } else
        {
            codeList.list = new List<int>();
        }
                    
      //  DontDestroyOnLoad(this.gameObject);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnApplicationQuit()
    {
        string json = JsonUtility.ToJson(codeList);
        if (!string.IsNullOrEmpty(json))
        {
            PlayerPrefs.SetString("codeList", json);
        }
    }

}
