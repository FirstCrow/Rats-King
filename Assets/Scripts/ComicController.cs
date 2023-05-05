using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ComicController : MonoBehaviour
{
    public GameObject[] panels;
    public float duration = 2f;
    float timer = 0f;
    [SerializeField] int currentPanel = 0;
    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < panels.Length; i++)
        {
            panels[i].GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
        }

        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !isMoving && currentPanel < panels.Length-1)
        {
            StartCoroutine(ShowNextPanel());
        }
        else if(Input.GetKeyDown(KeyCode.Mouse0) && currentPanel == panels.Length-1)
        {
            SceneManager.LoadScene(2);
        }

        //Show next panel
        if (timer < duration)
        {
            timer += Time.deltaTime;
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        float t = timer / duration;
        panels[currentPanel].GetComponentInChildren<Image>().color = new Color(1, 1, 1, t);
    }

    IEnumerator ShowNextPanel()
    {
        isMoving = true;
        timer = 0f;

        Mathf.Clamp(++currentPanel, 0, panels.Length-1);

        yield return new WaitForSeconds(duration);
        isMoving = false;
    }
}
