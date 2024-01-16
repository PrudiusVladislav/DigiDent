using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiDent.EFCorePersistence.Migrations.ClinicCoreDb
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Clinic_Core");

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "Clinic_Core",
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
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                schema: "Clinic_Core",
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
                name: "SchedulePreferences",
                schema: "Clinic_Core",
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
                        name: "FK_SchedulePreferences_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Clinic_Core",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingDays",
                schema: "Clinic_Core",
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
                        name: "FK_WorkingDays_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Clinic_Core",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentPlans",
                schema: "Clinic_Core",
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
                        principalSchema: "Clinic_Core",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                schema: "Clinic_Core",
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
                        name: "FK_Appointments_Employee_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Clinic_Core",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Clinic_Core",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_TreatmentPlans_TreatmentPlanId",
                        column: x => x.TreatmentPlanId,
                        principalSchema: "Clinic_Core",
                        principalTable: "TreatmentPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PastVisits",
                schema: "Clinic_Core",
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
                        name: "FK_PastVisits_Employee_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Clinic_Core",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PastVisits_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Clinic_Core",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PastVisits_TreatmentPlans_TreatmentPlanId",
                        column: x => x.TreatmentPlanId,
                        principalSchema: "Clinic_Core",
                        principalTable: "TreatmentPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DentalProcedures",
                schema: "Clinic_Core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsualDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DentalProcedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DentalProcedures_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalSchema: "Clinic_Core",
                        principalTable: "Appointments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DoctorsProvidedServices",
                schema: "Clinic_Core",
                columns: table => new
                {
                    DoctorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProvidedServicesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsProvidedServices", x => new { x.DoctorsId, x.ProvidedServicesId });
                    table.ForeignKey(
                        name: "FK_DoctorsProvidedServices_DentalProcedures_ProvidedServicesId",
                        column: x => x.ProvidedServicesId,
                        principalSchema: "Clinic_Core",
                        principalTable: "DentalProcedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorsProvidedServices_Employee_DoctorsId",
                        column: x => x.DoctorsId,
                        principalSchema: "Clinic_Core",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                schema: "Clinic_Core",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                schema: "Clinic_Core",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TreatmentPlanId",
                schema: "Clinic_Core",
                table: "Appointments",
                column: "TreatmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DentalProcedures_AppointmentId",
                schema: "Clinic_Core",
                table: "DentalProcedures",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsProvidedServices_ProvidedServicesId",
                schema: "Clinic_Core",
                table: "DoctorsProvidedServices",
                column: "ProvidedServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_PastVisits_DoctorId",
                schema: "Clinic_Core",
                table: "PastVisits",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PastVisits_PatientId",
                schema: "Clinic_Core",
                table: "PastVisits",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PastVisits_TreatmentPlanId",
                schema: "Clinic_Core",
                table: "PastVisits",
                column: "TreatmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulePreferences_EmployeeId",
                schema: "Clinic_Core",
                table: "SchedulePreferences",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentPlans_PatientId",
                schema: "Clinic_Core",
                table: "TreatmentPlans",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDays_EmployeeId",
                schema: "Clinic_Core",
                table: "WorkingDays",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorsProvidedServices",
                schema: "Clinic_Core");

            migrationBuilder.DropTable(
                name: "PastVisits",
                schema: "Clinic_Core");

            migrationBuilder.DropTable(
                name: "SchedulePreferences",
                schema: "Clinic_Core");

            migrationBuilder.DropTable(
                name: "WorkingDays",
                schema: "Clinic_Core");

            migrationBuilder.DropTable(
                name: "DentalProcedures",
                schema: "Clinic_Core");

            migrationBuilder.DropTable(
                name: "Appointments",
                schema: "Clinic_Core");

            migrationBuilder.DropTable(
                name: "Employee",
                schema: "Clinic_Core");

            migrationBuilder.DropTable(
                name: "TreatmentPlans",
                schema: "Clinic_Core");

            migrationBuilder.DropTable(
                name: "Patients",
                schema: "Clinic_Core");
        }
    }
}
