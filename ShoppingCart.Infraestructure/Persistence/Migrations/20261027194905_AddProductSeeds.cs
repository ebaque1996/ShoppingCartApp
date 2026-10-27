using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoppingCart.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProductSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "RGB mechanical keyboard with red switches", "Mechanical Keyboard", 75.50m, 20 },
                    { 2, "Ergonomic mouse with high precision sensor", "Wireless Gaming Mouse", 45.00m, 35 },
                    { 3, "IPS Full HD monitor for productivity and gaming", "UltraWide Monitor 29\"", 299.99m, 10 },
                    { 4, "Multiport adapter with HDMI, USB 3.0, and SD card reader", "USB-C Hub 7-in-1", 29.99m, 50 },
                    { 5, "Over-ear wireless headphones with deep bass", "Noise-Canceling Headphones", 120.00m, 15 },
                    { 6, "Adjustable aluminum stand for laptops up to 17 inches", "Laptop Stand", 34.99m, 40 },
                    { 7, "High-speed portable solid state drive", "External SSD 1TB", 99.99m, 25 },
                    { 8, "Full HD webcam with built-in microphone for streaming", "WebCam 1080p", 49.99m, 30 },
                    { 9, "Mesh chair with lumbar support and adjustable armrests", "Ergonomic Office Chair", 189.99m, 8 },
                    { 10, "Dimmable desk lamp with wireless charging base", "Desk LED Lamp", 39.99m, 22 },
                    { 11, "Waterproof portable speaker with 12-hour battery life", "Bluetooth Speaker", 59.99m, 28 },
                    { 12, "Heart rate monitor, sleep tracker, and GPS", "Smartwatch Fitness Tracker", 89.99m, 18 },
                    { 13, "Fast Qi-certified wireless charger for smartphones", "Wireless Charging Pad", 22.50m, 60 },
                    { 14, "Ergonomic vertical mouse to prevent wrist strain", "Vertical Mouse", 32.99m, 25 },
                    { 15, "High-speed memory card with adapter", "MicroSD Card 128GB", 18.99m, 75 },
                    { 16, "4K@120Hz high-speed braided cable 2m", "HDMI 2.1 Cable", 15.00m, 100 },
                    { 17, "Fast-charging external battery pack", "Portable Power Bank 20000mAh", 45.50m, 30 },
                    { 18, "Large waterproof desk pad for keyboard and mouse", "Desk Mat Extended", 19.99m, 50 },
                    { 19, "LED ring light with tripod stand for content creators", "Ring Light 10\"", 35.00m, 20 },
                    { 20, "Snagless patch cable 5 meters", "Ethernet Cable Cat6", 9.99m, 120 },
                    { 21, "High-speed USB 3.0 storage drive", "USB Flash Drive 64GB", 12.50m, 90 },
                    { 22, "USB microphone for podcasting, gaming, and recording", "Condenser Microphone", 69.99m, 14 },
                    { 23, "Drawing tablet with stylus for digital art and design", "Graphics Tablet", 79.99m, 12 },
                    { 24, "Voice-controlled smart outlet compatible with Alexa and Google", "Smart Plug Wi-Fi", 16.99m, 45 },
                    { 25, "Organize power strips and messy cords cleanly", "Cable Management Box", 14.99m, 55 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);
        }
    }
}
