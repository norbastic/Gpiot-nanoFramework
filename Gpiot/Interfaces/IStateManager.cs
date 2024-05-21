namespace Gpiot.Interfaces
{
    public interface IStateManager
    {
        string GetState(string key);
        void SetState(string key, string value);
    }
}