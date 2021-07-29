using UnityEngine;
using UnityEditor;

public class PackageTool
{
	[MenuItem("Package/Update Package")]
	static void UpdatePackage()
	{
		AssetDatabase.ExportPackage( "Assets/Gizmores", "Gizmores.unitypackage", ExportPackageOptions.Recurse );
		Debug.Log( "Updated packages\n" );
	}
}