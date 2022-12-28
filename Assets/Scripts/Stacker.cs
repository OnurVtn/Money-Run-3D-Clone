using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacker : MonoBehaviour
{
    [SerializeField] private float moneyXDistance, moneyYDistance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            StackMoney(other.transform);
        }
    }

    private void StackMoney(Transform collectableParentTransform)
    {
        while (collectableParentTransform.childCount > 0)
        {
            collectableParentTransform.GetChild(0).SetParent(transform);
        }

        Destroy(collectableParentTransform.gameObject);

        foreach (Transform child in transform)
        {
            child.position = transform.position;
            child.rotation = Quaternion.Euler(child.rotation.x, -90f, child.rotation.z);
        }

        SortMoney();
    }

    private void SortMoney()
    {
        float addXPosition = 0;
        float addYPosition = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (i % 5 == 0 && i != 0)
            {
                addXPosition = 0;
                addYPosition += moneyYDistance;
            }

            transform.GetChild(i).position = new Vector3(transform.position.x + addXPosition, transform.position.y + addYPosition, transform.position.z);
            addXPosition += moneyXDistance;           
        }
    }
}
