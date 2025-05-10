using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
    }

    public float moveSpeed;

    public Animator anim; // refrence till animatorn har med att saker ser bra ut :)

    public float pickupRange = 1.5f;

    //public Weapon activeWeapon;

    public List<Weapon> unassignedWeapons, assignedWeapons;

    public int maxWeapons = 3;

    [HideInInspector]
    public List<Weapon> fullyLevelledWeapons = new List<Weapon>();

    // Start is called before the first frame update
    void Start()
    {
        AddWeapon(Random.Range(0, unassignedWeapons.Count));
    }

    // Update is called once per frame
    void Update()
    {
        //använder unitys inbyggda funktioner :)
        Vector3 MoveInput = new Vector3(0f, 0f, 0f);
        MoveInput.x = Input.GetAxisRaw("Horizontal");
        MoveInput.y = Input.GetAxisRaw("Vertical");

        

        MoveInput.Normalize(); //ger playern en konstant hastighet för att fixa så att den inte rör sig snabbare i Z axeln
        //Debug.Log(MoveInput); används för att kolla att move scripten fungerar
        transform.position += MoveInput * moveSpeed * Time.deltaTime; // varje component i unity har en transfrom, denna rad påverkar objectets transform alltså position X, Y ,Z
                                                                      // time.deltaTime gör så att hastigheter är oberoende av frameraten


        if(MoveInput != Vector3.zero) // Parametrar för att kolla om animationen ska spelas.
        { 
            anim.SetBool("IsMoving", true);
        } else
        {
            anim.SetBool("IsMoving", false);
        }

    }

    public void AddWeapon(int weaponNumber)
    {
        if(weaponNumber < unassignedWeapons.Count)
        {
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);

            unassignedWeapons[weaponNumber].gameObject.SetActive(true);
            unassignedWeapons.RemoveAt(weaponNumber);
        }
    }

    public void AddWeapon(Weapon weaponToAdd)
    {
        weaponToAdd.gameObject.SetActive(true);

        assignedWeapons.Add(weaponToAdd);
        unassignedWeapons.Remove(weaponToAdd);
    }

}
