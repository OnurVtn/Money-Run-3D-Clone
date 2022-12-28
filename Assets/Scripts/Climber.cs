using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour
{
    private static Climber instance;

    public static Climber Instance => instance ?? (instance = FindObjectOfType<Climber>());

    public bool isClimbing;

    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float upMovementSpeed;

    [SerializeField] private Transform stackerTransform;
    [SerializeField] private GameObject stepMoneyPrefab;
    [SerializeField] private Transform stepMoneySpawnerTransform;
    [SerializeField] private float stepMoneySpawnDelay;

    void Start()
    {
        StartCoroutine(SpawnStepMoney());
    }

    void Update()
    {
        VerticalMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ClimbWall"))
        {
            isClimbing = true;
        }
    }

    private void VerticalMovement()
    {
        if (isClimbing == true)
        {
            if (stackerTransform.childCount >= 1)
            {
                playerTransform.position += transform.up * Time.deltaTime * upMovementSpeed;
            }
            else
            {
                playerRigidbody.isKinematic = false;
                playerRigidbody.useGravity = true;            
            }
        }      
    }

    private IEnumerator SpawnStepMoney()
    {
        while (true)
        {
            if (isClimbing == true)
            {
                if (stackerTransform.childCount >= 1)
                {
                    Destroy(stackerTransform.GetChild(stackerTransform.childCount - 1).gameObject);
                    Instantiate(stepMoneyPrefab, stepMoneySpawnerTransform.position, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(stepMoneySpawnDelay);
        }       
    }
}
