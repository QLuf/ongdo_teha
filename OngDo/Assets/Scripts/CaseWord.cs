using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CaseWord : MonoBehaviour {
    public GameObject Meaning;
    public Text wordText;
    public Text meaningText;
    public static List<MeanList> listMeaning = new List<MeanList>();
    public string word;

    // Use this for initialization
    void Start () {
        LoadOnFile();
       // Meaning = GameObject.Find("Meaning").GetComponent<GameObject>();
    }
    private void Awake()
    {
 
    }
    // Update is called once per frame
    void Update () {
		
	}
    public void onWordClick()
    {
        GameObject buttonClicked = EventSystem.current.currentSelectedGameObject;
     
        //  word = buttonClicked.GetComponentInChildren<Text>().text;
        //  Debug.Log("word " + word);
        Meaning.SetActive(true);
        wordText = GameObject.Find("wordText").GetComponent<Text>();
        meaningText = GameObject.Find("meaningText").GetComponent<Text>();
        WordInfo wordInfo = buttonClicked.GetComponent<WordInfo>();
        Word word = wordInfo.word;
        this.word = word.ToString();
        switch (word)
        {
            case Word.Phúc:
                LoadMean(0);
                break;
            case Word.Lộc:
                LoadMean(1);
                break;
            case Word.Thọ:
                LoadMean(2);
                break;
            case Word.Nhẫn:
                LoadMean(3);
                break;
            case Word.Trí:
                LoadMean(4);
                break;
            case Word.Tâm:
                LoadMean(5);
                break;
            case Word.Thành:
                LoadMean(6);
                break;
            case Word.Dũng:
                LoadMean(7);
                break;
            case Word.Tài:
                LoadMean(8);
                break;
            case Word.An:
                LoadMean(9);
                break;
            case Word.Mỹ:
                LoadMean(10);
                break;
            case Word.Đức:
                LoadMean(11);
                break;
        }

    }
    public void Back()
    {
        Meaning.SetActive(false);
    }

    public void LoadMean(int pos)
    {   
        wordText.text = listMeaning[pos].word.ToString();
        meaningText.text = listMeaning[pos].mean.ToString();
    }
    public void LoadOnFile()
    {
        String path = Application.dataPath + "\\meaning.txt";
        string line;
        // Read the file and display it line by line.  
        StreamReader file = File.OpenText(path);
    
        while ((line = file.ReadLine()) != null)
        {
            // đưa vào list
            String[] chuoi = line.Split('#');
           MeanList meaning = new MeanList();
            meaning.word = chuoi[0].ToString();
            meaning.mean = chuoi[1].ToString();

          listMeaning.Add(meaning);
        }
        file.Close();
    }
    public class MeanList
    {
        public String word { get; set; }
        public String mean{ get; set; }
    }
}
