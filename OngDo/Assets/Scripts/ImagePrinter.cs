using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Drawing.Printing;

public class ImagePrinter : MonoBehaviour {

    //private Texture2D imageTexture;
    //public SpriteRenderer image;
    //public string code;
    public Camera printCam;
    public string fileName;
    public RenderTexture printRenderTexture;
    //public event System.Drawing.Printing.PrintPageEventHandler PrintPage;
    private Texture2D imageTexture;
    public Text codeTxt;
    public Text wordTxt;
    
    // Use this for initialization
    void Start () {
        //   printCam = GameObject.Find("PrintCam").GetComponent<Camera>();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onCaptureClick()
    {
        StartCoroutine(Capture());

    }

    IEnumerator Capture()
    {
        yield return new WaitForEndOfFrame();
        RenderTexture currentRenderTexture = RenderTexture.active;
        RenderTexture.active = printRenderTexture;
        imageTexture = new Texture2D(printCam.pixelWidth, printCam.pixelHeight, TextureFormat.RGB24, false);

        imageTexture.ReadPixels(printCam.pixelRect, 0, 0);
        imageTexture.Apply();

        byte[] data = imageTexture.EncodeToPNG();
        Destroy(imageTexture);

        fileName = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";

        string path = Application.dataPath + "\\" + fileName;

        File.WriteAllBytes(path, data);

      //Debug.Log("written file to " + Application.dataPath + fileName);
        RenderTexture.active = currentRenderTexture;
        //IN ẢNH
        //Class1 runClass1 = new Class1();
        //runClass1.RunPrinter(Application.dataPath + "\\" + fileName, "DS-RX1 (Copy 1)");
        PrintImage(path, "DS-RX1");
    }

    void PrintImage(string path, string printerName)
    {
        PrintDocument pd = new PrintDocument();
        pd.DefaultPageSettings.PrinterSettings.PrinterName = printerName;
        pd.DefaultPageSettings.Landscape = true; //or false!

        pd.PrintPage += (sender, a) =>
        {
            System.Drawing.Image i = System.Drawing.Image.FromFile(path);
            a.Graphics.DrawImage(i, -9, -110, 620, 600);
        };

        pd.Print();
    }
}
