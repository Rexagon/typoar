using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MenuControlsScript : MonoBehaviour {

    GameObject i;
    public string act;
    public string deact;
    bool ent;
    void Start()
    {
        i = GameObject.Find("Interface");
    }
    public void Enter()
    {
        ent = true;
    }
    public void Exit()
    {
        ent = false;
    }

    public void ExitLevel()
    {
        StartCoroutine(Stoped());  
    }

    IEnumerator Stoped()
    {
        yield return new WaitForSeconds(3);
        if (ent == true)
        {
            SceneManager.LoadScene(0);
        }
    }
    public void Aktiv()
    {
        StartCoroutine(Act());
    }
    IEnumerator Act()
    {
        yield return new WaitForSeconds(1f);
        if (ent == true)
        {
            i.transform.Find(act).gameObject.SetActive(true);
        }
    }
    public void Deaktiv()
    {
        StartCoroutine(Deact());
    }
    IEnumerator Deact()
    {
        yield return new WaitForSeconds(1f);
        if (ent == true)
        {
            i.transform.Find(deact).gameObject.SetActive(false);
        }
    }
}
