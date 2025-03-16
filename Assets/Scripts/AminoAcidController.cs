using UnityEngine;

public class AminoAcidController : MonoBehaviour
{
    int aminoAcidCount = 0;
	int cylinderCount = 0;
	bool stop = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		// Find all GameObjects with the specified tag
		GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("AminoAcid");

		// Count the number of GameObjects with the specified tag
		aminoAcidCount = taggedObjects.Length;
        if(aminoAcidCount >= 2 && cylinderCount + 1 < aminoAcidCount && !stop)
		{
			int firstAminoacidIndex = aminoAcidCount-2;
			int secondAminoacidIndex = aminoAcidCount-1;
			// Create a cylinder
			GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			cylinder.tag = "AminoAcidConnector";
			// Step 2: Access the existing CapsuleCollider component
			CapsuleCollider capsuleCollider = cylinder.GetComponent<CapsuleCollider>();
			// Step 3: Disable the CapsuleCollider
			capsuleCollider.enabled = false;
			cylinderCount++;
			// Position the cylinder between the two prefabs
			cylinder.transform.position = (taggedObjects[firstAminoacidIndex].transform.position + taggedObjects[secondAminoacidIndex].transform.position) / 2;

			// Adjust the cylinder's rotation
			cylinder.transform.LookAt(taggedObjects[firstAminoacidIndex].transform);
			cylinder.transform.Rotate(90, 0, 0);

			// Adjust the cylinder's scale to fit between the prefabs
			float distance = Vector3.Distance(taggedObjects[firstAminoacidIndex].transform.position, taggedObjects[secondAminoacidIndex].transform.position);
			cylinder.transform.localScale = new Vector3(0.1f, distance / 2, 0.1f);
		}
	}
}
