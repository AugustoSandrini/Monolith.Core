using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSnapshots",
                columns: table => new
                {
                    AggregateId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Version = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Aggregate = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSnapshots", x => new { x.Version, x.AggregateId });
                });

            migrationBuilder.CreateTable(
                name: "UserStoreEvents",
                columns: table => new
                {
                    AggregateId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Version = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    EventType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Event = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStoreEvents", x => new { x.Version, x.AggregateId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSnapshots");

            migrationBuilder.DropTable(
                name: "UserStoreEvents");
        }
    }
}
