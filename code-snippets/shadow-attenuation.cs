// Calculate the amount that light from a given light source is attenuated 
// by at a given point, according to whether that point is in the shadow of 
// an opaque or translucent object.
private Color getShadowAttenuation(Vector3 origin, Vector3 lightPos)
{
  // start with no attenuation
  Color attenuation = Color.white;	

  // determine direction of ray from point to light source
  Vector3 directionToLight = Vector3.Normalize(lightPos - origin);
  Ray ray = new Ray(origin, directionToLight);		

  // cast a ray from current point to light source
  Intersection hit;
  bool isHit = Intersect(ray, out hit);
  while (isHit)
  {
    if (Vector3.Dot(hit.point - lightPos, directionToLight) > 0)
    {
  	  // intersection is behind light source: 
  	  // the shadow is not cast onto the point but in the other direction.
      return attenuation;     
    }
  	// else calculate the attenuation due to the material at the intersection.
    var mat = hit.material;
    attenuation *= mat.Kt;

  	// cast another ray in the same direction to determine any further attenuation 
  	// from additional translucent or opaque objects in the scene
    ray = new Ray(hit.point, directionToLight);
    isHit = Intersect(ray, out hit);
  }
  return attenuation;
}
