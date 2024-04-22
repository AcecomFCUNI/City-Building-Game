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
    [SerializeField] private City city;
    [SerializeField] private UIController uIController;
    [SerializeField] private Building[] buildings;
    [SerializeField] private Grid grid;
    private Building selectedBuilding;
    private void Start()
    {
        Vector3 auxPos = grid.CalculateGridPosition(new Vector3(10, 0, 10));

    }
    void Update()
    {
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift) && selectedBuilding != null)
        {
            InteractWithBoard(Action.Build);
        }
        else if(Input.GetMouseButtonDown(0) && selectedBuilding != null)
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
                    if (city.Cash >= selectedBuilding.cost)
                    {
                        city.DepositCash(-selectedBuilding.cost);
                        uIController.UpdateCityData();
                        city.buildingCounts[selectedBuilding.id]++;
                        grid.AddBuilding(selectedBuilding, gridPosition);
                    }
                }
                else if(action == Action.Remove && grid.CheckForBuildingAtPosition(gridPosition) != null)
                {
                    grid.RemoveBuilding(gridPosition);
                    city.DepositCash(grid.CheckForBuildingAtPosition(gridPosition).cost * 3 / 4);
                    uIController.UpdateCityData();
                }
            }

        }
    }
    public void EnableBuilder(int building)
    {
        selectedBuilding = buildings[building];
        Debug.Log("Selected building: " + selectedBuilding.buildingName);
    }
}
