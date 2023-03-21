using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrigger : MonoBehaviour
{
    public GameObject activeMouse;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Mouse collider entered");
        if (other.gameObject.tag == "Player")
        {
            activeMouse.SetActive(true);
            Destroy(gameObject);
        }

    }
}
