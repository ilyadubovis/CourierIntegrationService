using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CourierIntegrationService.Models
{
    public class Shipment
    {
        [Key]
        public Guid ShipmentId { get; set; }

        [Required]
        public required string TrackingNumber { get; set; }

        [Required]
        public required string CourierName { get; set; }

        [Required]
        public int ShipmentStatusId { get; set; }

        [ForeignKey("ShipmentStatusId")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public ShipmentStatus? ShipmentStatus { get; set; }

        [Required]
        public Guid ShipperId { get; set; }

        [ForeignKey("ShipperId")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Shipper? Shipper { get; set; }

        [Required]
        public Guid ReceiverId { get; set; }

        [ForeignKey("ReceiverId")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Receiver? Receiver { get; set; }

        public ICollection<Event> Events { get; set; } = [];
    }
}
