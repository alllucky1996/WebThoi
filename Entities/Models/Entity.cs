namespace Entities.Models
{
    public interface Entity
    {
        //[NotMapped]
        //public virtual System.Guid Guid { get; set; }
        string Describe();
    }
}