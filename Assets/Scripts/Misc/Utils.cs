using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Forlorn
{

// This method fades only the alpha.
public static class Utils
{
public static IEnumerator AlphaFade(bool fadeIn, float fadeSpeed, Material material)
{
	// Debug.Log("FADING SOME ALPHA: " + fadeIn.ToString());
	// Alpha start value.
	float alpha = material.color.a;

	Color color = material.color;

	// Loop until aplha is below zero (completely invisible)
	while (fadeIn ? (alpha < 1.0f) : (alpha > 0.0f))
	{
		// Reduce alpha by fadeSpeed amount.
		alpha += fadeSpeed * Time.deltaTime * (fadeIn ? 1 : -1);

		// Create a new color using original color RGB values combined
		// with new alpha value. We have to do this because we can't 
		// change the alpha value of the original color directly.
		material.color = new Color (color.r, color.g, color.b, alpha);

		yield return null;
	}
}
}
}