using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

	[SerializeField] ParticleSystem boosterParticles;
	[SerializeField] ParticleSystem lBoosterParticles;
	[SerializeField] ParticleSystem rBoosterParticles;

	Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    public void stopAllParticles() {
		boosterParticles.Stop();
		lBoosterParticles.Stop();
		rBoosterParticles.Stop();
	}
   public void ProcessThrust(bool? isArrowUp = null) {
        if (Input.GetKey(KeyCode.Space)||isArrowUp is not null && (bool)isArrowUp)
        {
			Thrust();

		}
        else
        {
            audioSource.Stop();
            boosterParticles.Stop();
        }
    }
    public void Thrust() {

		rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
		if (!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(mainEngine);
		}
		if (!boosterParticles.isPlaying)
		{
			boosterParticles.Play();
		}
	}
    public void ProcessRotation(bool? isArrowLeft = null, bool? isArrowRight = null)
    {
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)|| isArrowLeft is not null && (bool) isArrowLeft)
		{
			ApplyRotation(rotationThrust);
            if (!lBoosterParticles.isPlaying)
                lBoosterParticles.Play();
		}
		else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || isArrowRight is not null && (bool) isArrowRight)
		{
			ApplyRotation(-rotationThrust);
            if(!rBoosterParticles.isPlaying)
				rBoosterParticles.Play();
		}else{
			lBoosterParticles.Stop();
			rBoosterParticles.Stop();
		}
	}

	private void ApplyRotation(float rotationThisFrame)
	{
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
		transform.Rotate(rotationThisFrame * Time.deltaTime * Vector3.forward);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
	}
}
