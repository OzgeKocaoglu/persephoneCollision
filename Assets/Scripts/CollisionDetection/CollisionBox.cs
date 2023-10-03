/* --------------------------------------------------------------------------
  Title       :  CollisionBox.cs
  Date        :  30 Sat 2023
  Programmer  :  Ozge Kocaoglu
  Package     :  Version 1.0
  Copyright   :  MIT License
 -------------------------------------------------------------------------- */
/* --------------------------------------------------------------------------
 Copyright (c) 2023 Ozge Kocaoglu

 THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 SOFTWARE.
-------------------------------------------------------------------------- */


using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


// -------------------------------------------------------------------------
namespace Persephone {
  // -------------------------------------------------------------------------
  public class CollisionBox : Box
  {
    public delegate void CollisionBoxHandler(CollisionBox collidedBox);
    public CollisionBoxHandler collisionStart;
    public CollisionBoxHandler collisionEnd;
    static List<CollisionBox> boxesOnScene = new List<CollisionBox>();
    List<CollisionBox> collidedBoxes = new List<CollisionBox>();

    // -----------------------------------------------------------------------
    protected override void onEnable() 
    {
      if (!boxesOnScene.Contains(this)) {
       boxesOnScene.Add(this); 
      }
      base.onEnable();
    }

    // -----------------------------------------------------------------------
    protected override void onDisable()
    {
      if (boxesOnScene.Contains(this)) {
        boxesOnScene.Remove(this);
      }
      base.onDisable();
    }

    // -----------------------------------------------------------------------
    protected override void onUpdate() 
    {
      List<CollisionBox> willAdd = new List<CollisionBox>();
      List<CollisionBox> buffer = new List<CollisionBox>(collidedBoxes);

      for (int i = 0; i < boxesOnScene.Count; i++) {
        var box = boxesOnScene[i];
        if (!box.Equals(this) && this.isIntersect(box)) {
          willAdd.Add(box); 
        }
      }

      for (int i = 0; i < willAdd.Count; i++) {
        var box = willAdd[i];
        if(!collidedBoxes.Contains(box)) {
          collisionStart?.Invoke(this);
          collidedBoxes.Add(box);
        }
      }

      for (int i = 0; i < buffer.Count; i++) {
        var box = buffer[i];
        if (!willAdd.Contains(box)) {
          collidedBoxes.Remove(box);
          collisionEnd?.Invoke(box);
        }
      }

      base.onUpdate();
    }

    // -----------------------------------------------------------------------
    IEnumerable<CollisionBox> getAllCollisionBoxes()
    {
      return AppDomain.CurrentDomain.GetAssemblies().ToList().SelectMany(assembly => assembly.GetTypes()).Where(type => type.IsSubclassOf(typeof(CollisionBox))).Select(type => Activator.CreateInstance(type) as CollisionBox);
    }

#if UNITY_EDITOR
    static Color boxBorderColor = new Color(0,1,0,1);
    static Color boxColor = new Color(0,1,0,0.5f);

    // -----------------------------------------------------------------------
    public override void OnDrawGizmos() 
    {
      Gizmos.color = boxColor;
      Gizmos.DrawCube(center, size);
      Gizmos.color = boxBorderColor;
      Gizmos.DrawWireCube(center, size);
      Gizmos.color = Color.white;
      base.OnDrawGizmos();
    }
#endif    


  }
}