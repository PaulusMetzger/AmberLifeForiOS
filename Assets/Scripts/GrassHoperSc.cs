using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassHoperSc : MonoBehaviour
{
    Animator anim;
    
    public float jumpSize=20;
    public float jumpSpeed=50;
    bool x;
    bool move;
    bool reloadTime;
    bool returning;
    float dist;
    Vector3 destPoint, startPoint;
    Quaternion startRotation;

    void Start()
    {
        anim = GetComponent<Animator>();
        move = false;
        reloadTime = true;
        returning = false;
        x= ObjectManager.activity;
        startPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);       
        destPoint = startPoint + transform.forward * jumpSize;       
        Debug.Log(destPoint);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (x != ObjectManager.activity&&reloadTime) //прыжок кузнечика в сторону
        {
            StartCoroutine(MoveTime());
            anim.SetBool("jump", true);            
            destPoint = startPoint+transform.forward* jumpSize;            
            reloadTime = false;
            //5 секунд на прыжок и возвращение кузнечика
            StartCoroutine(ReloadOnce());
            startRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        }
       if (move)
        {
            if (!returning)    // прыжок
            {
                transform.position = Vector3.MoveTowards(transform.position, destPoint, jumpSpeed*Time.deltaTime);
                dist = Vector3.Distance(transform.position, destPoint);
                if (dist <= 0.2) returning = true;                
            }
            else             //возвращение из прыжка
            {
                anim.SetBool("go", true);
                anim.SetBool("jump", false);
                transform.position = Vector3.MoveTowards(transform.position, startPoint, jumpSpeed/8*Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(startPoint - transform.position, transform.up), jumpSpeed * Time.deltaTime);
                dist = Vector3.Distance(transform.position, startPoint);
                if (dist <= 0.2)
                {
                   returning = false;
                    move = false;
                    transform.position = startPoint;
                   transform.rotation = startRotation;
                   transform.Rotate(0, 180, 0);                   
                   anim.SetBool("go", false);
                }
            }
        }
    }

    IEnumerator ReloadOnce() //5 секунд на прыжок и возвращение кузнечика
    {
        yield return new WaitForSeconds(5);
        x = ObjectManager.activity;
        reloadTime = true;
    }
    IEnumerator MoveTime() //полсекунды задержки перед прыжком
    {
        yield return new WaitForSeconds(0.5f);
        move = true;
    }
}
