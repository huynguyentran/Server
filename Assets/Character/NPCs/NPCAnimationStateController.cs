using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationStateController : MonoBehaviour
{
    Animator animator;
    public GameObject dialogueContainer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueContainer.activeSelf)
        {
             animator.SetBool("isTalking", true);
        }
        else
        {
             animator.SetBool("isTalking", false);
        }
    }
}
