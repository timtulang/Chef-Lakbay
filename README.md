Step by step in applying the grabbing mechanics in an item:

1. Add the appropriate type of Collider to the item(e.g. Circle Collider for circular objects and box Collider for rectangular objects)
2. Add the Rigidbody2D component to the item. Adjust the following parameters:

   a. Body Type -> Dynamic

   ![Body Type -> Dynamic](image.png)

   b. Gravity Scale = 0

   ![Gravity Scale = 0](image-1.png)

   c. Constraints -> Freeze all axis

   ![Constraints -> Freeze all axis](image-2.png)

3. Set the item tag to "Item"

   ![Item Tag](image-4.png)

4. Set the layer to "Items"

   ![alt text](image-5.png)

NOTE:

- If the prefab appears behind the stage, adjust the sorting layer accordingly:

  ![Sorting Layer -> Stage: 1](image-6.png)

- DO NOT FORGET TO SAVE THE OBJECT AS A PREFAB/OVERRIDE THE CHANGES YOU MAKE.

- CHECK CONSTRAINTS X, Y, Z

- AS OF: 11/22/2024: 13th UMAK ENDED
- RESULT: LOSE
