using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController instance;

    private void Awake()
    {
        instance = this;
    }

    public DamageNumber numberToSpawn;
    public Transform NumberCanvas;

    private List<DamageNumber> numberPool = new List<DamageNumber>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            spawnDmg(57f, new Vector3(4, 3, 0));
        }
    }

    public void spawnDmg(float dmgAmount, Vector3 location)
    {
        int rounded = Mathf.RoundToInt(dmgAmount);

        //DamageNumber newDmg = Instantiate(numberToSpawn, location, Quaternion.identity, NumberCanvas);

        DamageNumber newDmg = GetFromPool();

        newDmg.Setup(rounded);
        newDmg.gameObject.SetActive(true);

        newDmg.transform.position = location;
    }

    public DamageNumber GetFromPool()
    {
        DamageNumber numberToOutput = null;

        if(numberPool.Count == 0)
        {
            numberToOutput = Instantiate(numberToSpawn, NumberCanvas);
        }else
        {
            numberToOutput = numberPool[0];
            numberPool.RemoveAt(0);
        }

        return numberToOutput;
    }

    public void PlaceInPool(DamageNumber numberToPlace)
    {
        numberToPlace.gameObject.SetActive(false);

        numberPool.Add(numberToPlace);
    }
}
