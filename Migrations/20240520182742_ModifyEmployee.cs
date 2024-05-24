using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProj.Migrations
{
    /// <inheritdoc />
    public partial class ModifyEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "EmpolyeesTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNumber",
                table: "EmpolyeesTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "EmpolyeesTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "JoiningDate",
                table: "EmpolyeesTable",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "EmpolyeesTable",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "EmpolyeesTable");

            migrationBuilder.DropColumn(
                name: "EmployeeNumber",
                table: "EmpolyeesTable");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "EmpolyeesTable");

            migrationBuilder.DropColumn(
                name: "JoiningDate",
                table: "EmpolyeesTable");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "EmpolyeesTable");
        }
    }
}
