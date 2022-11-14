// Calculate the intensity of light to the viewer ray 
// directly reflected by the material at the hit point
// without including the reflected or refracted components.
private Color getDirectComponent(Ray ray, 
                                 Point hit, // point of intersection
                                 Color ambientColor, // global illumination
                                 double ke, // material emissive constant
                                 double kd, // material diffuse constant
                                 double ks, // material specular constant
                                 double shininess) 
{
  Color directComponent = ambientColor + ke;
  // vector from point to viewer
  Vector3 V = Vector3.Normalize(ray.origin - hit.point);
  // surface normal
  Vector3 N = Vector3.Normalize(hit.normal);
  // a PointLightObject is a point light source in the scene
  foreach (PointLightObject light in pointLightObjects)
  {
    // vector from point to light source
    Vector3 L = Vector3.Normalize(light.LightPos - hit.point);
    // vector that points halfway between light source and viewer vectors
    Vector3 H = Vector3.Normalize(L + V);
    float r = Vector3.Distance(light.LightPos, hit.point);
    float distanceAttenutation = 1.0f / (1.0f + r * r);
    Color shadowAttenuation = getShadowAttenuation(hit.point, light.LightPos);
    directComponent += 
      // diffuse component
      (kd * Mathf.Max(Vector3.Dot(N, L), 0) +
      // specular component (using blinn-phong model)
      // https://en.wikipedia.org/wiki/Blinn–Phong_reflection_model
      ks * Mathf.Pow(Mathf.Max(Vector3.Dot(N, H), 0), shininess))
      // other modifiers
      * distanceAttenutation * shadowAttenuation 
      * light.Color * light.Intensity;
  }
  return directComponent;
}
