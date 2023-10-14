using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCast : MonoBehaviour
{
    [SerializeField] private GameObject target;
    void Update()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                Instantiate(target, hit.transform);
        }
    }
}
