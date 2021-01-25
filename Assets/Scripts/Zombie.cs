using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float hp = 100;
    public Transform target;
    public float speed = 10;
    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        target = Player.instance.transform;
        controller = GetComponent<CharacterController>();

        if (GetComponent<AudioSource>())
            GetComponent<AudioSource>().volume = VolumeSetting.volume;
    }

    // Update is called once per frame
    void Update()
    {
        var v = target.position - transform.position;
        controller.SimpleMove(v.normalized* speed*Time.deltaTime);
        transform.LookAt(target);


        var dis = Vector3.Distance(target.position, transform.position);
        if(dis<0.5&&Time.timeScale>0)
        {
            v = target.position -transform.position;
            target.GetComponent<CharacterController>().SimpleMove(v.normalized * 50);
            Player.instance.hp -= 10;
            Player.instance.hpUI.text = Player.instance.hp.ToString();
            if (Player.instance.hp <= 0)
            {
                Time.timeScale = 0;
                GameManager.instance.End();
            }
        }

    }
}
