//Written By Gabriel Tupy on 11-23-20
//Last Modified by Gabriel Tupy on 11-28-20

using UnityEngine;

/// <summary>
/// A script that allows you to disable/enable a set gameobject.
/// </summary>
public class ToggleGameObjectComponent : MonoBehaviour
{
    //Stored Variables
    #region
    [Tooltip("The GameObject to be Toggled.")] [SerializeField] private GameObject toggleObject = null;
    #endregion

    /// <summary>
    /// Toggles the ToggleObject.
    /// </summary>
    public void ToggleObject()
    {
        if (toggleObject != null)
        {
            toggleObject.SetActive(!toggleObject.activeSelf);
        }
    }
}
