using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomParticleColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Gradient g;
		// GradientColorKey[] gck;
		// GradientAlphaKey[] gak;
		// g = new Gradient();
  //       gck = new GradientColorKey[2];
  //       gck[0].color = Color.red;
  //       gck[0].time = 0.0F;
  //       gck[1].color = Color.blue;
  //       gck[1].time = 1.0F;
  //       gak = new GradientAlphaKey[2];
  //       gak[0].alpha = 1.0F;
  //       gak[0].time = 0.0F;
  //       gak[1].alpha = 1.0F;
  //       gak[1].time = 1.0F;
  //       g.SetKeys(gck, gak);

		ParticleSystem particle = GetComponent<ParticleSystem>();
		particle.startColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
	}

	// Update is called once per frame
	void Update () {

	}
}
