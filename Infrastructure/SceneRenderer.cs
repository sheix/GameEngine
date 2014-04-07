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
		public SceneRenderer ()
			: base(800, 600, GraphicsMode.Default, "GameEngine v0.01", GameWindowFlags.Default, DisplayDevice.Default, 2, 1, GraphicsContextFlags.Default)
		{
			VSync = VSyncMode.On;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			GL.ClearColor(Color.Black);
			GL.Enable(EnableCap.DepthTest);

			GL.Enable( EnableCap.Lighting );
			GL.Enable( EnableCap.Light0 );



		}

		protected override void OnUnload( EventArgs e )
		{

			base.OnUnload( e );
			//	Object.Dispose();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			GL.Viewport(ClientRectangle);
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);

			GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

			GL.Begin(BeginMode.Quads);

			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.LoadIdentity ();
			//render scene
			GL.Color3(Color.White);
			GL.Vertex2(0.5, 0.5);
			GL.Vertex2(0.6, 0.5);
			GL.Vertex2(0.6, 0.6);
			GL.Vertex2(0.5, 0.6);

			GL.End ();
			if (Keyboard[Key.Escape])
				Exit();
		}
	}
}

