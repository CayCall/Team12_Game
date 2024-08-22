using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class MainMenuCam : MonoBehaviour
{
    public Transform camPosition;
    public TextMeshProUGUI text;
    public MainMenuTextData menuTxt;


    // Start is called before the first frame update
    void Start()
    {
        menuTxt.RandomiseSplash();
        menuTxt.OutputEntry();
        text.text = menuTxt.OutputText;
    }

    // Update is called once per frame
    void Update()
    {
        camPosition.transform.Rotate(0, 5 * Time.deltaTime, 0);
        
      

        
    }
}
