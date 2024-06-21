using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimation : MonoBehaviour
{
    [SerializeField] private string anim_name;
    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim.Play(anim_name);
    }

    
}
