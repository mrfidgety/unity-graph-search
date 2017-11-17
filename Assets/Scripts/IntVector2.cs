using System;

[System.Serializable]
public struct IntVector2 {

	public int x, z;

	public IntVector2 (int x, int z) {
		this.x = x;
		this.z = z;
	}

	public int EuclideanDistanceTo(IntVector2 target) {
		int x_diff = x - target.x;
		int z_diff = z - target.z;

		return Convert.ToInt32(Math.Sqrt((x_diff * x_diff) + (z_diff * z_diff)));
	}

	public int ManhattanDistanceTo(IntVector2 target) {
		int x_diff = x - target.x;
		int z_diff = z - target.z;

		return Math.Abs(x_diff) + Math.Abs(z_diff);
	}

  public static bool operator== (IntVector2 a, IntVector2 b) {
    return (a.x == b.x && a.z == b.z);
  }

	public static bool operator!= (IntVector2 a, IntVector2 b) {
    return !(a.x == b.x && a.z == b.z);
  }
}
