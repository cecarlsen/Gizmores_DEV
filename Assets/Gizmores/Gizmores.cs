﻿/*
	Gizmores - more gizmos.

	The MIT License (MIT)

		Copyright (c) 2018-2022, Carl Emil Carlsen http://cec.dk

		Permission is hereby granted, free of charge, to any person obtaining a copy
		of this software and associated documentation files (the "Software"), to deal
		in the Software without restriction, including without limitation the rights
		to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
		copies of the Software, and to permit persons to whom the Software is
		furnished to do so, subject to the following conditions:

		The above copyright notice and this permission notice shall be included in
		all copies or substantial portions of the Software.

		THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
		IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
		FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
		AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
		LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
		OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
		THE SOFTWARE.
*/

using UnityEngine;

public static class Gizmores
{
	static Vector2[] circlePoints;

	const int cirlceResolution = 32;

	public enum Axis { X, Y, Z }


	/// <summary>
	/// Draw a rect (zero at center).
	/// </summary>
	/// <param name="rect"></param>
	public static void DrawWireRect( Rect rect )
	{
		Vector2 min = rect.min;
		Vector2 max = rect.max;
		Gizmos.DrawLine( new Vector3( min.x, min.y ), new Vector3( min.x, max.y ) );
		Gizmos.DrawLine( new Vector3( min.x, max.y ), new Vector3( max.x, max.y ) );
		Gizmos.DrawLine( new Vector3( max.x, max.y ), new Vector3( max.x, min.y ) );
		Gizmos.DrawLine( new Vector3( max.x, min.y ), new Vector3( min.x, min.y ) );
	}


	/// <summary>
	/// Draw a circle.
	/// </summary>
	public static void DrawWireCircle( Vector3 center, float radius, Axis axis = Axis.Z )
	{
		if( circlePoints == null ) CreateCirlcePoints();

		Vector3 pos = Vector3.zero;
		Vector3 prevPos = Vector3.zero;
		Vector2 circlePos = circlePoints[cirlceResolution-1];

		switch( axis )
		{
			case Axis.X:
				prevPos.Set( center.x, center.y+circlePos.x*radius, center.z+circlePos.y*radius );
				for( int i = 0; i < cirlceResolution; i++ ){
					circlePos = circlePoints[i];
					pos.Set( center.x, center.y+circlePos.x*radius, center.z+circlePos.y*radius );
					Gizmos.DrawLine( prevPos, pos );
					prevPos = pos;
				}
				break;
			case Axis.Y:
				prevPos.Set( center.x+circlePos.x*radius, center.y, center.z+circlePos.y*radius );
				for( int i = 0; i < cirlceResolution; i++ ){
					circlePos = circlePoints[i];
					pos.Set( center.x+circlePos.x*radius, center.y, center.z+circlePos.y*radius );
					Gizmos.DrawLine( prevPos, pos );
					prevPos = pos;
				}
				break;

			case Axis.Z:
				prevPos.Set( center.x+circlePos.x*radius, center.y+circlePos.y*radius, center.z );
				for( int i = 0; i < cirlceResolution; i++ ){
					circlePos = circlePoints[i];
					pos.Set( center.x+circlePos.x*radius, center.y+circlePos.y*radius, center.z );
					Gizmos.DrawLine( prevPos, pos );
					prevPos = pos;
				}
				break;
		}
	}


