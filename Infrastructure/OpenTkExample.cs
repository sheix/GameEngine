using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Collections.Generic;

namespace Infrastructure
{
	public struct VertexT2dN3dV3d
	{
		public Vector2d TexCoord;
		public Vector3d Normal;
		public Vector3d Position;

		public VertexT2dN3dV3d( Vector2d texcoord, Vector3d normal, Vector3d position )
		{
			TexCoord = texcoord;
			Normal = normal;
			Position = position;
		}
	}
	public class OpenTKTest
	{
		public OpenTKTest ()
		{
		}
	}

	public struct VertexT2fN3fV3f
	{
		public Vector2 TexCoord;
		public Vector3 Normal;
		public Vector3 Position;
	}

	public struct VertexT2hN3hV3h
	{
		public Vector2h TexCoord;
		public Vector3h Normal;
		public Vector3h Position;
	}
	public abstract class DrawableShape: IDisposable
	{
		protected BeginMode PrimitiveMode;
		protected VertexT2dN3dV3d[] VertexArray;
		protected uint[] IndexArray;

		public int GetTriangleCount
		{
			get
			{
				switch ( PrimitiveMode )
				{ 
				case BeginMode.Triangles:
					if ( IndexArray != null )
					{
						return IndexArray.Length / 3;
					} else
					{
						return VertexArray.Length / 3;
					}
					//  break;
				default: throw new NotImplementedException("Unknown primitive type.");
				}
			}
		}

		#region Display List

		private bool UseDisplayList;
		private int DisplayListHandle = 0;

		#endregion Display List

		public DrawableShape( bool useDisplayList )
		{
			UseDisplayList = useDisplayList;
			PrimitiveMode = BeginMode.Triangles;
			VertexArray = null;
			IndexArray = null;
		}

		#region Convert to VBO

		public void GetArraysforVBO(out BeginMode primitives, out VertexT2dN3dV3d[] vertices, out uint[] indices)
		{
			primitives = PrimitiveMode;

			vertices = new VertexT2dN3dV3d[VertexArray.Length];
			for (uint i = 0; i < VertexArray.Length; i++)
			{
				vertices[i].TexCoord = VertexArray[i].TexCoord;
				vertices[i].Normal = VertexArray[i].Normal;
				vertices[i].Position = VertexArray[i].Position;
			}

			indices = IndexArray;
		}

		public void GetArraysforVBO(out BeginMode primitives, out VertexT2fN3fV3f[] vertices, out uint[] indices)
		{
			primitives = PrimitiveMode;

			vertices = new VertexT2fN3fV3f[VertexArray.Length];
			for (uint i = 0; i < VertexArray.Length; i++)
			{
				vertices[i].TexCoord = (Vector2)VertexArray[i].TexCoord;
				vertices[i].Normal = (Vector3)VertexArray[i].Normal;
				vertices[i].Position = (Vector3)VertexArray[i].Position;
			}

			indices = IndexArray;
		}

		public void GetArraysforVBO(out BeginMode primitives, out VertexT2hN3hV3h[] vertices, out uint[] indices)
		{
			primitives = PrimitiveMode;

			vertices = new VertexT2hN3hV3h[VertexArray.Length];
			for (uint i = 0; i < VertexArray.Length; i++)
			{
				vertices[i].TexCoord = (Vector2h)VertexArray[i].TexCoord;
				vertices[i].Normal = (Vector3h)VertexArray[i].Normal;
				vertices[i].Position = (Vector3h)VertexArray[i].Position;
			}

			indices = IndexArray;
		}

		#endregion Convert to VBO

		private void DrawImmediateMode()
		{
			GL.Begin( PrimitiveMode );
			{
				if ( IndexArray == null )
					foreach ( VertexT2dN3dV3d v in VertexArray )
					{
						GL.TexCoord2( v.TexCoord.X, v.TexCoord.Y );
						GL.Normal3( v.Normal.X, v.Normal.Y, v.Normal.Z );
						GL.Vertex3( v.Position.X, v.Position.Y, v.Position.Z );
					} else
				{
					for ( uint i = 0; i < IndexArray.Length; i++ )
					{
						uint index = IndexArray[i];
						GL.TexCoord2( VertexArray[index].TexCoord.X, VertexArray[index].TexCoord.Y );
						GL.Normal3( VertexArray[index].Normal.X, VertexArray[index].Normal.Y, VertexArray[index].Normal.Z );
						GL.Vertex3( VertexArray[index].Position.X, VertexArray[index].Position.Y, VertexArray[index].Position.Z );
					}
				}
			}
			GL.End();
		}

