using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcetobiumSc : MonoBehaviour
{
    ArcPlant[] plants;
    int regulator;
    bool proof;
    GameObject PlantObject;

    void Start()
    {
        plants = GetComponentsInChildren<ArcPlant>();
        regulator = 1;
        proof = ObjectManager.activity;
        foreach (ArcPlant plant in plants)
        {
            PlantObject = plant.gameObject;
            if (plant.Generation == regulator)
            {
                PlantObject.SetActive(true);
            }
            else PlantObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (proof != ObjectManager.activity)
        {
            if (regulator < 3)
            {
                regulator++;
                foreach (ArcPlant plant in plants)
                {
                    PlantObject = plant.gameObject;
                    if (plant.Generation == regulator)
                    {
                        PlantObject.SetActive(true);
                    }
                }
            }
            else
            {
                regulator = 1;
                foreach (ArcPlant plant in plants)
                {
                    PlantObject = plant.gameObject;
                    if (plant.Generation == regulator)
                    {
                        PlantObject.SetActive(true);
                    }
                    else PlantObject.SetActive(false);
                }                    
            }

            proof = ObjectManager.activity;
        }
    }
}
