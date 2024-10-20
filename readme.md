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

In addition I've implemented a new triangle:AABB solution which improves on the performance of the Schwarz-Seidel test.  It is conceptually similar, in that it first tests the edges of the triangle against the AABB and then checks for the cases where the box intersects the face of the triangle without touching any edges.

Where the new solution differs is that it performs these constitute tests directly against the AABB in 3D space, rather than projecting the triangle onto the three cardial planes and using triangle-rectangle tests.

The Schwarz-Seidel test follows a similar approach to the Separating Axes Theorem, which means that it can only exit early in situations where the shapes are disjoint and finding intersecting shapes requires the whole function to be completed.  The new approach inverts this logic; after an initial broad-phase rejection using bounding boxes, it can return as soon as one of the possible intersection cases is met.

This means that the new approach is significantly more efficient if the domain is mostly made up of intersecting shapes.  This is often the case when performing a series of hierarchial tests such as with the generation of Sparse Voxel Octrees from triangle meshes.

If the domain contains a significant number of disjoint shape queries then performance of the two solutions is very similar, since the early bounding box check filters out a large proportion of the cases.
