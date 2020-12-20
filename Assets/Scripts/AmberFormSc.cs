using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmberFormSc : MonoBehaviour
{    
    bool x;
    bool action;
    Color colorBegin;
    Color colorEnd;
    public float Delay;
    
    // Start is called before the first frame update
    void Start()
    {
       x = ObjectManager.activity;
       action = true;
       colorBegin = GetComponent<Renderer>().material.color;
       colorEnd = new Color(colorBegin.r, colorBegin.g, colorBegin.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (x != ObjectManager.activity)
        {
            if (action)
            {
                Delay += Time.deltaTime;
                GetComponent<Renderer>().material.color = Color.Lerp(colorBegin, colorEnd, Delay * 0.3f);
                if (GetComponent<Renderer>().material.color.a == 0)
                {
                    action = false;
                    x = ObjectManager.activity;
                    Delay = 0;
                }
            }
            else
            {
                Delay += Time.deltaTime;
                GetComponent<Renderer>().material.color = Color.Lerp(colorEnd, colorBegin,  Delay * 0.3f);
                if (GetComponent<Renderer>().material.color.a == 1)
                {
                    action = true;
                    x = ObjectManager.activity;
                    Delay = 0;
                }
            }            
        }
    }
}
