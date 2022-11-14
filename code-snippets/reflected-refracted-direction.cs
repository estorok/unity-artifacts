// Calculate direction of ray when refracted through the material at 
// the given intersection point.
private Vector3 getRefractedDirection(Ray ray,   // from viewer to intersection
                                      Point hit, // point of intersection
                                      double indexOfRefraction) 
{
  // vector from intersection to viewer
  Vector3 V = Vector3.Normalize(ray.origin - hit.point);
  // surface normal
  var N = Vector3.Normalize(hit.normal);
  bool isEntering = Vector3.Dot(ray.origin - hit.point, N) > 0f;
  if (!isEntering)
  {
      N *= -1;
  }

  float eta_i = 0;
  float eta_t = 0;
  if (isEntering)
  {
      eta_i = 1.0003f;  // index of refraction of air
      eta_t = indexOfRefraction;
  }
  else
  {
      eta_i = indexOfRefraction;
      eta_t = 1.0003f;
  }

  // reference: Marschner and Shirley, Fundamentals of Computer Graphics
  // 13.1. Transparency and Refraction
  Vector3 D = -1 * V;   // ray direction vector
  float d_dot_n = Vector3.Dot(D, N);
  float discriminant = 1.0f - (eta_i * eta_i * (1 - d_dot_n * d_dot_n)) / (eta_t * eta_t);
  bool isTotalInternalReflection = discriminant <= 0f;
  if (Vector3.Magnitude(new Vector3(kt.r, kt.g, kt.b)) > 0f && !isTotalInternalReflection)
  {
      Vector3 refractionDirection = ((eta_i * (D - N * d_dot_n)) 
                                    / eta_t - N * Mathf.Sqrt(discriminant));
      return refractionDirection;
  }
  return null;  // no refraction due to total internal reflection or otherwise
}

// Calculate direction of ray according to mirror reflection 
// given surface normal and incoming ray direction.
private Vector3 getReflectedDirection(Vector3 rayDirection, // incoming ray direction
                                      Vector3 N)            // surface normal
{
  Vector3 reflectionDirection = 2 * Vector3.Dot(rayDirection, N) * N - rayDirection;
  return reflectionDirection;
}
