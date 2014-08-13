namespace Contracts
{
    public interface IRenderable
    {
		//void Render();
		void RenderMessage (string message);
		void RenderCalendar (ICalendar calendar);
		void RenderScene(IScene scene);
    }
}