using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LevelChanger : MonoBehaviour {
    public Animator animator;
    private IEnumerator coroutine;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
    public void FadeToLevel()
    {
        animator.SetTrigger("FadeOut");
        coroutine = Wait1(0.23f);
        StartCoroutine(coroutine);
    }
    public void FadeToLevel2()
    {
        animator.SetTrigger("FadeOut");
        coroutine = Wait2(0.23f);
        StartCoroutine(coroutine);
    }
    public void FadeToLevel3()
    {
        animator.SetTrigger("FadeOut");
        coroutine = Wait3(0.23f);
        StartCoroutine(coroutine);
    }
    private IEnumerator Wait1(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene(1);
        }
    }
    private IEnumerator Wait2(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene(2);
        }
    }
    private IEnumerator Wait3(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene(3);
        }
    }
}
