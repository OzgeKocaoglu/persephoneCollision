/* --------------------------------------------------------------------------
  Title       :  BoxEditor.cs
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


using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

// ---------------------------------------------------------------------------
namespace Persephone {
  // -------------------------------------------------------------------------
  [CustomEditor(typeof(Box), true)]
  public class BoxEditor : Editor
  {
    Box box;

    // -----------------------------------------------------------------------
    private void OnEnable() 
    {
      box = target as Box;
    }

    // -----------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
      EditorGUI.BeginChangeCheck();
      var activeValue = EditorGUILayout.Toggle("Active", box.active);
      var sizeValue = EditorGUILayout.Vector3Field("Size", box.size);

      if (EditorGUI.EndChangeCheck()) {
        box.size = sizeValue;
        box.active = activeValue;
      }
    }

    // -----------------------------------------------------------------------
    private void OnSceneGUI() 
    {
      //TODO : size handles will be implement
      var color = new Color(1, 0.8f, 0.4f, 1);
      Handles.color = color;
      float size = HandleUtility.GetHandleSize(box.center) * 0.5f;
      Vector3 snap = Vector3.one * 0.5f;
      Handles.FreeMoveHandle(box.center + new Vector3(0, box.size.y / 2f, 0 ), Quaternion.Euler(Vector3.up), 0.1f, snap, Handles.CubeHandleCap);
      GUI.color = color;

    }
  }
}