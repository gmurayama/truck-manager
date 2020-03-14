namespace TruckManager.Application.Features
{ 
    public interface IHandler<T1, T2>
    {
        T2 Handle(T1 input);
    }
}