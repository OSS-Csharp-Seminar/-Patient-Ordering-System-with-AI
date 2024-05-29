public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Specializations",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Specializations", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Patients",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(nullable: true),
                Surname = table.Column<string>(nullable: true),
                Password = table.Column<string>(nullable: true),
                Contact = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Patients", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Doctors",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(nullable: true),
                Surname = table.Column<string>(nullable: true),
                Password = table.Column<string>(nullable: true),
                SpecializationId = table.Column<int>(nullable: false),
                Contact = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Doctors", x => x.Id);
                table.ForeignKey(
                    name: "FK_Doctors_Specializations_SpecializationId",
                    column: x => x.SpecializationId,
                    principalTable: "Specializations",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Orders",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                DoctorId = table.Column<int>(nullable: false),
                PatientId = table.Column<int>(nullable: false),
                DateOfAppointment = table.Column<DateTime>(nullable: false),
                Diagnosis = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Orders", x => x.Id);
                table.ForeignKey(
                    name: "FK_Orders_Doctors_DoctorId",
                    column: x => x.DoctorId,
                    principalTable: "Doctors",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Orders_Patients_PatientId",
                    column: x => x.PatientId,
                    principalTable: "Patients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Doctors_SpecializationId",
            table: "Doctors",
            column: "SpecializationId");

        migrationBuilder.CreateIndex(
            name: "IX_Orders_DoctorId",
            table: "Orders",
            column: "DoctorId");

        migrationBuilder.CreateIndex(
            name: "IX_Orders_PatientId",
            table: "Orders",
            column: "PatientId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Orders");

        migrationBuilder.DropTable(
            name: "Doctors");

        migrationBuilder.DropTable(
            name: "Patients");

        migrationBuilder.DropTable(
            name: "Specializations");
    }
}
