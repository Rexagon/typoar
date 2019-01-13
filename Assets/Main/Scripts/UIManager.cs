using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text headerText;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        headerText.text = "Touch count: " + Input.touchCount;
    }
}
