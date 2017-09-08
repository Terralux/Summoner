using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplexNoiseTest {
	
	public static Vector3 offset;
	public static Vector3 rotation;

	public static float morphSpeed;

	[Range (0f, 1f)]
	public static float strength = 1f;

	public static bool damping;

	public static float frequency = 2f;

	[Range (1, 8)]
	public static int octaves = 1;

	[Range (1f, 4f)]
	public static float lacunarity = 2f;

	[Range (0f, 1f)]
	public static float persistence = 0.443f;

	[Range (1, 3)]
	public static int dimensions = 3;

	public static NoiseMethodType type;

	private static float morphOffset;

	public static float GetSurfaceChunkPosition (int x, int y, int z) {
		Quaternion q = Quaternion.Euler (rotation);
		Quaternion qInv = Quaternion.Inverse (q);

		NoiseMethod method = Noise.methods [(int)type] [dimensions - 1];

		float amplitude = damping ? strength / frequency : strength;

		morphOffset += Time.deltaTime * morphSpeed;

		if (morphOffset > 256f) {
			morphOffset -= 256f;
		}

		Vector3 position = new Vector3 (x, y, z);
		Vector3 point = q * new Vector3 (position.z, position.y, position.x + morphOffset) + offset;

		NoiseSample sampleX = Noise.Sum (method, point, frequency, octaves, lacunarity, persistence);
		sampleX *= amplitude;
		sampleX.derivative = qInv * sampleX.derivative;
		point = q * new Vector3 (position.x + 100f, position.z, position.y + morphOffset) + offset;

		/*
		NoiseSample sampleY = Noise.Sum (method, point, frequency, octaves, lacunarity, persistence);
		sampleY *= amplitude;
		sampleY.derivative = qInv * sampleY.derivative;
		*/

		point = q * new Vector3 (position.y, position.x + 100f, position.z + morphOffset) + offset;

		NoiseSample sampleZ = Noise.Sum (method, point, frequency, octaves, lacunarity, persistence);
		sampleZ *= amplitude;
		sampleZ.derivative = qInv * sampleZ.derivative;

		/*
		Vector3 curl;
		curl.x = sampleZ.derivative.x - sampleY.derivative.y;
		curl.y = sampleX.derivative.x - sampleZ.derivative.y + (1f / (1f + position.y));
		curl.z = sampleY.derivative.x - sampleX.derivative.y;
		*/

		return sampleX.derivative.x - sampleZ.derivative.y + (1f / (1f + position.y));
	}
}