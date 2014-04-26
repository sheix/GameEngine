using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;


namespace Infrastructure
{
	public class SceneRenderer : GameWindow
	{

		int[] vbo = new int[2];
		Vector3[] vertices;
		Random _random;
		int frame = 0;

		public SceneRenderer ()
			: base(800, 600, GraphicsMode.Default, "GameEngine v0.01", GameWindowFlags.Default, DisplayDevice.Default, 2, 1, GraphicsContextFlags.Default)
		{
			VSync = VSyncMode.On;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			GL.ClearColor(Color.Brown);
			GL.Enable(EnableCap.DepthTest);
			CreateVertexBuffer();
			_random = new Random ();
		}

		protected override void OnUnload( EventArgs e )
		{

			base.OnUnload( e );
			VSync = VSyncMode.On;

			//	Object.Dispose();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			GL.Viewport(ClientRectangle);
		}

		void CreateVertexBuffer()
		{
			vertices = new Vector3[3];
			vertices[0] = new Vector3(-0.5f, -0.5f, 0f);
			vertices[1] = new Vector3( 1f, -0.5f, 0f);
			vertices[2] = new Vector3( 0f,  1f, 0f);
			GL.GenBuffers(2, vbo);
			currentBuffer = vbo [0];


		}

		int currentBuffer;
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);
			Console.WriteLine ("frame = {0}", frame++);
			//GL.Clear(ClearBufferMask.ColorBufferBit);
			if (currentBuffer == vbo [0])
				currentBuffer = vbo [1];
			else
				currentBuffer = vbo [1];

			GL.BufferData<Vector3>(BufferTarget.ArrayBuffer,
				new IntPtr(vertices.Length * Vector3.SizeInBytes),
				vertices, BufferUsageHint.StreamDraw);
			GL.BindBuffer(BufferTarget.ArrayBuffer, currentBuffer);
			GL.EnableVertexAttribArray(0);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
			GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
			//GL.Rotate(1, new Vector3(0.5f, 0f,0f));
			foreach (var v in vertices) {
				//v [0] += (float) _random.NextDouble () - 0.5f / 10;
				//v [1] += (float) _random.NextDouble () - 0.5f / 10;
				//v [2] += (float) _random.NextDouble () - 0.5f / 10;
				Console.WriteLine (v);
			}

			GL.DisableVertexAttribArray(0);
			SwapBuffers ();

			if (Keyboard[Key.Escape])
				Exit();
		}

	}
}

