using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int gunType;
    public float damage = 10;
    public float cd;
    public int ammo;
    public bool available=false;
    public bool onGround = false;
    public Bullet bullet;

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<AudioSource>())
            GetComponent<AudioSource>().volume = VolumeSetting.volume;
        
    }

    float cdNow = 0;
    // Update is called once per frame
    void Update()
    {
        if (cdNow > 0)
            cdNow -= Time.deltaTime;

        if (onGround)
            transform.Rotate(new Vector3(0, Time.deltaTime * 100, 0));
    }


    public void Shoot()
    {
        if (cdNow > 0||ammo<1)
            return;
        cdNow = cd;
        ammo--;

        var ob=Instantiate(bullet.gameObject);
        Vector3 target = transform.position+transform.forward;
        ob.transform.position = transform.position;
        ob.transform.localScale = bullet.transform.lossyScale;
        var b=ob.GetComponent<Bullet>();
        b.towards = transform.forward;
        b.damage = damage;

        ob.transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        ob.SetActive(true);

        GetComponent<AudioSource>().Play();
    }

}
