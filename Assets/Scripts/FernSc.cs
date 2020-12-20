using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FernSc : MonoBehaviour
{
    Animator anim;
    bool x;
    public int i;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("large", false);
        x = ObjectManager.activity;       
    }

    // Update is called once per frame
    void Update()
    {
        if (x != ObjectManager.activity)
        {
            if (i == 0) anim.SetBool("large", true);
            else anim.SetBool("large", false);
            i++;
            if (i > 2) i = 0;
           
            x = ObjectManager.activity;
            Debug.Log("i " + i);
        }
    }
}
