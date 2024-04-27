using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Action
{
    Build,
    Remove
}
public class BuildingHandler : MonoBehaviour
{
    [SerializeField] private Building[] buildings;
    [SerializeField] private Grid grid;
    private Building selectedBuilding;

    public Building SelectedBuilding {get => selectedBuilding; private set => selectedBuilding = value;}
    private void Start()
    {
        Vector3 auxPos = grid.CalculateGridPosition(new Vector3(10, 0, 10));
        grid.AddBuilding(buildings[1], new Vector3(5, 0.5f, 5));
        grid.AddBuilding(buildings[2], new Vector3(3, 0.5f, 5));
        grid.AddBuilding(buildings[3], new Vector3(5, 0.5f, 3));
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift) && SelectedBuilding != null)
        {
            InteractWithBoard(Action.Build);
        }
        else if(Input.GetMouseButtonDown(0) && SelectedBuilding != null)
        {
            InteractWithBoard(Action.Build);
        }

        if (Input.GetMouseButtonDown(1))
        {
            InteractWithBoard(Action.Remove);
        }
    }
    private void InteractWithBoard(Action action)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            Vector3 gridPosition = grid.CalculateGridPosition(hit.point);
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                if (action == Action.Build && grid.CheckForBuildingAtPosition(gridPosition) == null)
                {
                    if (City.Instance.Cash >= SelectedBuilding.cost)
                    {
                        City.Instance.DepositCash(-SelectedBuilding.cost);
                        UIController.Instance.UpdateCityData();
                        City.Instance.buildingCounts[SelectedBuilding.id]++;
                        grid.AddBuilding(SelectedBuilding, gridPosition);
                    }
                    else SelectedBuilding = null;
                }
                else if(action == Action.Remove && grid.CheckForBuildingAtPosition(gridPosition) != null)
                {
                    grid.RemoveBuilding(gridPosition);
                    City.Instance.DepositCash(grid.CheckForBuildingAtPosition(gridPosition).cost * 3 / 4);
                    UIController.Instance.UpdateCityData();
                }
            }

        }
    }
    public void EnableBuilder(int building)
    {
        SelectedBuilding = buildings[building];
        Debug.Log("Selected building: " + SelectedBuilding.buildingName);
    }
}
