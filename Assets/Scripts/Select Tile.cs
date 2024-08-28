using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectTile : MonoBehaviour
{
    [SerializeField] private GameObject selector;
    [SerializeField] private GameObject Gem;
    [SerializeField] private GameObject[] Towers;
    [SerializeField] private int[] price;
    private int selectedTower = 0;
    private GameObject lastSelectedTile;
    private GameObject selectedTile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        //comment out later
        //GlobalReferencesCamera.Price.text = GlobalReferencesCamera.Eyes.ToString();


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            lastSelectedTile = hit.transform.gameObject;
            Vector3 t = lastSelectedTile.transform.position;
            selector.transform.position = new Vector3(t.x, selector.transform.position.y, t.z);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Gem.transform.position = selector.transform.position;
            selectedTile = lastSelectedTile;
            //print(selectedTile);

        }

        //Implemented towers = 4 [0,3]
        if (Input.GetKeyDown(KeyCode.W))
        {
            selectedTower++;
            if (selectedTower >= 3)
                selectedTower = 3;
            GlobalReferencesCamera.switchSelectedTower(selectedTower, price[selectedTower]);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            selectedTower--;
            if (selectedTower <= 0)
                selectedTower = 0;
            GlobalReferencesCamera.switchSelectedTower(selectedTower, price[selectedTower]);
        }

        if (Input.GetKeyDown(KeyCode.Space))
            PlaceTower();
    }

    void PlaceTower()
    {
        //print("PLACING");
        HasTower tile = selectedTile.GetComponent<HasTower>();
        if (tile.towerPresent)
        {
            // Do Nothing
        }
        else
        {
            // Remove the eye cost
            if ((GlobalReferencesCamera.Eyes + 2) > price[selectedTower])
            {
                tile.towerPresent = true;
                GlobalReferencesCamera.UpdateEyes(-price[selectedTower]);
                tile.tower = Instantiate(Towers[selectedTower], tile.transform.position + Vector3.up*0.2f, Quaternion.identity, GlobalReferencesCamera.Misc);
            }
            // Place the tower using selectedTower
        }
    }
}
