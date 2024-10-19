/* --------------------------------------------------------------------------
  Title       :  Box.cs
  Date        :  29 Fri 2023
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


using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Persephone {

  [ExecuteAlways]
  [System.Serializable]
  public class Box : MonoBehaviour
  {
    public Vector3 center => position;
    public Vector3 max => size / 2 + position;
    public Vector3 min => position - size / 2;

    public Vector3 nearBottomRight => new Vector3(size.x / 2 + position.x, position.y - size.y / 2, position.z - size.z / 2);
    public Vector3 nearBottomLeft => min;
    public Vector3 farBottomRight => new Vector3(size.x / 2 + position.x, position.y - size.y / 2, position.z + size.z / 2);
    public Vector3 farBottomLeft => new Vector3(position.x - size.x / 2, position.y - size.y / 2, position.z + size.z / 2);

    public Vector3 nearTopRight => new Vector3(position.x + size.x / 2, position.y + size.y / 2, position.z - size.z / 2);
    public Vector3 nearTopLeft => new Vector3(position.x - size.x / 2, position.y + size.y / 2, position.z - size.z / 2);
    public Vector3 farTopRight => max;
    public Vector3 farTopLeft => new Vector3(position.x - size.x / 2, position.y + size.y / 2, position.z + size.z / 2);
    Vector3 position => transform.position;

    public Vector3 size {
      get {
        return _size;
      }
      set {
        if (value != _size) {
          _size = value; 
        }
      }
    }

    public bool active {
      get {
        return _active;
      }
      set {
        if (value != _active) {
          _active = value;
        }
      }
    }

    public IEnumerable<Vector3> corners {
      get {
        yield return nearBottomRight;
        yield return nearBottomLeft;
        yield return farBottomRight;
        yield return farBottomLeft;
        yield return nearTopRight;
        yield return nearTopLeft;
        yield return farTopRight;
        yield return farTopLeft;
      }
    }


    [SerializeField] Vector3 _size = new Vector3(1,1,1);
    [SerializeField] bool _active = true;

    public bool isIntersect(Vector3 point) 
    {
      return intersectWithPoint(point);
    }

    public bool isIntersect(Box box) 
    {
      return intersects(box);
    }

    protected virtual void onUpdate() 
    { 
    }

    protected virtual void onEnable() 
    {
    }

    protected virtual void onDisable() 
    {
    }

    void Update() 
    {
      if (Application.isPlaying) {
        onUpdate();
      }
    }

    void OnDisable() 
    {
      if (Application.isPlaying) {
        onDisable();
      }
    }

    void OnEnable() 
    {
      if (Application.isPlaying) {
        onEnable();
      }
    }
    
    bool intersectWithPoint(Vector3 point) 
    {
      bool intersected = false;

      if ((point.x > min.x && point.x <= max.x) && (point.y > min.y && point.y <= max.y) && (point.z > min.z && point.z <= max.z)) {
        intersected = true;
      }
      
      return intersected;
    }

    bool intersects(Box box) 
    {
      bool intersected = false;
      foreach (var corner in box.corners) {
        intersected = intersectWithPoint(corner);
        if (intersected) {
          break;
        }
      }

      return intersected;
    }

#if UNITY_EDITOR
    public virtual void OnDrawGizmos() 
    {
      Gizmos.color = Color.red;
      Gizmos.DrawCube(max, Vector3.one * 0.1f);
      Gizmos.color = Color.green;
      Gizmos.DrawCube(min, Vector3.one * 0.1f);
    }
#endif
  }
}