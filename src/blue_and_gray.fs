// Set the pixel color to blue or gray depending on is_moon.
//
// Uniforms:
uniform bool is_moon;
// Outputs:
out vec3 color;
void main()
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code:
  if (is_moon){
  	// gray moon
    color = vec3(0.5, 0.5, 0.5);
  } else{
  	// blue non-moon obj
    color = vec3(0, 0, 1);
  }
  /////////////////////////////////////////////////////////////////////////////
}
