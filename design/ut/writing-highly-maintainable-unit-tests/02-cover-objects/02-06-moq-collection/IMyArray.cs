namespace moq_collections
{
    public interface IMyArray
    {
        int Lenght { get; }

        void Append(int value);
        int GetElementAt(int index);
    }
}