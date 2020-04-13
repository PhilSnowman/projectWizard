using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickToDissolve : MonoBehaviour
{
    Animator animator;

    private void OnMouseDown()
    {
        animator.SetTrigger("dissolve");
    }

    private void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
