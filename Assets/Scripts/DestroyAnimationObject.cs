using UnityEngine;

public class DestroyAnimationObject : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //Random size and rotation
        transform.localScale = Vector2.one * Random.Range(1.0f, 2.0f);
        transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));

        //Destroy when animation is finished
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
}