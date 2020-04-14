// Generate a pseudorandom unit 3D vector
// 
// Inputs:
//   seed  3D seed
// Returns psuedorandom, unit 3D vector drawn from uniform distribution over
// the unit sphere (assuming random2 is uniform over [0,1]²).
//
// expects: random2.glsl, PI.glsl
vec3 random_direction( vec3 seed)
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  vec2 factor = random2(seed);
  float theta = 2 * M_PI * factor.x;
  float phi = M_PI * factor.y;

  float x = -cos(theta) * sin(phi);
  float y = -sin(theta) * sin(phi);
  float z = -cos(phi);
  
  return normalize(vec3(x, y, z));
  /////////////////////////////////////////////////////////////////////////////
}
