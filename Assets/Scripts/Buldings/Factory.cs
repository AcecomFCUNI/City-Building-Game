using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Building, IProfitable
{
    private static int totalJobs;
    [SerializeField] private int workers;
    [SerializeField] private int timeBetweenProfits;

    private void Start() 
    {
        City.Instance.JobsCeiling += 10;
        UpdateWorkers();
        UIController.Instance.UpdateCityData();
        StartCoroutine(CalculateCityParameters());
    }

    private void UpdateWorkers()
    {
        totalJobs+=10 - workers;
        if(totalJobs > City.Instance.CurrentJobs)
        {
            workers = 10 - (totalJobs - City.Instance.CurrentJobs);
            totalJobs = City.Instance.CurrentJobs;
        }else workers = 10;
        UIController.Instance.UpdateCityData();
        Debug.Log(totalJobs);
    }

    public IEnumerator CalculateCityParameters()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeBetweenProfits);
            Debug.Log("jobs");
            UpdateWorkers();
            City.Instance.DepositCash(workers * 2);
            UIController.Instance.UpdateCityData();
        }
    }
}
