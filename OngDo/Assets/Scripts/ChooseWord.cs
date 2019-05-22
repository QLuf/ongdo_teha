using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using System.IO;

[Serializable]
public class CodeWordSet
{
    public int code;
    public string word;
    public CodeWordSet(int code, string word)
    {
        this.code = code;
        this.word = word;
    }

    public override string ToString()
    {
        return "code: " + code + ", word: " + word;
    }
}

public class ChooseWord : MonoBehaviour
{
    public GameObject Loading;
    public CaseWord caseWord;
    public testEx TestEx;
    public ImagePrinter imagePrinter;
    public string serverIp;
    public int port;
    GameManager gm;

    //string word;


    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("Manager").GetComponent<GameManager>();
        CaseWord caseWord = gameObject.GetComponent<CaseWord>();
        testEx TestEx = gameObject.GetComponent<testEx>();
        ImagePrinter imagePrinter = gameObject.GetComponent<ImagePrinter>();
        String path2 = Application.dataPath + "\\ip.txt";
        serverIp = File.ReadAllText(path2);
        Debug.Log(serverIp);
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int generateCode()
    {
        int r = UnityEngine.Random.Range(1000, 9999);

        if (!gm.codeList.list.Contains(r))
        {
            gm.codeList.list.Add(r);
        }
        else
        {
            generateCode();
        }

        return r;
    }

    public void onBuyClick()
    {
        //  GameObject buttonClicked = EventSystem.current.currentSelectedGameObject;
        //  string word = buttonClicked.GetComponentInChildren<Text>().text;    
        int code = generateCode();
        string word = caseWord.word;

        CodeWordSet set = new CodeWordSet(code, word);

        string json = JsonUtility.ToJson(set);

        SendData(json);
        imagePrinter.codeTxt.text = code.ToString();
        imagePrinter.wordTxt.text = word.ToString();
        imagePrinter.onCaptureClick();
       
        // print ma code ra
      // TestEx.PrintImage();
        // enable ongDo video
        Loading.SetActive(true);

    }


    void SendData(string message)
    {
        try
        {
            TcpClient client = new TcpClient(serverIp, port);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
            Debug.Log("Sent:" + message);

            data = new byte[256];
            string responseData = string.Empty;

            int bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);
            Debug.Log("Received: " + responseData);

            stream.Close(); 
            client.Close();
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("ArgumentNullException: {0}", e);
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
    }
}
