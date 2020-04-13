using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonFX : MonoBehaviour
{

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetBool("Highlighted") == true)
        {
            FindObjectOfType<AudioManager>().Play("menuHighlight");
        }

        if (animator.GetBool("Selected") == true)
        {
            FindObjectOfType<AudioManager>().Play("menuHighlight");
        }
    }
}
