using System.Threading.Tasks;

namespace Nucleus.SimpleStateChecking
{
    public interface ISimpleStateCheckerManager<TState>
        where TState : IHasSimpleStateCheckers<TState>
    {
        Task<bool> IsEnabledAsync(TState state);

        Task<SimpleStateCheckerResult<TState>> IsEnabledAsync(TState[] states);
    }
}

