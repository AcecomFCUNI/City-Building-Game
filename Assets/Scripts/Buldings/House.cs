using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building, IProfitable
{
    [SerializeField] private float timeToIncreasePopulation;

    private void Start() 
    {
        City.Instance.PopulationCeiling += 3;
        City.Instance.CurrentPopulation++;
        UIController.Instance.UpdateCityData();
        StartCoroutine(CalculateCityParameters());    
    }

    public IEnumerator CalculateCityParameters()
    {
        
        while(true)
        {
            yield return new WaitForSeconds(timeToIncreasePopulation);
            Debug.Log("population");
            CalculatePopulation();
            UIController.Instance.UpdateCityData();
        }
    }

    private void CalculatePopulation()
    {
        float food = City.Instance.Food;
        float currentPopulation = City.Instance.CurrentPopulation;
        float populationCeiling = City.Instance.PopulationCeiling;
        if(food >= currentPopulation && currentPopulation < populationCeiling)
        {
            City.Instance.Food -= currentPopulation * 0.25f;
            City.Instance.CurrentPopulation = Mathf.Min(currentPopulation + food * .25f, populationCeiling);
        }
        else if(food < currentPopulation)
        {
            City.Instance.CurrentPopulation -= (currentPopulation - food) * .5f;
        }
    }
}
