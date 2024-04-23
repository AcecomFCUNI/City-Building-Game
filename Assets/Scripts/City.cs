using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    private int jobsCeiling;
    private int cash;
    private float currentPopulation;
    public static City Instance { get; private set; }
    public int Day { get; set; }
    public float PopulationCeiling { get; set; }
    public int CurrentJobs { get; set; }
    public float Food { get; set; }
    public int[] buildingCounts = new int[4]; //0 is for roads, 1 is for houses, 2 is for farms, 3 is for factorys

    public int JobsCeiling 
    { 
        get => jobsCeiling;
        set
        {
            jobsCeiling = value;
            CurrentJobs = Mathf.Min((int)CurrentPopulation, jobsCeiling);
        }
    }

    public int Cash 
    { 
        get => cash; 
        set
        {
            cash = value;
        } 
    }

    public float CurrentPopulation 
    { 
        get => currentPopulation;
        set
        {
            currentPopulation = value;
            CurrentJobs = Mathf.Min((int)CurrentPopulation, jobsCeiling);
        }
    }

    private void Awake() 
    {
        SetUpGame();
    }

    void Start()
    {
        Cash = 50;
        Day = 1;
        buildingCounts[1] = 1;
        buildingCounts[2] = 1;
        buildingCounts[3] = 1;
        UIController.Instance.UpdateCityData();
    }

    private void SetUpGame()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this) Destroy(gameObject);
    }

    public void EndTurn()
    {
        Day++;
        CalculateJobs();
        CalculateCash();
        Debug.Log("Day ended.");
        UIController.Instance.UpdateCityData();
        UIController.Instance.UpdateDayCount();
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
}
