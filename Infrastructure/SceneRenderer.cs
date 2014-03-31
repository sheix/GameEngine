using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


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

			GL.ClearColor(System.Drawing.Color.Black);
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

			//render scene

			if (Keyboard[Key.Escape])
				Exit();
		}
	}
}

