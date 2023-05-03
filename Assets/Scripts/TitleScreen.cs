using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class TitleScreen : MonoBehaviour
{
    /* General Arrow Indexes:
     * 0 = Start Game
     * 1 = Options
     * 2 = Exit Game
     */
    public GameObject arrow;
    public List<Vector2> arrowPositions;
    private int arrowIndex;

    public AudioSource scroll;              // Scroll SFX prefab
    public AudioSource select;              // Select SFX prefab
    private int pastArrowIndex;             // Used to track when the arrow switches indexes for SFX

    void Start()
    {
        arrowIndex = 0;
        arrow.GetComponent<RectTransform>().anchoredPosition = arrowPositions[arrowIndex];
    }

    void Update()
    {
        pastArrowIndex = arrowIndex;

        if (Input.GetKeyDown(KeyCode.W))    //Navigate Up
        {
            arrowIndex = Mathf.Clamp(--arrowIndex, 0, arrowPositions.Count - 1);
        }
        if (Input.GetKeyDown(KeyCode.S))    //Navigate Down
        {
            arrowIndex = Mathf.Clamp(++arrowIndex, 0, arrowPositions.Count - 1);
        }
        arrow.GetComponent<RectTransform>().anchoredPosition = arrowPositions[arrowIndex];  //Moves between first and last positions

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))  //Select Current Option
        {
            switch (arrowIndex)
            {
                case 0:
                    StartGame();
                    break;
                case 1:
                    OpenOptions();
                    break;
                case 2:
                    ExitGame();
                    break;
            }
        }


        if (pastArrowIndex != arrowIndex)           // Checks if the arrow has changed index and plays sound effect if true
        {
            Instantiate(scroll);
        }


    }

    public void StartGame()
    {
        Instantiate(select);                        // Plays select SFX
        SceneManager.LoadScene(3);
        Debug.Log("Start Button Clicked");
    }

    public void OpenOptions()
    {
        Instantiate(select);                        // Plays select SFX
        Debug.Log("Options Button Clicked");
    }

    public void ExitGame()
    {
        Instantiate(select);                        // Plays select SFX
        Application.Quit();
        Debug.Log("Exit Button Clicked");
    }

    public void MouseOverButton(int index)
    {
        arrowIndex = index;

        if (pastArrowIndex != arrowIndex)          // Checks if the arrow has changed index and plays sound effect if true
        {
            Instantiate(scroll);
        }
    }



    //Custom Inspector button to add current arrow position into the list
    public void AddArrowPosition()
    {
        arrowPositions.Add(arrow.GetComponent<RectTransform>().anchoredPosition);
    }
}

[CustomEditor(typeof(TitleScreen))]
public class TitleScreenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TitleScreen titleScript = (TitleScreen)target;
        if (GUILayout.Button("Add Arrow Position"))
        {
            titleScript.AddArrowPosition();
        }
    }
}
