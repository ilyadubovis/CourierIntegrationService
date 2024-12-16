using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourierIntegrationService.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StateCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Classification",
                columns: table => new
                {
                    ClassificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassificationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classification", x => x.ClassificationId);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentStatus",
                columns: table => new
                {
                    ShipmentStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipmentStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentStatus", x => x.ShipmentStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Receiver",
                columns: table => new
                {
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receiver", x => x.ReceiverId);
                    table.ForeignKey(
                        name: "FK_Receiver_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipper",
                columns: table => new
                {
                    ShipperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipper", x => x.ShipperId);
                    table.ForeignKey(
                        name: "FK_Shipper_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    ShipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipmentStatusId = table.Column<int>(type: "int", nullable: false),
                    ClassificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassificationIdId = table.Column<int>(type: "int", nullable: true),
                    ShipperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstimatedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.ShipmentId);
                    table.ForeignKey(
                        name: "FK_Shipments_Classification_ClassificationIdId",
                        column: x => x.ClassificationIdId,
                        principalTable: "Classification",
                        principalColumn: "ClassificationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipments_Receiver_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Receiver",
                        principalColumn: "ReceiverId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipments_ShipmentStatus_ShipmentStatusId",
                        column: x => x.ShipmentStatusId,
                        principalTable: "ShipmentStatus",
                        principalColumn: "ShipmentStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipments_Shipper_ShipperId",
                        column: x => x.ShipperId,
                        principalTable: "Shipper",
                        principalColumn: "ShipperId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceAreaCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceAreaDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "ShipmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ShipmentStatus",
                columns: new[] { "ShipmentStatusId", "ShipmentStatusName" },
                values: new object[,]
                {
                    { 1, "Unknown" },
                    { 2, "LabelCreated" },
                    { 3, "OutForDelivery" },
                    { 4, "Shipped" },
                    { 5, "AtLocalFacility" },
                    { 6, "Delivered" },
                    { 7, "Delayed" },
                    { 8, "Cancelled" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_ShipmentId",
                table: "Event",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Receiver_AddressId",
                table: "Receiver",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ClassificationIdId",
                table: "Shipments",
                column: "ClassificationIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ReceiverId",
                table: "Shipments",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ShipmentStatusId",
                table: "Shipments",
                column: "ShipmentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ShipperId",
                table: "Shipments",
                column: "ShipperId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipper_AddressId",
                table: "Shipper",
                column: "AddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "Classification");

            migrationBuilder.DropTable(
                name: "Receiver");

            migrationBuilder.DropTable(
                name: "ShipmentStatus");

            migrationBuilder.DropTable(
                name: "Shipper");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
