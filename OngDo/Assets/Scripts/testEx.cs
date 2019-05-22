
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
//using LibExample;


public class testEx : MonoBehaviour {
    public int frameRate = 24;
    public float startTime = 0.0f;
    public float endTime = 1.0f;
    public float timeScale = 0.1f;
    // public string printName = "screenshot";
    public ImagePrinter imagePrinter;
    int updateTime = 0;
    int doTimes;
    int frame = 0;
    float currentTime;
    float usingScale = 1.0f;
    //public Class1 runClass1 = new Class1();
	// Use this for initialization
	void Start () {
        ImagePrinter imagePrinter = gameObject.GetComponent<ImagePrinter>();
    }
	
	// Update is called once per frame
	void Update () {
        

	}
    public void PrintImage()
    {
        UnityEngine.Debug.Log(imagePrinter.fileName.ToString());
        //runClass1.RunPrinter(Application.dataPath + imagePrinter.fileName.ToString(), "DS-RX1 (Copy 1)");
        UnityEngine.Debug.Log("in");
    }
}
