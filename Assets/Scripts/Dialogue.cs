using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject ParentCanvas;
    //public TextMeshProUGUI messageText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.T)) {
            ParentCanvas.SetActive(false);

            //Unpause Game
            Time.timeScale = 1;
        }
    }
}
