using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TwoPlayerRocketController : MonoBehaviour
{
	[SerializeField] float destroyDelay = 2f;
	[SerializeField] GameObject prefab;
	[SerializeField] GameObject[] codonPrefabs;
	[SerializeField] Material[] aminoAcidMaterials;

	List<string> codonTags = new List<string>();
	public string currentCodonTag = "";
	public Material currentAminoacid = null;
	int currentCodonIndex = 0;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
        for (int i = 0; i < codonPrefabs.Length; i++)
        {
            GameObject codon = codonPrefabs[i];
			codonTags.Add(codon.tag);
        }
        GameObject currentRocket = GameObject.FindWithTag("CurrentRocket");
		if (currentRocket != null)
		{
			foreach (Transform child in currentRocket.transform)
			{
				// Check if the child has the specified tag
				if (child.name == "Codon")
				{
					GameObject prefabInstance = Instantiate(codonPrefabs[currentCodonIndex], child.position, Quaternion.identity);
					prefabInstance.transform.parent = child.transform;
					currentCodonTag = codonTags[currentCodonIndex];
                }
				if (child.name == "AminoAcid")
				{
					currentAminoacid = aminoAcidMaterials[currentCodonIndex];
					child.GetComponent<Renderer>().material = currentAminoacid;
					foreach (Transform item in child.transform)
					{
						if (item.name == "AminoAcidText")
						{
							item.GetComponent<TextMeshPro>().text = currentAminoacid.name;
						}
					}
				}
			}
		}
	}

	// Update is called once per frame
	void Update()
	{

		GameObject currentRocket = GameObject.FindWithTag("CurrentRocket");

		if (currentRocket != null)
		{

			TwoPlayerCollisionHandler collisionInstance = currentRocket.GetComponent<TwoPlayerCollisionHandler>();
			if (collisionInstance.isTransitioning)
			{
				Debug.Log("Current GameObject Landed found: " + currentRocket.name);
				Destroy(collisionInstance);
				Rigidbody rb = currentRocket.GetComponent<Rigidbody>();
				if (rb != null)
				{
					// Destroy the Rigidbody component
					Destroy(rb);
					Debug.Log("Rigidbody component removed from the GameObject.");
				}
				currentRocket.transform.rotation = Quaternion.identity;
				if (currentRocket.GetComponent<PlayerTwoMover>() == null) {
					currentRocket.AddComponent<PlayerTwoMover>();
				}
				currentRocket.tag = "Friendly";
				foreach (Transform child in currentRocket.transform)
				{
					if (child.name == "AminoAcid" && currentAminoacid.name != "Parar")
					{
						//Leaves behind the aminoacid
						if (child.transform.parent != null)
						{
							child.transform.SetParent(null);
							child.transform.tag = "AminoAcid";
						}
					}
				}
				Destroy(currentRocket, destroyDelay);
				currentCodonIndex++;
				if (currentCodonIndex < codonPrefabs.Length) {
					GameObject newRocket = Instantiate(prefab, new Vector3(-15, 15, 0), Quaternion.identity);
					newRocket.tag = "CurrentRocket";
					foreach (Transform child in newRocket.transform)
					{
						// Check if the child has the specified tag
						if (child.name == "Codon")
						{
							GameObject prefabInstance = Instantiate(codonPrefabs[currentCodonIndex], child.position, Quaternion.identity);
							prefabInstance.transform.parent = child.transform;
						}
						if (child.name == "AminoAcid")
						{
							currentAminoacid = aminoAcidMaterials[currentCodonIndex];
							child.GetComponent<Renderer>().material = currentAminoacid;
							foreach (Transform item in child.transform)
							{
								if (item.name == "AminoAcidText")
								{
									item.GetComponent<TextMeshPro>().text = currentAminoacid.name;
								}
							}
						}
					}
					currentCodonTag = codonTags[currentCodonIndex];
					currentAminoacid = aminoAcidMaterials[currentCodonIndex];
				}

			}

		}

	}
}
