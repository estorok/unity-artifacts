# Unity Artifacts

## Ray-traced Images

I implemented a software ray tracer that renders a scene 
by calculating the color and intensity for each pixel. 
A ray is cast from the viewer, through the location of the pixel 
on the screen, into the scene, until it intersects with geometry 
in the scene.

At the point at which the ray intersects the scene, my code 
calculates the light intensity at that point according to 
the presence of light sources in the scene and shadow attenuation using 
the reflective and matte (specular and diffuse) characteristics of the material.
Another ray is recursively cast with direction dependent on the type of intersection
(usually a mirror reflection; for transparent objects, the ray is transmitted 
and bent according to Snell's law, or reflected when the conditions are met for 
total internal reflection).

Relevant code snippets for calculating shadow attenuation, 
specular and diffuse reflection, and reflection/refraction directions 
are included in the `code-snippets/` directory.

![rt-1](rt-images/BallTransparent.png "ball on luminous checkerboard")

![rt-2](rt-images/CornellRay.png "cornell ray scene")

![rt-3](rt-images/Z_CornellRayRefract.png "cornell ray scene with refraction")

![rt-4](rt-images/BallRefraction.png "ball refraction")
