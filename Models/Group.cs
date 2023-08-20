using System.Collections.Generic;
namespace EMS.Models
{
public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } // Name of the group, e.g., "Class of 2023"

    // Navigation properties
    public virtual ICollection<ApplicationUser> Students { get; set; }
    public virtual ICollection<ExamGroup> ExamGroups { get; set; }
 }
}