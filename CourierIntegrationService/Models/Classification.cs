using System.ComponentModel.DataAnnotations;
namespace CourierIntegrationService.Models;

public class Classification
{
    [Key]
    public int ClassificationId { get; set; }

    [Required]
    required public string ClassificationName { get; set; }
}


