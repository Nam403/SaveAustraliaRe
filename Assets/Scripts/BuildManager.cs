using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    //public GameObject redTowerPrefab;
    //public GameObject blueTowerPrefab;

    public GameObject buildEffect;
    public GameObject sellEffect;

    private TowerPrint towerToBuild;
    private Base selectedBase;

    public BaseUI baseUI;

    public bool CanBuild() { return towerToBuild != null; }
    public bool HasMoney() { return PlayerStats.Money >= towerToBuild.cost; }

    public void SelectBase(Base baseNode)
    {
        if(selectedBase == baseNode)
        {
            DeselectBase();
            return;
        }

        selectedBase = baseNode;
        towerToBuild = null;

        baseUI.SetTarget(baseNode);
    }

    public void DeselectBase()
    {
        selectedBase = null;
        baseUI.Hide();
    }

    public void SelectTowerToBuild(TowerPrint tower)
    {
        towerToBuild = tower;
        DeselectBase();
    }

    public TowerPrint GetTowerToBuild()
    {
        return towerToBuild;
    }
}
