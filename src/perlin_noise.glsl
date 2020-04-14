// Given a 3d position as a seed, compute a smooth procedural noise
// value: "Perlin Noise", also known as "Gradient noise".
//
// Inputs:
//   st  3D seed
// Returns a smooth value between (-1,1)
//
// expects: random_direction, smooth_step
float perlin_noise( vec3 st) 
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code
  
  vec3 flr = floor(st);
  vec3 v1 = flr;
  vec3 v2 = flr + vec3(0, 0, 1);
  vec3 v3 = flr + vec3(0, 1, 0);
  vec3 v4 = flr + vec3(0, 1, 1);
  vec3 v5 = flr + vec3(1, 0, 0);
  vec3 v6 = flr + vec3(1, 0, 1);
  vec3 v7 = flr + vec3(1, 1, 0);
  vec3 v8 = flr + vec3(1, 1, 1);

  vec3 g1, g2, g3, g4, g5, g6, g7, g8;
  g1 = random_direction(v1);
  g2 = random_direction(v2);
  g3 = random_direction(v3);
  g4 = random_direction(v4);
  g5 = random_direction(v5);
  g6 = random_direction(v6);
  g7 = random_direction(v7);
  g8 = random_direction(v8);

  vec3 frac = fract(st);
  float d1 = dot(g1, frac);
  float d2 = dot(g2, (frac - vec3(0, 0, 1)));
  float d3 = dot(g3, (frac - vec3(0, 1, 0)));
  float d4 = dot(g4, (frac - vec3(0, 1, 1)));
  float d5 = dot(g5, (frac - vec3(1, 0, 0)));
  float d6 = dot(g6, (frac - vec3(1, 0, 1)));
  float d7 = dot(g7, (frac - vec3(1, 1, 0)));
  float d8 = dot(g8, (frac - vec3(1, 1, 1)));

  // see reference: https://en.wikipedia.org/wiki/Trilinear_interpolation
  vec3 s = smooth_step(frac);
  float c15 = mix(d1, d5, s.x);
  float c26 = mix(d2, d6, s.x);
  float c37 = mix(d3, d7, s.x);
  float c48 = mix(d4, d8, s.x);
  float c1 = mix(c15, c37, s.y);
  float c2 = mix(c26, c48, s.y);
  float c = mix(c1, c2, s.z);
  
  // bound the output range to [-1,1]
  return -1.0+2.0*c;
  /////////////////////////////////////////////////////////////////////////////
}

