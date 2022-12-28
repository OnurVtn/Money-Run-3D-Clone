using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepMoneySpawner : MonoBehaviour
{  
    [SerializeField] private Transform stackerTransform;
    [SerializeField] private GameObject stepMoneyPrefab;
    [SerializeField] private float stepMoneySpawnDelay;

    void Start()
    {
        StartCoroutine(SpawnStepMoney());
    }

    private IEnumerator SpawnStepMoney()
    {
        while (true)
        {
            if (Input.GetMouseButton(0))
            {               
                if (stackerTransform.childCount >= 1)
                {
                    Destroy(stackerTransform.GetChild(stackerTransform.childCount - 1).gameObject);
                    Instantiate(stepMoneyPrefab, transform.position, Quaternion.identity);
                }              
            }
          
            yield return new WaitForSeconds(stepMoneySpawnDelay);
        }      
    }
}
