/* --------------------------------------------------------------------------
    Title       :  Character.cs
    Date        :  03 Oct 2023
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
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
// ---------------------------------------------------------------------------
namespace Persephone {

// -------------------------------------------------------------------------
public class Character : MonoBehaviour
{
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
}
}