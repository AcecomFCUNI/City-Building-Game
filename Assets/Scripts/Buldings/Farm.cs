using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building, IProfitable
{
    [SerializeField] private float timeToIncreaseFood;

    private void Start() 
    {
        StartCoroutine(CalculateCityParameters());    
    }
    public IEnumerator CalculateCityParameters()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToIncreaseFood);
            Debug.Log("food");
            City.Instance.Food += 2;
            UIController.Instance.UpdateCityData();    
        }
    }
}