	/// <summary>
	/// Draw an arc (angles in degrees).
	/// </summary>
	public static void DrawWireArc( Vector3 center, float radius, float angleBegin, float angleEnd, Axis axis = Axis.Z )
	{
		if( circlePoints == null ) CreateCirlcePoints();

		if( angleEnd > angleBegin + 360 ) angleEnd = angleBegin + 360;
		Vector3 pos = Vector3.zero;
		Vector3 prevPos = Vector3.zero;
		Vector3 circlePos;
		Matrix4x4 rotation = Matrix4x4.Rotate( Quaternion.AngleAxis( angleBegin, Vector3.forward ) ); // This could be done with a Matrix2x2
		float angleEndRad = angleEnd * Mathf.Deg2Rad;
		Vector3 circlePosEnd = new Vector2( Mathf.Cos( angleEndRad ), Mathf.Sin( angleEndRad ) ) * radius;
		float angleRange = Mathf.Min( 360, angleEnd - angleBegin );
		int pointCount = Mathf.CeilToInt( cirlceResolution * angleRange / 360f );

		switch( axis )
		{
			case Axis.X:
				circlePos = rotation * circlePoints[0] * radius;
				prevPos.Set( center.x+circlePos.z, center.y+circlePos.y, center.z+circlePos.x );
				for( int i = 1; i < pointCount; i++ ){
					circlePos = rotation * circlePoints[i] * radius;
					pos.Set( center.x+circlePos.z, center.y+circlePos.y, center.z+circlePos.x );
					Gizmos.DrawLine( prevPos, pos );
					prevPos = pos;
				}
				pos.Set( center.x+circlePosEnd.z, center.y+circlePosEnd.y, center.z+circlePosEnd.x );
				Gizmos.DrawLine( prevPos, pos );
				break;
			case Axis.Y:
				circlePos = rotation * circlePoints[0] * radius;
				prevPos.Set( center.x+circlePos.x, center.y+circlePos.z, center.z+circlePos.y );
				for( int i = 1; i < pointCount; i++ ){
					circlePos = rotation * circlePoints[i] * radius;
					pos.Set( center.x+circlePos.x, center.y+circlePos.z, center.z+circlePos.y );
					Gizmos.DrawLine( prevPos, pos );
					prevPos = pos;
				}
				pos.Set( center.x+circlePosEnd.x, center.y+circlePosEnd.z, center.z+circlePosEnd.y );
				Gizmos.DrawLine( prevPos, pos );
				break;

			case Axis.Z:
				circlePos = rotation * circlePoints[0] * radius;
				prevPos.Set( center.x+circlePos.x, center.y+circlePos.y, center.z+circlePos.z );
				for( int i = 1; i < pointCount; i++ ){
					circlePos = rotation * circlePoints[i] * radius;
					pos.Set( center.x+circlePos.x, center.y+circlePos.y, center.z+circlePos.z );
					Gizmos.DrawLine( prevPos, pos );
					prevPos = pos;
				}
				pos.Set( center.x+circlePosEnd.x, center.y+circlePosEnd.y, center.z+circlePosEnd.z );
				Gizmos.DrawLine( prevPos, pos );
				break;
		}
	}


	/// <summary>
	/// Draw a dome (half sphere).
	/// </summary>
	public static void DrawWireDome( Vector3 center, float radius, Axis axis = Axis.Z, bool mirror = false )
	{
		DrawWireCircle( center, radius, axis );
		float aMin1 = 0;
		float aMax1 = 180;
		float aMin2 = -90;
		float aMax2 = 90;
		if( mirror ) {
			aMin1 += 180;
			aMax1 += 180;
			aMin2 += 180;
			aMax2 += 180;
		}
		switch( axis ){
			case Axis.X:
				DrawWireArc( center, radius, aMin1, aMax1, Axis.Z );
				DrawWireArc( center, radius, aMin2, aMax2, Axis.Y );
				break;
			case Axis.Y:
				DrawWireArc( center, radius, aMin1, aMax1, Axis.X );
				DrawWireArc( center, radius, aMin2, aMax2, Axis.Z );
				break;
			case Axis.Z:
				DrawWireArc( center, radius, aMin1, aMax1, Axis.Y );
				DrawWireArc( center, radius, aMin2, aMax2, Axis.X );
				break;
		}
	}


	/// <summary>
	/// Draw a cylinder.
	/// </summary>
	public static void DrawWireCyllinder( Vector3 center, float cyllinderHeight, float radius )
	{
		if( circlePoints == null ) CreateCirlcePoints();

		float halfHeight = cyllinderHeight * 0.5f;
		
		// Side lines.
		Gizmos.DrawLine( new Vector3( center.x-radius, center.y-halfHeight, center.z ), new Vector3( center.x-radius, center.y+halfHeight, center.z ) );
		Gizmos.DrawLine( new Vector3( center.x+radius, center.y-halfHeight, center.z ), new Vector3( center.x+radius, center.y+halfHeight, center.z ) );
		Gizmos.DrawLine( new Vector3( center.x, center.y-halfHeight, center.z-radius ), new Vector3( center.x, center.y+halfHeight, center.z-radius ) );
		Gizmos.DrawLine( new Vector3( center.x, center.y-halfHeight, center.z+radius ), new Vector3( center.x, center.y+halfHeight, center.z+radius ) );
		
		// Lower and upper cirlces.
		DrawWireCircle( new Vector3( center.x, center.y-halfHeight, center.z ), radius, Axis.Y );
		DrawWireCircle( new Vector3( center.x, center.y+halfHeight, center.z ), radius, Axis.Y );
	}