		/// <summary>
		/// Does not touch any state/matrices. Does call Begin/End and Vertex&Co.
		/// Creates and compiles a display list if not present yet. Requires an OpenGL context.
		/// </summary>
		public void Draw()
		{
			if ( !UseDisplayList )
				DrawImmediateMode();
			else
				if ( DisplayListHandle == 0 )
				{
					if ( VertexArray == null )
						throw new Exception("Cannot draw null Vertex Array.");
					DisplayListHandle = GL.GenLists( 1 );
					GL.NewList( DisplayListHandle, ListMode.CompileAndExecute );
					DrawImmediateMode();
					GL.EndList();
				} else
					GL.CallList( DisplayListHandle );
		}

		#region IDisposable Members

		/// <summary>
		/// Removes reference to VertexArray and IndexArray.
		/// Deletes the Display List, so it requires an OpenGL context.
		/// The instance is effectively destroyed.
		/// </summary>
		public void Dispose()
		{
			if ( VertexArray != null )
				VertexArray = null;
			if ( IndexArray != null )
				IndexArray = null;
			if ( DisplayListHandle != 0 )
			{
				GL.DeleteLists( DisplayListHandle, 1 );
				DisplayListHandle = 0;
			}
		}

		#endregion
	}

	class Anaglyph : GameWindow
	{

		DrawableShape Object;

		/// <summary>Creates a 800x600 window with the specified title.</summary>
		public Anaglyph()// :base(
			: base(800, 600, GraphicsMode.Default, "OpenTK Quick Start Sample", GameWindowFlags.Default, DisplayDevice.Default, 2, 1, GraphicsContextFlags.Default)
		{
			VSync = VSyncMode.On;
		}

		/// <summary>Load resources here.</summary>
		/// <param name="e">Not used.</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			GL.ClearColor(System.Drawing.Color.Black);
			GL.Enable(EnableCap.DepthTest);

			GL.Enable( EnableCap.Lighting );
			GL.Enable( EnableCap.Light0 );

			Object = new MengerSponge(1.0, MengerSponge.eSubdivisions.Two, true );
			// Object = new Examples.Shapes.TorusKnot( 256, 32, 0.1, 3, 4, 1, true );
		}

		protected override void OnUnload( EventArgs e )
		{
			base.OnUnload( e );

			Object.Dispose();
		}

