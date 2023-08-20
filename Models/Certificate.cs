namespace EMS.Models;

public class Certificate
{
    public int Id { get; set; }
    public string CertificateName { get; set; }
    public string Description { get; set; }

    public string ApplicationUserId { get; set; } // User who achieved the certificate
    public virtual ApplicationUser ApplicationUser { get; set; }
}
