namespace Nucleus.ObjectMapping
{
    public interface IMapFrom<in TSource>
    {
        void MapFrom(TSource source);
    }
}