		/// <summary>
		/// Called when your window is resized. Set your viewport here. It is also
		/// a good place to set up your projection matrix (which probably changes
		/// along when the aspect ratio of your window).
		/// </summary>
		/// <param name="e">Not used.</param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			GL.Viewport(ClientRectangle);
		}

		/// <summary>
		/// Called when it is time to setup the next frame. Add you game logic here.
		/// </summary>
		/// <param name="e">Contains timing information for framerate independent logic.</param>
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);

			if (Keyboard[Key.Escape])
				Exit();
		}

		struct Camera
		{
			public Vector3 Position, Direction, Up;
			public double NearPlane, FarPlane;
			public double EyeSeparation;
			public double Aperture; // FOV in degrees
			public double FocalLength;
		}

		enum Eye
		{
			left,
			right,
		}

		void SetupCamera( Eye eye )
		{
			Camera camera;

			camera.Position = Vector3.UnitZ;
			camera.Up = Vector3.UnitY;
			camera.Direction = -Vector3.UnitZ;
			camera.NearPlane = 1.0;
			camera.FarPlane = 5.0;
			camera.FocalLength = 2.0;
			camera.EyeSeparation = camera.FocalLength / 30.0;
			camera.Aperture = 75.0;

			double left, right,
			bottom, top;

			double widthdiv2 = camera.NearPlane * Math.Tan( MathHelper.DegreesToRadians( (float)( camera.Aperture / 2.0 ) ) ); // aperture in radians
			double precalc1 = ClientRectangle.Width / (double)ClientRectangle.Height * widthdiv2;
			double precalc2 = 0.5 * camera.EyeSeparation * camera.NearPlane / camera.FocalLength;

			Vector3 Right = Vector3.Cross( camera.Direction, camera.Up ); // Each unit vectors
			Right.Normalize();

			Right.X *= (float)( camera.EyeSeparation / 2.0 );
			Right.Y *= (float)( camera.EyeSeparation / 2.0 );
			Right.Z *= (float)( camera.EyeSeparation / 2.0 );

			// Projection Matrix
			top = widthdiv2;
			bottom = -widthdiv2;
			if ( eye == Eye.right )
			{
				left = -precalc1 - precalc2;
				right = precalc1 - precalc2;
			}
			else
			{
				left = -precalc1 + precalc2;
				right = precalc1 + precalc2;
			}

			GL.MatrixMode( MatrixMode.Projection );
			GL.LoadIdentity();
			GL.Frustum( left, right, bottom, top, camera.NearPlane, camera.FarPlane );

			// Modelview Matrix
			Matrix4 modelview;
			if ( eye == Eye.right )
			{
				modelview = Matrix4.LookAt(
					new Vector3( camera.Position.X + Right.X, camera.Position.Y + Right.Y, camera.Position.Z + Right.Z ),
					new Vector3( camera.Position.X + Right.X + camera.Direction.X, camera.Position.Y + Right.Y + camera.Direction.Y, camera.Position.Z + Right.Z + camera.Direction.Z ),
					camera.Up );
			}
			else
			{
				modelview = Matrix4.LookAt(
					new Vector3( camera.Position.X - Right.X, camera.Position.Y - Right.Y, camera.Position.Z - Right.Z ),
					new Vector3( camera.Position.X - Right.X + camera.Direction.X, camera.Position.Y - Right.Y + camera.Direction.Y, camera.Position.Z - Right.Z + camera.Direction.Z ),
					camera.Up );
			}
			GL.MatrixMode( MatrixMode.Modelview );
			GL.LoadIdentity();
			GL.MultMatrix( ref modelview );

		}

		float Angle;

		void Draw()
		{
			GL.Translate( 0f, 0f, -2f );
			GL.Rotate( Angle, Vector3.UnitY );
			Object.Draw();
		}

		/// <summary>
		/// Called when it is time to render the next frame. Add your rendering code here.
		/// </summary>
		/// <param name="e">Contains timing information.</param>
		protected override void OnRenderFrame( FrameEventArgs e )
		{
			Angle += (float)(e.Time *20.0);


			GL.Clear( ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit );
			SetupCamera( Eye.right );
			GL.ColorMask( true, false, false, true );
			Draw();

			GL.Clear( ClearBufferMask.DepthBufferBit ); // 
			SetupCamera( Eye.left );
			GL.ColorMask( false, true, true, true );
			Draw();

			GL.ColorMask( true, true, true, true );
			SwapBuffers();
		}
	}

	public sealed partial class MengerSponge: DrawableShape
	{

		public enum eSubdivisions
		{
			None = 0,
			One = 1,
			Two = 2,
			Three = 3,
		}

		public MengerSponge( double scale, eSubdivisions subdivs, bool useDL )
			: base( useDL )
		{
			List<MengerCube> Cubes;
			switch ( subdivs )
			{
			case eSubdivisions.None:
				CreateDefaultMengerSponge( scale, out Cubes );
				break;
			case eSubdivisions.One:
			case eSubdivisions.Two:
			case eSubdivisions.Three:
				CreateDefaultMengerSponge( scale, out Cubes );
				for ( int i = 0; i < (int)subdivs; i++ )
				{
					List<MengerCube> temp;
					SubdivideMengerSponge( ref Cubes, out temp );
					Cubes = temp;
				}
				break;
			default: throw new ArgumentOutOfRangeException( "Subdivisions other than contained in the enum cause overflows and are not allowed." );
			}

			PrimitiveMode = OpenTK.Graphics.OpenGL.BeginMode.Triangles;

			#region Get Array Dimensions
			uint
			VertexCount = 0,
			IndexCount = 0;

			foreach ( MengerCube c in Cubes )
			{
			uint t1, t2;
			c.GetArraySizes( out t1, out t2 );
			VertexCount += t1;
			IndexCount += t2;
			}

			VertexArray = new VertexT2dN3dV3d[VertexCount];
			IndexArray = new uint[IndexCount];
			#endregion Get Array Dimensions

			List<Chunk> AllChunks = new List<Chunk>();

			#region Build a temporary List of all loose pieces
			foreach ( MengerCube c in Cubes )
			{
			c.GetVboAndIbo( ref AllChunks );
			}
			#endregion Build a temporary List of all loose pieces

			#region Assemble pieces into a single VBO and IBO
			VertexCount = 0;
			IndexCount = 0;

			foreach ( Chunk ch in AllChunks )
			{
			for ( int i = 0; i < ch.Vertices.Length; i++ )
			{
				VertexArray[VertexCount + i] = ch.Vertices[i];
			}

			for ( int i = 0; i < ch.Indices.Length; i++ )
			{
				IndexArray[IndexCount + i] = ch.Indices[i] + VertexCount;
			}

			VertexCount += (uint)ch.Vertices.Length;
			IndexCount += (uint)ch.Indices.Length;
			}

			#endregion Assemble pieces into a single VBO and IBO

			AllChunks.Clear();
		}

		private void CreateDefaultMengerSponge( double halfwidth, out List<MengerCube> output )
		{
			output = new List<MengerCube>( 1 );
			output.Add( new MengerCube( Vector3d.Zero, halfwidth, MengerCube.AllSides, MengerCube.AllSides ) );
		}

		private void SubdivideMengerSponge( ref List<MengerCube> input, out List<MengerCube> output )
		{
			output = new List<MengerCube>( input.Count * 20 );
			foreach ( MengerCube InputCube in input )
			{
				MengerCube[] SubdividedCubes;
				InputCube.Subdivide( out SubdividedCubes );
				for ( int i = 0; i < SubdividedCubes.Length; i++ )
				{
					output.Add( SubdividedCubes[i] );
				}
			}
		}

	}

	public class Chunk
	{
		public VertexT2dN3dV3d[] Vertices;
		public uint[] Indices;

		public uint VertexCount
		{
			get
			{
				return (uint)Vertices.Length;
			}
		}
		public uint IndexCount
		{
			get
			{
				return (uint)Indices.Length;
			}
		}

		public Chunk( uint vertexcount, uint indexcount )
		{
			Vertices = new VertexT2dN3dV3d[vertexcount];
			Indices = new uint[indexcount];
		}

		public Chunk( ref VertexT2dN3dV3d[] vbo, ref uint[] ibo )
		{
			Vertices = new VertexT2dN3dV3d[vbo.Length];
			for ( int i = 0; i < Vertices.Length; i++ )
			{
				Vertices[i] = vbo[i];
			} 
			Indices = new uint[ibo.Length];
			for ( int i = 0; i < Indices.Length; i++ )
			{
				Indices[i] = ibo[i];
			}
		}

		public static void GetArray( ref List<Chunk> c, out VertexT2dN3dV3d[] vbo, out uint[] ibo )
		{

			uint VertexCounter = 0;
			uint IndexCounter = 0;

			foreach ( Chunk ch in c )
			{
				VertexCounter += ch.VertexCount;
				IndexCounter += ch.IndexCount;
			}

			vbo = new VertexT2dN3dV3d[VertexCounter];
			ibo = new uint[IndexCounter];

			VertexCounter = 0;
			IndexCounter = 0;

			foreach ( Chunk ch in c )
			{
				for ( int i = 0; i < ch.Vertices.Length; i++ )
				{
					vbo[VertexCounter + i] = ch.Vertices[i];
				}

				for ( int i = 0; i < ch.Indices.Length; i++ )
				{
					ibo[IndexCounter + i] = ch.Indices[i] + VertexCounter;
				}

				VertexCounter += (uint)ch.VertexCount;
				IndexCounter += (uint)ch.IndexCount;
			}
		}
	}

}

