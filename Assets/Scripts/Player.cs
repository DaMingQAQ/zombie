using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    private CharacterController controller;
    Animator animator;
    public int speed = 10;
    public List<Weapon> weapons = new List<Weapon>();
    public Transform weaponUI;
    public Text ammoUI;
    public Text hpUI;
    Weapon weaponNow;
    public float hp = 100;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        ChooseWeapon(0);
    }

    void ChooseWeapon(int num)
    {
        if (!weapons[num].available)
            return;
        weaponNow = weapons[num];
        weaponUI.GetChild(0).GetComponent<Image>().color = Color.white;
        weaponUI.GetChild(1).GetComponent<Image>().color = Color.white;
        weaponUI.GetChild(2).GetComponent<Image>().color = Color.white;
        weaponUI.GetChild(num).GetComponent<Image>().color = new Color(200f/255, 200f / 255, 200f / 255,1);

        weapons.ForEach(x => x.gameObject.SetActive(false));
        weaponNow.gameObject.SetActive(true);
        ammoUI.text = weaponNow.ammo.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
        controller.SimpleMove(movement);
        animator.SetBool("Walking", movement != Vector3.zero);


        //rotation
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 200,1<<0))
        {
            Vector3 target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }



        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChooseWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChooseWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ChooseWeapon(2);

        if(Input.GetMouseButton(0))
        {
            weaponNow.Shoot();
            ammoUI.text = weaponNow.ammo.ToString();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        var weapon = other.GetComponent<Weapon>();
        if (weapon)
        {
            weapons[weapon.gunType].ammo += weapon.ammo;
            ammoUI.text = weaponNow.ammo.ToString();
            Destroy(weapon.gameObject);
        }


    }



}
