using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    private UIController uIController;
    public int Cash { get; set; }
    public int Day { get; set; }
    public float CurrentPopulation { get; set; }
    public float PopulationCeiling { get; set; }
    public int CurrentJobs { get; set; }
    public int JobsCeiling { get; set; }
    public float Food { get; set; }
    public int[] buildingCounts = new int[4];
    void Start()
    {
        uIController = GetComponent<UIController>();
        Cash = 50;
        uIController.UpdateCityData();

    }

    public void EndTurn()
    {
        Day++;
        CalculatePopulation();
        CalculateJobs();
        CalculateFood();
        CalculateCash();
        Debug.Log("Day ended.");
        uIController.UpdateCityData();
        uIController.UpdateDayCount();
    }
    private void CalculateJobs()
    {
        JobsCeiling = buildingCounts[3] * 10;
        CurrentJobs = Mathf.Min((int)CurrentPopulation, JobsCeiling);
    }
    private void CalculateCash()
    {
        Cash += CurrentJobs * 2;
    }
    public void DepositCash(int cash)
    {
        Cash += cash;
    }
    private void CalculateFood()
    {
        Food += buildingCounts[2] * 4;
    }
    private void CalculatePopulation()
    {
        PopulationCeiling = buildingCounts[1] * 3;
        if(Food >= CurrentPopulation && CurrentPopulation < PopulationCeiling)
        {
            Food -= CurrentPopulation * 0.25f;
            CurrentPopulation = Mathf.Min(CurrentPopulation + Food * .25f, PopulationCeiling);
        }
        else if(Food < CurrentPopulation)
        {
            CurrentPopulation -= (CurrentPopulation - Food) * .5f;
        }

    }
}
