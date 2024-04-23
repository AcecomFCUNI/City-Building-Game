using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public static UIController Instance {get; private set;}
    [SerializeField] private Text dayText;
    [SerializeField] private Text cityText;

    private void Awake() 
    {
        SetUpGame();    
    }

    private void SetUpGame()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this) Destroy(gameObject);
    }

    public void UpdateDayCount()
    {
        dayText.text = string.Format("Day {0}", City.Instance.Day.ToString());
    }

    public void UpdateCityData()
    {
        cityText.text = string.Format("Jobs:{0}/{1}\n Cash: {2} + (${6})\n " +
            "Population: {3}/{4}\n Food: {5} ", City.Instance.CurrentJobs, City.Instance.JobsCeiling,
            City.Instance.Cash, (int)City.Instance.CurrentPopulation, City.Instance.PopulationCeiling, 
            (int)City.Instance.Food, City.Instance.CurrentJobs * 2);
    }
}
