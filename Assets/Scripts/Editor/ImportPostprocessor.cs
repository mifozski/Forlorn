using UnityEngine;
using UnityEditor;

public class ImportPostprocessor : AssetPostprocessor
{
	void OnPostprocessModel(GameObject g)
	{
		Apply(g.transform);
	}

	void Apply(Transform t)
	{
		// Debug.Log($"Checking {t.name}");
		var index = t.name.IndexOf("collider");
		if (index >= 0)
		{
			if (t.name.ToLower().Contains("collider_box"))
			{
				// Debug.Log($"Adding box collider to {t.gameObject.name}");
				BoxCollider boxCollider = t.gameObject.AddComponent<BoxCollider>();

				Bounds meshBounds = t.GetComponent<MeshRenderer>().bounds;
				meshBounds.size = Vector3.Scale(meshBounds.size, t.localScale);
				meshBounds.center += t.localPosition;

				boxCollider.size = meshBounds.size;
				// boxCollider.center = meshBounds.center;
			}
			else
			{
				// Debug.Log($"Adding mesh collider to {t.gameObject.name}");
				t.gameObject.AddComponent<MeshCollider>();
			}

			if (index == 0)
			{
				GameObject.DestroyImmediate(t.gameObject.GetComponent<MeshRenderer>());
			}
		}

		// Recurse
		foreach (Transform child in t)
			Apply(child);
	}
}
