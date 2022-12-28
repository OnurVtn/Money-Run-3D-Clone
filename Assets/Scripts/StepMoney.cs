using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepMoney : MonoBehaviour
{
    [SerializeField] private float stepMoneyDropSpeed, stepMoneyDropDelay;
    private bool isStepMoneyDropping = false;

    void Start()
    {
        StartCoroutine(DelayStepMoneyDropping());
    }

    void Update()
    {
        DropStepMoney();
    }

    private void DropStepMoney()
    {
        if (isStepMoneyDropping == true)
        {
            transform.position -= transform.up * Time.deltaTime * stepMoneyDropSpeed;

            Destroy(this.gameObject, 5f);
        }       
    }

    private IEnumerator DelayStepMoneyDropping()
    {
        yield return new WaitForSeconds(stepMoneyDropDelay);

        isStepMoneyDropping = true;
    }
}