	/// <summary>
	/// Draw a capsule.
	/// </summary>
	public static void DrawWireCapsule( Vector3 center, float cyllinderHeight, float radius )
	{
		if( circlePoints == null ) CreateCirlcePoints();

		float halfHeight = cyllinderHeight * 0.5f;
		Vector3 top = new Vector3( center.x, center.y+halfHeight, center.z );
		Vector3 bottom = new Vector3( center.x, center.y-halfHeight, center.z );

		// Side lines.
		Gizmos.DrawLine( new Vector3( top.x-radius, top.y, top.z ), new Vector3( bottom.x-radius, bottom.y, bottom.z ) );
		Gizmos.DrawLine( new Vector3( top.x+radius, top.y, top.z ), new Vector3( bottom.x+radius, bottom.y, center.z ) );
		Gizmos.DrawLine( new Vector3( top.x, top.y, top.z-radius ), new Vector3( bottom.x, bottom.y, bottom.z-radius ) );
		Gizmos.DrawLine( new Vector3( top.x, top.y, top.z+radius ), new Vector3( bottom.x, bottom.y, bottom.z+radius ) );
		
		// Lower and upper cirlces.
		DrawWireCircle( top, radius, Axis.Y );
		DrawWireCircle( bottom, radius, Axis.Y );

		// Caps.
		DrawWireArc( top, radius, -90, 90, Axis.Z );
		DrawWireArc( top, radius, 0, 180, Axis.X );
		DrawWireArc( bottom, radius, 90, 270, Axis.Z );
		DrawWireArc( bottom, radius, 180, 360, Axis.X );
	}


	/// <summary>
	/// Draw a cone.
	/// </summary>
	public static void DrawWireCone( Vector3 position, float height, float angle, Axis axis = Axis.Z, bool originAtBottom = true )
	{
		float radius = Mathf.Tan( angle * 0.5f * Mathf.Deg2Rad ) * height;
		Vector3 forward = AxisDirection( axis );
		Vector3 right = new Vector3( forward[ 1 ], forward[ 2 ], forward[ 0 ] ) * radius;
		Vector3 up = new Vector3( forward[ 2 ], forward[ 0 ], forward[ 1 ] ) * radius;
		forward *= height;
		Vector3 top = position + forward;
		if( originAtBottom ){
			DrawWireCircle( position, radius, axis );
			Gizmos.DrawLine( top, position + right );
			Gizmos.DrawLine( top, position - right );
			Gizmos.DrawLine( top, position + up );
			Gizmos.DrawLine( top, position - up );
		} else {
			DrawWireCircle( position + forward, radius, axis );
			Gizmos.DrawLine( position, top + right );
			Gizmos.DrawLine( position, top - right );
			Gizmos.DrawLine( position, top + up );
			Gizmos.DrawLine( position, top - up );
		}
	}


	/// <summary>
	/// Draw a spherical cone.
	/// </summary>
	public static void DrawWireSphericalCone( Vector3 position, float radius, float angle, Axis axis = Axis.Z )
	{
		float circleOffset = Mathf.Cos( angle * 0.5f * Mathf.Deg2Rad ) * radius;
		float circleRadius = Mathf.Sin( angle * 0.5f * Mathf.Deg2Rad ) * radius;
		Vector3 forward = AxisDirection( axis );
		Vector3 right = new Vector3( forward[ 1 ], forward[ 2 ], forward[ 0 ] ) * circleRadius;
		Vector3 up = new Vector3( forward[ 2 ], forward[ 0 ], forward[ 1 ] ) * circleRadius;
		forward *= circleOffset;
		Vector3 top = position + forward;
		DrawWireCircle( position + forward, circleRadius, axis );
		Gizmos.DrawLine( position, top + right );
		Gizmos.DrawLine( position, top - right );
		Gizmos.DrawLine( position, top + up );
		Gizmos.DrawLine( position, top - up );
		float angleOffset = axis == Axis.Y ? 90f : 0f;
		DrawWireArc( position, radius, -angle * 0.5f + angleOffset, angle * 0.5f + angleOffset, (Axis) ( ( (int) axis + 1 ) % 3 ) );
		angleOffset = axis == Axis.X ? 90f : 0f;
		DrawWireArc( position, radius, -angle * 0.5f + 90 - angleOffset, angle * 0.5f + 90 - angleOffset, (Axis) ( ( (int) axis + 2 ) % 3 ) );
	}


	/// <summary>
	/// Draw a spherical segment.
	/// </summary>
	public static void DrawWireSphericalSegment( Vector3 position, float radius, float angle, Axis axis = Axis.Z )
	{
		float circleOffset = Mathf.Cos( angle * 0.5f * Mathf.Deg2Rad ) * radius;
		float circleRadius = Mathf.Sin( angle * 0.5f * Mathf.Deg2Rad ) * radius;
		Vector3 forward = AxisDirection( axis );
		forward *= circleOffset;
		DrawWireCircle( position + forward, circleRadius, axis );
		DrawWireArc( position, radius, -angle * 0.5f, angle * 0.5f, (Axis) ( ( (int) axis + 1 ) % 3 ) );
		DrawWireArc( position, radius, -angle * 0.5f + 90, angle * 0.5f + 90, (Axis) ( ( (int) axis + 2 ) % 3 ) );
	}


