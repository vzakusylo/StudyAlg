namespace moq_collections_instarct_iterface
{
    public interface IMyArray
    {
        int Lenght { get; }

        void Append(int value);
        int GetElementAt(int index);
    }
}