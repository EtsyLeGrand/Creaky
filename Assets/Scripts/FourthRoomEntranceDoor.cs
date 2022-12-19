using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthRoomEntranceDoor : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private GameObject rapaxToSpawn;
    [SerializeField] private float timeBeforeEvent;
    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBeforeEvent)
        {
            source.Stop();
            GetComponent<Animator>().enabled = true;
            GetComponent<Animator>().Play("Door Breakdown");
            GameObject rapax = Instantiate(rapaxToSpawn, new Vector3(-25f, 1.4f, 2.16f), Quaternion.Euler(new Vector3(0, 180, 0)));
            rapax.GetComponent<RapaxController>().Scream();
            rapax.GetComponent<RapaxController>().EnableChase(true);
            Destroy(this);
        }

    }
}
