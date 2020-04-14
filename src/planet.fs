// Generate a procedural planet and orbiting moon. Use layers of (improved)
// Perlin noise to generate planetary features such as vegetation, gaseous
// clouds, mountains, valleys, ice caps, rivers, oceans. Don't forget about the
// moon. Use `animation_seconds` in your noise input to create (periodic)
// temporal effects.
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
// expects: model, blinn_phong, bump_height, bump_position,
// improved_perlin_noise, tangent
void main()
{
  float theta = (2* M_PI * animation_seconds) / 4;  
  mat4 rotate = mat4(cos(theta), 0, -sin(theta), 0,
					0, 1, 0, 0,
					sin(theta), 0, cos(theta), 0,
					0, 0, 0, 1);
	
  // calculate vector v and l and normalize
  vec3 v = -((view * view_pos_fs_in).xyz);
  vec3 l = (view * vec4(1, 0.5, 0, 0) * rotate).xyz;
	
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
  vec4 output = normal_matrix * vec4(normal_map, 1.0);
  vec3 n = normalize(output.xyz);

  vec3 ka, ks, kd;
  int p;
  if (is_moon) {
  	ka = vec3(0.05, 0.05, 0.05);
	kd = vec3(0.45, 0.45, 0.45);
  	ks = vec3(1.0, 1.0, 1.0);
	p = 1000;
  } else {
  	float bump = bump_height(is_moon, sphere_fs_in);
    if (animation_seconds <= 1.5){
	  ka = vec3(0.05, 0.05, 0.05);
	  kd = vec3(0.2, 0.4, 0.8);
	  ks = vec3(1.0, 1.0, 1.0);
	  p = 1000;
	} else if (animation_seconds <= 2.75){
	  // add land for seconds 1.5-2.75	
	  if (bump >= -0.35){
		ka = vec3(0.30, 0.20, 0.10);
		kd= vec3(0.25, 0.25, 0.20);
		ks =vec3(0.80, 0.80, 0.80);
		p = 1000;
	  }else {
		ka = vec3(0.05, 0.05, 0.05);
		kd = vec3(0.2, 0.4, 0.8);
		ks = vec3(1.0, 1.0, 1.0);
		p = 1000;
	  }
	}else{
	  // add land and green for seconds 3.5-	
	  if (bump >= -0.35){
		// land 
		ka = vec3(0.30, 0.20, 0.10);
		kd= vec3(0.25, 0.25, 0.20);
		ks =vec3(0.80, 0.80, 0.80);
		p = 1000;
	  }else if (bump >= -0.45){
		// green 
		ka = vec3(0.301961,0.686275,0.290196);
		kd = vec3(0.301961,0.686275,0.290196);
		ks = vec3(0.2,0.2,0.2);
		p = 500;
	  }	else {
		ka = vec3(0.05, 0.05, 0.05);
		kd = vec3(0.2, 0.4, 0.8);
		ks = vec3(1.0, 1.0, 1.0);
		p = 1000;
	  }
	} 	
  }
  color = blinn_phong(ka, kd, ks, p, n, v, l); 
}
