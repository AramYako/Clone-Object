namespace CloneObjectLibrary
{
    public interface ICloneClass
    {
        T DeepClone<T>(T source);
        T ShallowClone<T>(T source);
    }
}