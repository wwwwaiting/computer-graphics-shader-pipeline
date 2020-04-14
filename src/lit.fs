// Add (hard code) an orbiting (point or directional) light to the scene. Light
// the scene using the Blinn-Phong Lighting Model.
//
// Uniforms:
uniform mat4 view;
uniform mat4 proj;
uniform float animation_seconds;
uniform bool is_moon;
// Inputs:
in vec3 sphere_fs_in;
in vec3 normal_fs_in;
in vec4 pos_fs_in; 
in vec4 view_pos_fs_in; 
// Outputs:
out vec3 color;
// expects: PI, blinn_phong
void main()
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  float theta = (2* M_PI * animation_seconds) / 4;  
  mat4 rotate_matrix = mat4(cos(theta), 0, -sin(theta), 0,
							 0, 1, 0, 0,
							 sin(theta), 0, cos(theta), 0,
							 0, 0, 0, 1);
	
  // calculate vector v and l
  vec3 v = -((view * view_pos_fs_in).xyz);
  vec3 l = (view * vec4(1, 0.5, 0, 0) * rotate_matrix).xyz;
  vec3 n = normalize(normal_fs_in);
  v = normalize(v);
  l = normalize(l);
  
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
