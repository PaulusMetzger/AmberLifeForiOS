using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoSc : MonoBehaviour
{
    bool x;
    bool go;
    public float distanse;
    public float step;
    public Vector3 destinationPoint, startPoint;
    Quaternion startRotation;
    Animator anim;
   
    void Start()
    {
        x = ObjectManager.activity;
        go = false;
        step = 2;
        startPoint= new Vector3(transform.position.x, transform.position.y, transform.position.z);
        destinationPoint = new Vector3(Random.Range(26, 38), Random.Range(-4, 8), Random.Range(-6, 6));
        StartCoroutine(ChangeDestPoint());
        anim = GetComponent<Animator>();
        startRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
    }
    
    void Update()
    {       
        if (x != ObjectManager.activity)
        {
            go = !go; 
            x = ObjectManager.activity;             
        }
        if (go)
        {
            distanse = Vector3.Distance(transform.position, destinationPoint);
            if (distanse >= 0.1)
            {
                transform.position = Vector3.MoveTowards(transform.position, destinationPoint, step * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(destinationPoint- transform.position, transform.up), step * Time.deltaTime);
                anim.SetBool("fly", true);
            }
            else
            {
                anim.SetBool("fly", false);
            }
        }
        else
        {
            distanse = Vector3.Distance(transform.position, startPoint);
            if (distanse >= 0.1)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPoint, step * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(startPoint - transform.position, transform.up), step * Time.deltaTime);
                anim.SetBool("fly", true);
            }
            else
            {
                anim.SetBool("fly", false);
                transform.rotation = startRotation;
            }
        }
    }

    IEnumerator ChangeDestPoint()
    {
        yield return new WaitForSeconds(Random.Range(2,3));
        destinationPoint = new Vector3(Random.Range(26, 38), Random.Range(-4, 8), Random.Range(-6, 6));
        StartCoroutine(ChangeDestPoint());
    }
}
