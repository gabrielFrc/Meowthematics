using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperAnimation : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void AnimatePaper(string stateName)
    {
        bool animAlreadyPlaying = anim.GetCurrentAnimatorStateInfo(0).IsName("PaperOut") || anim.GetCurrentAnimatorStateInfo(0).IsName("PaperIn");
        if (!animAlreadyPlaying)
        {
            anim.Play(stateName);
            AnimatorStateInfo anim_state = anim.GetCurrentAnimatorStateInfo(0);

            StartCoroutine(WaitForAnimEnd(anim_state));
        }
    }

    private IEnumerator WaitForAnimEnd(AnimatorStateInfo anim_state)
    {
        if(anim_state.IsName("PaperOutIdle"))
        {
            gameObject.SetActive(false);
        }

        else if(anim_state.IsName("PaperIn"))
        {
            gameObject.SetActive(true);
        }

        else
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(WaitForAnimEnd(anim.GetCurrentAnimatorStateInfo(0)));
        }
    }
}
