namespace CodeTestUnity
{
	public interface IRenderer<TTarget>
	{
		TTarget RenderTarget { get; set; }
	}
}
