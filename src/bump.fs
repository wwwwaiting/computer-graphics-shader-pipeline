// Set the pixel color using Blinn-Phong shading (e.g., with constant blue and
// gray material color) with a bumpy texture.
// 
// Uniforms:
uniform mat4 view;
uniform mat4 proj;
uniform float animation_seconds;
uniform bool is_moon;
// Inputs:
//                     linearly interpolated from tessellation evaluation shader
//                     output
in vec3 sphere_fs_in;
in vec3 normal_fs_in;
in vec4 pos_fs_in; 
in vec4 view_pos_fs_in; 
// Outputs:
//               rgb color of this pixel
out vec3 color;
// expects: model, blinn_phong, bump_height, bump_position,
// improved_perlin_noise, tangent
void main()
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  float theta = (2* M_PI * animation_seconds) / 4;  
	mat4 rotate = mat4(cos(theta), 0, -sin(theta), 0,
							 0, 1, 0, 0,
							 sin(theta), 0, cos(theta), 0,
							 0, 0, 0, 1);
	
	// calculate vector v and l and normalize
	vec3 v = -((view * view_pos_fs_in).xyz);
	vec3 l = (view * vec4(1, 0.5, 0, 0) * rotate).xyz;   // rotate the light
  v = normalize(v);
  l = normalize(l);
	
  // use bump_position and tangent to calculate normal map, see assignment readme for reference
	vec3 T, B;
	tangent(sphere_fs_in, T, B);
	vec3 pT = bump_position(is_moon, sphere_fs_in + 0.0001*T) - bump_position(is_moon, sphere_fs_in);
	vec3 pB = bump_position(is_moon, sphere_fs_in + 0.0001*B) - bump_position(is_moon, sphere_fs_in);
	vec3 normal_map = normalize(cross(pT/0.0001, pB/0.0001));
		
	// get transformation matrix
  mat4 model = model(is_moon, animation_seconds);	
   
  // transformed by (M^-1)^T
  mat4 normal_matrix = transpose(inverse(view*model));
  vec4 output = normal_matrix * vec4(normal_map, 1);
  vec3 n = normalize(output.xyz);

  vec3 ka = vec3(0.05, 0.05, 0.05);
  vec3 ks = vec3(1.0, 1.0, 1.0);
  int p = 1000;
  vec3 kd;
  if(is_moon){
    kd = vec3(0.45, 0.45, 0.45);
  } else{
    kd = vec3(0.2, 0.4, 0.8);
  }
	color = blinn_phong(ka, kd, ks, p, n, v, l); 
  /////////////////////////////////////////////////////////////////////////////
}
