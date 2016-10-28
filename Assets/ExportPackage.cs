using UnityEngine;
using System.Collections;
using UnityEditor;

public static class ExportEverythingBitch
{

    [MenuItem("Custom Tools/Export Project As Package")]
    public static void ExportPackage()
    {
        string[] projectContent = new string[] { "Assets", "ProjectSettings/TagManager.asset" };
        AssetDatabase.ExportPackage(projectContent, "ExportedPackage.unitypackage", ExportPackageOptions.Interactive | ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);
        Debug.Log("Project Exported");
    }

}