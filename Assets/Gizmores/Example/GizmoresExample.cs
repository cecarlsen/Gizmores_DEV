/*
	Copyright © Carl Emil Carlsen 2018-2022
	http://cec.dk
*/

using UnityEngine;

public class GizmoresExample : MonoBehaviour
{
	[SerializeField] float _arcAngleBegin = 0;
	[SerializeField] float _arcAngleEnd = 110;
	[SerializeField] Camera _camera;

	public float coneAngle = 90;


	void OnDrawGizmos()
	{
		Vector3 pos = Vector3.zero;

		Gizmores.DrawWireRect( new Rect( 0, 0, 1f, 1f ) );
		Next( ref pos );

		pos.x = 0; pos.z += 2;
		Gizmores.DrawWireCircle( pos, 0.5f, Gizmores.Axis.X );
		Next( ref pos );
		Gizmores.DrawWireCircle( pos, 0.5f, Gizmores.Axis.Y );
		Next( ref pos );
		Gizmores.DrawWireCircle( pos, 0.5f, Gizmores.Axis.Z );
		Next( ref pos );

		pos.x = 0; pos.z += 2;
		Gizmores.DrawWireArc( pos, 0.5f, _arcAngleBegin, _arcAngleEnd, Gizmores.Axis.X );
		Next( ref pos );
		Gizmores.DrawWireArc( pos, 0.5f, _arcAngleBegin, _arcAngleEnd, Gizmores.Axis.Y );
		Next( ref pos );
		Gizmores.DrawWireArc( pos, 0.5f, _arcAngleBegin, _arcAngleEnd, Gizmores.Axis.Z );
		Next( ref pos );

		pos.x = 0; pos.z += 2;
		Gizmores.DrawWireDome( pos, 0.5f, Gizmores.Axis.X );
		Next( ref pos );
		Gizmores.DrawWireDome( pos, 0.5f, Gizmores.Axis.Y );
		Next( ref pos );
		Gizmores.DrawWireDome( pos, 0.5f, Gizmores.Axis.Z );
		Next( ref pos );

		pos.x = 0; pos.z += 2;
		Gizmores.DrawWireCyllinder( pos, 2, 0.5f );
		Next( ref pos );

		pos.x = 0; pos.z += 2;
		Gizmores.DrawWireCapsule( pos, 2, 0.5f );
		Next( ref pos );

		pos.x = 0; pos.z += 2;
		Gizmores.DrawWireCone( pos, 1, coneAngle, Gizmores.Axis.X );
		Next( ref pos );
		Gizmores.DrawWireCone( pos, 1, coneAngle, Gizmores.Axis.Y );
		Next( ref pos );
		Gizmores.DrawWireCone( pos, 1, coneAngle, Gizmores.Axis.Z );
		Next( ref pos );
		Gizmores.DrawWireCone( pos, 1, coneAngle, Gizmores.Axis.X, false );
		Next( ref pos );
		Gizmores.DrawWireCone( pos, 1, coneAngle, Gizmores.Axis.Y, false );
		Next( ref pos );
		Gizmores.DrawWireCone( pos, 1, coneAngle, Gizmores.Axis.Z, false );
		Next( ref pos );

		pos.x = 0; pos.z += 2;
		Gizmores.DrawWireSphericalCone( pos, 1, coneAngle, Gizmores.Axis.X );
		Next( ref pos );
		Gizmores.DrawWireSphericalCone( pos, 1, coneAngle, Gizmores.Axis.Y );
		Next( ref pos );
		Gizmores.DrawWireSphericalCone( pos, 1, coneAngle, Gizmores.Axis.Z );

		pos.x = 0; pos.z += 2;
		Gizmores.DrawWireCross( pos, 1 );
		Next( ref pos );

		pos.x = 0; pos.z += 2;
		Gizmores.DrawWireAxis( pos, 0.5f );
		Next( ref pos );

		if( _camera ) Gizmores.DrawFrustum( _camera ); // You can also pass a projection matrix instead.
	}


	static void Next( ref Vector3 position )
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere( position, 0.04f );
		Gizmos.color = Color.white;

		position.x += 2;
	}
}
