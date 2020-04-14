// Create a bumpy surface by using procedural noise to generate a height (
// displacement in normal direction).
//
// Inputs:
//   is_moon  whether we're looking at the moon or centre planet
//   s  3D position of seed for noise generation
// Returns elevation adjust along normal (values between -0.1 and 0.1 are
//   reasonable.
// expects: smooth_heaviside, improved_perlin_noise
float bump_height( bool is_moon, vec3 s)
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  // use smooth_heaviside to smooth it
  if (is_moon){
	float moon = improved_perlin_noise(s * 1.5);
	return smooth_heaviside(moon, 0.5);
		
  } else {
	float earth = improved_perlin_noise(s * 5);
	return smooth_heaviside(earth, 0.5);
  }
  /////////////////////////////////////////////////////////////////////////////
}
