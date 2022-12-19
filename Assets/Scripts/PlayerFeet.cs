using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerFeet : MonoBehaviour
{
    public enum Surface
    {
        Wood,
        Stone
    }

    [System.Serializable]
    public struct SoundSurface
    {
        public Surface surface;
        public AudioClip clip;
    }

    [SerializeField] private List<SoundSurface> soundSurface;
    private Surface currentSurface = Surface.Wood;
    private AudioSource source;

    public Surface CurrentSurface { get => currentSurface; set => currentSurface = value; }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void Step()
    {
        List<SoundSurface> filtered = soundSurface.Where(x => x.surface == currentSurface).ToList();
        SoundSurface surfaceClip = filtered[Random.Range(0, filtered.Count)];
        source.clip = surfaceClip.clip;
        source.Play();
    }
}
