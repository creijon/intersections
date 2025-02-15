## Intersections

This is a small Unity project that implements a set of 2D and 3D interesection tests from simple point-in-box to more complex triangle-AABB tests.  The more later tests are built up by reusing the solutions for the earlier tests where possible, I think this helps to make them more understandable.

2D:

- point : rectangle
- rectangle : rectangle
- edge : rectangle
- point : triangle
- triangle : rectangle

3D:

- point : aabb
- aabb : aabb
- ray : aabb
- edge : aabb
- ray : triangle
- edge : triangle
- triangle : aabb (see below)
- plane : aabb
- edge : plane

The focus of these implementations has been on algorithmic optimisations and making them as clear and readable as I can, rather than tight code optimisations.  It is certainly possible to improve the performance of the tests by writing them in a language which provides more control of code generation, batching multiple tests together or exploiting redundancy in successive hierarchical tests.

I've included implementations of some of the standard algorithms, such as the Schwarz-Seidel triangle:AABB test and the Moller-Trumbore ray:triangle test.

### Triangle:AABB Intersection Test

In addition I've implemented a new triangle:AABB solution which takes a different approach to the Schwarz-Seidel test.  It has four stages:

1. Check for intersection between the AABB and the bounds of the triangle.  Exit early if disjoint.
2. Test each of the three triangle edges against the AABB.  Exit early with an intersection.
3. Check for intersection between the plane of triangle and the AABB.  Exit if disjoint.
4. Test each of the four internal diagonal axes of the AABB against the triangle, for the cases where the box intersects the face of the triangle without touching any of its edges.

The Schwarz-Seidel algorithm can only exit early in situations where the shapes are disjoint, so finding intersecting shapes requires all the individual tests to be checked.  The new approach inverts this logic; after an initial broad-phase rejection using bounding boxes, it can return as soon as one of the potential intersection cases is met.

This means that it is significantly more efficient if the domain is mostly made up of intersecting shapes, which is often the case when performing a series of hierarchial tests such as with the generation of Sparse Voxel Octrees from triangle meshes.

If the domain is predominantly disjoint shape queries then performance of the two solutions is very similar, since the bounding box check is very effective in filtering these out early.

### Unity Burst and Mathematics

I've ported the intersection functions over to use Unity.Mathematics, which has cleaned up a lot of the code and made it far more similar to GLSL or HLSL.  However, the performance when of Unity.Mathematics when not being complied with Unity Burst is significantly worse than the equivalent functions written with the traditional Vector3 classes and Mathf functions.  So I think it's worth keeping both implementations for the cases where you can't use Burst.
