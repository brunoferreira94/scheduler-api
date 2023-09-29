namespace Scheduler.Domain.Entities
{
    public abstract class Entity
    {
        #region Protected Constructors

        protected Entity()
        {
            CreatedDate = DateTime.Now;
            IsDeleted = false;
        }

        #endregion Protected Constructors

        #region Public Properties

        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        #endregion Public Properties
    }
}