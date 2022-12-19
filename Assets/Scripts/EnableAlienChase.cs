using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAlienChase : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ProgressManager.Instance.currentRapax.GetComponent<RapaxController>().EnableChase(true);
            Destroy(gameObject);
        }
    }
}
