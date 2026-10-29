using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReactivator : MonoBehaviour
{
    [SerializeField] private GameObject gridObject;

    public void Reactivate(string name)
    {
        if (gridObject != null)
        {
            foreach (Transform child in gridObject.transform)
            {
                if (child.name.Contains("Location")) 
                {
                    Transform target = child.Find(name);
                    if (target != null)
                    {
                        target.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    }
                }
            }
        }

    }

}
