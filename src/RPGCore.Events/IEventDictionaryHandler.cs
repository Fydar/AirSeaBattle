namespace RPGCore.Events
{
	public interface IEventDictionaryHandler<TKey, TValue>
	{
		void OnAdd(TKey key, TValue value);

		void OnRemove(TKey key, TValue value);
	}
}
