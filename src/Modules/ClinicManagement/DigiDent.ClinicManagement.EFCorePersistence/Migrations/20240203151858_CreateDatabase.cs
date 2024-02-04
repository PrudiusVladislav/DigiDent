using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiDent.ClinicManagement.EFCorePersistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "clinic_management");

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "clinic_management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(name: "Full Name", type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeType = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                schema: "clinic_management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(name: "Full Name", type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProvidedServices",
                schema: "clinic_management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsualDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvidedServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchedulePreferences",
                schema: "clinic_management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StartEndTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSetAsDayOff = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulePreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchedulePreferences_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "clinic_management",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingDays",
                schema: "clinic_management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StartEndTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingDays_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "clinic_management",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentPlans",
                schema: "clinic_management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfStart = table.Column<DateOnly>(type: "date", nullable: false),
                    DateOfFinish = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentPlans_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "clinic_management",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorsProvidedServices",
                schema: "clinic_management",
                columns: table => new
                {
                    DoctorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProvidedServicesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsProvidedServices", x => new { x.DoctorsId, x.ProvidedServicesId });
                    table.ForeignKey(
                        name: "FK_DoctorsProvidedServices_Employees_DoctorsId",
                        column: x => x.DoctorsId,
                        principalSchema: "clinic_management",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorsProvidedServices_ProvidedServices_ProvidedServicesId",
                        column: x => x.ProvidedServicesId,
                        principalSchema: "clinic_management",
                        principalTable: "ProvidedServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                schema: "clinic_management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    TreatmentPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Employees_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "clinic_management",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "clinic_management",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_TreatmentPlans_TreatmentPlanId",
                        column: x => x.TreatmentPlanId,
                        principalSchema: "clinic_management",
                        principalTable: "TreatmentPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PastVisits",
                schema: "clinic_management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreatmentPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VisitDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PricePaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Feedback_Rating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Feedback_Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ProceduresDone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PastVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PastVisits_Employees_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "clinic_management",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PastVisits_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "clinic_management",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PastVisits_TreatmentPlans_TreatmentPlanId",
                        column: x => x.TreatmentPlanId,
                        principalSchema: "clinic_management",
                        principalTable: "TreatmentPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentsProvidedServices",
                schema: "clinic_management",
                columns: table => new
                {
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProvidedServicesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentsProvidedServices", x => new { x.AppointmentId, x.ProvidedServicesId });
                    table.ForeignKey(
                        name: "FK_AppointmentsProvidedServices_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalSchema: "clinic_management",
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentsProvidedServices_ProvidedServices_ProvidedServicesId",
                        column: x => x.ProvidedServicesId,
                        principalSchema: "clinic_management",
                        principalTable: "ProvidedServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                schema: "clinic_management",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                schema: "clinic_management",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TreatmentPlanId",
                schema: "clinic_management",
                table: "Appointments",
                column: "TreatmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsProvidedServices_ProvidedServicesId",
                schema: "clinic_management",
                table: "AppointmentsProvidedServices",
                column: "ProvidedServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsProvidedServices_ProvidedServicesId",
                schema: "clinic_management",
                table: "DoctorsProvidedServices",
                column: "ProvidedServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_PastVisits_DoctorId",
                schema: "clinic_management",
                table: "PastVisits",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PastVisits_PatientId",
                schema: "clinic_management",
                table: "PastVisits",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PastVisits_TreatmentPlanId",
                schema: "clinic_management",
                table: "PastVisits",
                column: "TreatmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulePreferences_EmployeeId",
                schema: "clinic_management",
                table: "SchedulePreferences",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentPlans_PatientId",
                schema: "clinic_management",
                table: "TreatmentPlans",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDays_EmployeeId",
                schema: "clinic_management",
                table: "WorkingDays",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentsProvidedServices",
                schema: "clinic_management");

            migrationBuilder.DropTable(
                name: "DoctorsProvidedServices",
                schema: "clinic_management");

            migrationBuilder.DropTable(
                name: "PastVisits",
                schema: "clinic_management");

            migrationBuilder.DropTable(
                name: "SchedulePreferences",
                schema: "clinic_management");

            migrationBuilder.DropTable(
                name: "WorkingDays",
                schema: "clinic_management");

            migrationBuilder.DropTable(
                name: "Appointments",
                schema: "clinic_management");

            migrationBuilder.DropTable(
                name: "ProvidedServices",
                schema: "clinic_management");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "clinic_management");

            migrationBuilder.DropTable(
                name: "TreatmentPlans",
                schema: "clinic_management");

            migrationBuilder.DropTable(
                name: "Patients",
                schema: "clinic_management");
        }
    }
}
