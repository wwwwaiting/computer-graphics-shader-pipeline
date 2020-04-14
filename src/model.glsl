// Construct the model transformation matrix. The moon should orbit around the
// origin. The other object should stay still.
//
// Inputs:
//   is_moon  whether we're considering the moon
//   time  seconds on animation clock
// Returns affine model transformation as 4x4 matrix
//
// expects: identity, rotate_about_y, translate, PI
mat4 model(bool is_moon, float time)
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
	if (is_moon){
		float theta =  (2* M_PI * time) / 4;    // one revolution per 4 seconds
	  mat4 shift = translate(vec3(2, 0, 0));  // shift away by 2 units
  	mat4 model =  rotate_about_y(theta) * shift * uniform_scale(0.3);  //shrink by 30%
  	return model;
  }
  return identity();
  /////////////////////////////////////////////////////////////////////////////
}
