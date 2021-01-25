using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage=10;
    public float liveTime = 3;
    public float speed = 10;
    public float back = 20;
    public Vector3 towards;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        liveTime -= Time.deltaTime;
        if (liveTime < 0)
            Destroy(gameObject);

        transform.Translate(towards * speed * Time.deltaTime,Space.World);
    }


    private void OnTriggerEnter(Collider other)
    {
        var zb = other.GetComponent<Zombie>();
        if (zb)
        {
            var v = other.transform.position - zb.target.position;
            zb.GetComponent<CharacterController>().SimpleMove(v.normalized* back);
            zb.hp -= damage;
            zb.GetComponent<AudioSource>().Play();
            if (zb.hp < 0)
            {
                Destroy(zb.gameObject);
                GameManager.instance.SpawnAt(zb.transform.position);
            }
                
            Destroy(gameObject);
        }
    }
}
