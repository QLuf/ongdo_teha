using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End : MonoBehaviour {
    public GameObject meaning;
    public GameObject loading;
    public Text wordText;
    public Text text;
	// Use this for initialization
	void Start () {
        CaseWord caseWord = gameObject.GetComponent<CaseWord>();
        wordText = GameObject.Find("wordText").GetComponent<Text>();
        //  meaning.SetActive(false);
        //  loading.SetActive(false);
        text.text = wordText.text;
        StartCoroutine(Wait1(5.0f));
        
    }

    // Update is called once per frame
    void Update() { }
    private IEnumerator Wait1(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene(0);
        }
    }

}
