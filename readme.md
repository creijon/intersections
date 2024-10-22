## Intersections

This is a small Unity project that implements a set of 2D and 3D interesection tests from simple point-in-box to more complex triangle-AABB tests.  The more complex tests are built up by reusing the solutions for the simple tests where possible.

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
- triangle : aabb
- plane : aabb
- edge : plane

The focus of these implementations has been on algorithmic optimisations and making them as clear and readable as I can, rather than tight code optimisations.  It is certainly possible to improve the performance of the tests by using a linear algebra library that leverages native SIMD operations, writing them in a language which provides more control of code generation, batching multiple tests together or exploiting redundancy in successive hierarchical tests.

I've included implementations of some of the standard algorithms, such as the Schwarz-Seidel triangle:AABB test and the Moller-Trumbore ray:triangle test.

### Triangle:AABB Intersection Test

In addition I've implemented a new triangle:AABB solution which takes a different approach to the Schwarz-Seidel test.  It is broken into three stages:

1. Check for intersection between the AABB and the bounds of the triangle.  Exit early if disjoint.
2. Test each of the three triangle edges against the AABB.  Exit early with an intersection.
3. Test each of the four internal diagonal axes of the AABB against the triangle, for the cases where the box intersects the face of the triangle without touching any of its edges.

The Schwarz-Seidel algorithm can only exit early in situations where the shapes are disjoint, so finding intersecting shapes requires all the individual tests to be checked.  The new approach inverts this logic; after an initial broad-phase rejection using bounding boxes, it can return as soon as one of the potential intersection cases is met.

This means that it is significantly more efficient if the domain is mostly made up of intersecting shapes.  This is often the case when performing a series of hierarchial tests such as with the generation of Sparse Voxel Octrees from triangle meshes.

If the domain contains a significant number of disjoint shape queries then performance of the two solutions is very similar, since the early bounding box check filters out a large proportion of the cases.
