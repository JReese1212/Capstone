using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WorldHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> CarList;
    [SerializeField] private WorldGenerator MyWorld;
    private GameObject MyCar;
    private bool ActiveWorld = false;
    // random
    [SerializeField] private bool RandomSeed = false;
    [Range(-100000, 100000)] [SerializeField] public int MasterSeed;


    public GameObject cop;
    private Vector3 loc = new Vector3(60, 1, 0);
    private GameObject insCop;
    private int tmp = 0;
    public GameObject healthBar;
    public float totalDistance = 0;
    private Vector3 previousLoc;
    private Vector3 startLoc = new Vector3(50, 1, 50);


    public void BuildWorld()
    {
        ActiveWorld = true;
        SetSeed();
        MyWorld.BuildWorld();
        MyCar = Instantiate(CarList[0], new Vector3(50, 1, 50), Quaternion.Euler(0, 0, 0));
        healthBar = GameObject.Find("Health Bar");
        var c = healthBar.GetComponent<healthBarScript>();
        MyCar.GetComponent<CarHealth>().healthBar = c;


        insCop = Instantiate(cop, loc, Quaternion.identity) as GameObject;
        cop = GameObject.FindWithTag("Cop");
        cop.GetComponent<NavMeshAgent>().Warp(loc);

        tmp = 1;
       
    }

    public void DestroyWorld()
    {
        ActiveWorld = false;
        MyWorld.DestroyWorld();
        Destroy(MyCar);
        Destroy(cop);
    }

    public bool isActive()
    {
        return ActiveWorld;
    }

    void FixedUpdate()
    {
        if(tmp == 1)
        {
            cop.GetComponent<NavMeshAgent>().SetDestination(MyCar.transform.position);
            if (MyCar.transform.position != startLoc)
            {
                RecordDistance();
                //Debug.Log(totalDistance);
            }
        }
        
        
    }

    private void SetSeed()
    {
        if (RandomSeed)
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
        }
        else
        {
            Random.InitState(MasterSeed);
        }
    }


    public void RecordDistance()
    {
        totalDistance += Vector3.Distance(MyCar.transform.position, previousLoc);
        previousLoc = MyCar.transform.position;
    }
}