	/// <summary>
	/// Draw a bone.
	/// </summary>
	public static void DrawWireBone( Vector3 position, float radius, float length, Axis axis = Axis.Z, bool mirror = false )
	{
		Vector3 forward = AxisDirection( axis );
		Vector3 right = new Vector3( forward[ 1 ], forward[ 2 ], forward[ 0 ] ) * radius;
		Vector3 up = new Vector3( forward[ 2 ], forward[ 0 ], forward[ 1 ] ) * radius;
		Vector3 top = position + forward * length * ( mirror ? -1 : 1 );
		DrawWireDome( position, radius, axis, !mirror );
		Gizmos.DrawLine( top, position + right );
		Gizmos.DrawLine( top, position - right );
		Gizmos.DrawLine( top, position + up );
		Gizmos.DrawLine( top, position - up );
	}


	/// <summary>
	/// Draw a label.
	/// </summary>
	public static void DrawLabel( Vector3 position, string text )
	{
		#if UNITY_EDITOR
		UnityEditor.Handles.Label( position, text );
		#endif
	}


	/// <summary>
	/// Draw a camera frustum.
	/// </summary>
	public static void DrawFrustum( Matrix4x4 viewMatrix,  Matrix4x4 projectionMatrix, bool isGL = false )
	{
		if( !isGL ) viewMatrix = Matrix4x4.Scale( new Vector3( 1f, 1f, -1f ) ) * viewMatrix;
		projectionMatrix *= viewMatrix;
		projectionMatrix = projectionMatrix.inverse;
		Vector3 f0 = projectionMatrix.MultiplyPoint( new Vector3( -1f, -1f, -1f ) );
		Vector3 f1 = projectionMatrix.MultiplyPoint( new Vector3( -1f, 1f, -1f ) );
		Vector3 f2 = projectionMatrix.MultiplyPoint( new Vector3( 1f, 1f,-1f ) );
		Vector3 f3 = projectionMatrix.MultiplyPoint( new Vector3( 1f, -1f, -1f ) );
		Vector3 b0 = projectionMatrix.MultiplyPoint( new Vector3( -1f, -1f, 1f ) );
		Vector3 b1 = projectionMatrix.MultiplyPoint( new Vector3( -1f, 1f, 1f ) );
		Vector3 b2 = projectionMatrix.MultiplyPoint( new Vector3( 1f, 1f, 1f ) );
		Vector3 b3 = projectionMatrix.MultiplyPoint( new Vector3( 1f, -1f, 1f ) );
		Gizmos.DrawLine( f0, f1 );
		Gizmos.DrawLine( f1, f2 );
		Gizmos.DrawLine( f2, f3 );
		Gizmos.DrawLine( f3, f0 );
		Gizmos.DrawLine( b0, b1 );
		Gizmos.DrawLine( b1, b2 );
		Gizmos.DrawLine( b2, b3 );
		Gizmos.DrawLine( b3, b0 );
		Gizmos.DrawLine( f0, b0 );
		Gizmos.DrawLine( f1, b1 );
		Gizmos.DrawLine( f2, b2 );
		Gizmos.DrawLine( f3, b3 );
	}


	/// <summary>
	/// Draw a camera frustum.
	/// </summary>
	public static void DrawFrustum( Camera camera )
	{
		DrawFrustum( camera.transform.worldToLocalMatrix, camera.projectionMatrix );
	}


	/// <summary>
	/// Draw a 3D wire cross hair.
	/// </summary>
	public static void DrawWireCross( Vector3 position, float size )
    {
		float extents = size * 0.5f;
		Gizmos.DrawLine( position + Vector3.left * extents, position + Vector3.right * extents );
		Gizmos.DrawLine( position + Vector3.down * extents, position + Vector3.up * extents );
		Gizmos.DrawLine( position + Vector3.back * extents, position + Vector3.forward * extents );
	}


	/// <summary>
	/// Draw axis lines.
	/// </summary>
	public static void DrawWireAxis( Vector3 position, float extents, bool colorize = true )
	{
		if( colorize ) Gizmos.color = Color.red;
		Gizmos.DrawLine( position, position + Vector3.right * extents );
		if( colorize ) Gizmos.color = Color.green;
		Gizmos.DrawLine( position, position + Vector3.up * extents );
		if( colorize ) Gizmos.color = Color.blue;
		Gizmos.DrawLine( position, position + Vector3.forward * extents );
	}


	static void CreateCirlcePoints()
	{
		circlePoints = new Vector2[cirlceResolution];
		float aStep = Mathf.PI * 2 / (float) cirlceResolution;
		for( int i = 0; i < cirlceResolution; i++ ) {
			float a = i * aStep;
			circlePoints[i].Set( Mathf.Cos( a ), Mathf.Sin( a ) );
		}
	}


	static Vector3 AxisDirection( Axis axis )
	{
		switch( axis ) {
			case Axis.X: return Vector3.right;
			case Axis.Y: return Vector3.up;
			default: return Vector3.forward;
		}
	}
}