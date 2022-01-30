using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ghost : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle_system;
    public void disableParticles() {
        var main = particle_system.main;
        main.maxParticles = 0;
        particle_system.Clear();
        particle_system.Stop();
    }
}
