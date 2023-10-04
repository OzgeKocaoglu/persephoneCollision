# persephoneCollision
Simple, collision system for unity. It is simple, fast, easy to use. For custom purposes. 

<img src="https://github.com/OzgeKocaoglu/persephoneCollision/blob/main/Outsource/repository.png" alt="logo" width="600" height="auto" />


 <p>
    Simple, collision system for unity. It is simple, fast, easy to use. For custom purposes. 
  </p>

  <h4>
    <a href="https://github.com/OzgeKocaoglu/persephoneCollision/">Documentation</a>
  <span> · </span>
    <a href="https://github.com/OzgeKocaoglu/persephoneCollision/issues">Report Bug</a>
  <span> · </span>
    <a href="https://github.com/OzgeKocaoglu/persephoneCollision/issues">Request Feature</a>
  </h4>
</div>

<br />

# How to use?

1. Add "Collision Box" to your character. You can add as child.
2. Add this script to your class:
```
public CollisionBox collisionBox;
  
  // -----------------------------------------------------------------------
  public void OnEnable() 
  {
    collisionBox.collisionStart += collisionStart;
    collisionBox.collisionEnd += collisionEnd;
  }
  
  // -----------------------------------------------------------------------
  public void OnDisable() 
  {
    collisionBox.collisionStart -= collisionStart;
    collisionBox.collisionEnd -= collisionEnd;
  }
  
  // -----------------------------------------------------------------------
  public void collisionStart(CollisionBox box) 
  {
    Debug.Log("I'm collided with : " + box.name);
  }
  
  // -----------------------------------------------------------------------
  public void collisionEnd(CollisionBox box) 
  {
    Debug.Log("I'm not collided with : " + box.name + " anymore");
  }
```
3. And ta da! You are ready to go!

# What will be in the future?
I'm gonna add new collision types like sphere etc.
I will add editor handles to edit more efficient.
