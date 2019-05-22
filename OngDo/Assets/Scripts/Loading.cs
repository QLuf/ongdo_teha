using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour {
    public GameObject End;
	// Use this for initialization
	void Start () {
        StartCoroutine(Wait1(15.0f));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private IEnumerator Wait1(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            End.SetActive(true);
        }
    }
}
