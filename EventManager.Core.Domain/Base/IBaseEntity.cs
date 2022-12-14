namespace EventManager.Core.Domain.Base
{
    public interface IBaseEntity
    {
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
