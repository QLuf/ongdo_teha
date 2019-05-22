using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour {
    public GameObject Firework1;
    public GameObject Firework2;
    public GameObject Firework3;
    // Use this for initialization
    void Start () {
        StartCoroutine(Wait(2f));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private IEnumerator Wait(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Firework1.SetActive(true);
            yield return new WaitForSeconds(1.3f);
            Firework2.SetActive(true);
            yield return new WaitForSeconds(waitTime);
            Firework3.SetActive(true);
        }
    }
}
