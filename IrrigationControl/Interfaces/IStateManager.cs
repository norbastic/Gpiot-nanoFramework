namespace IrrigationControl.Interfaces
{
    public interface IStateManager
    {
        string GetState(string key);
        void SetState(string key, string value);
    }
}