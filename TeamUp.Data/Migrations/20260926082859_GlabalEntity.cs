using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamUpData.Migrations
{
    /// <inheritdoc />
    public partial class GlabalEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImgUrl",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATEDAT",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CREATEDBY",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DELETEDAT",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DELETEDBY",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ISDELETED",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UPDATEDAT",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UPDATEDBY",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATEDAT",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CREATEDBY",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DELETEDAT",
                table: "Roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DELETEDBY",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ISDELETED",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UPDATEDAT",
                table: "Roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UPDATEDBY",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATEDAT",
                table: "Registrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CREATEDBY",
                table: "Registrations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DELETEDAT",
                table: "Registrations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DELETEDBY",
                table: "Registrations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ISDELETED",
                table: "Registrations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UPDATEDAT",
                table: "Registrations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UPDATEDBY",
                table: "Registrations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATEDAT",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CREATEDBY",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DELETEDAT",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DELETEDBY",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ISDELETED",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UPDATEDAT",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UPDATEDBY",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATEDAT",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CREATEDBY",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DELETEDAT",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DELETEDBY",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ISDELETED",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UPDATEDAT",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UPDATEDBY",
                table: "Categories",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CREATEDAT",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CREATEDBY",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DELETEDAT",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DELETEDBY",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ISDELETED",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UPDATEDAT",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UPDATEDBY",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CREATEDAT",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CREATEDBY",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DELETEDAT",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DELETEDBY",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ISDELETED",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UPDATEDAT",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UPDATEDBY",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CREATEDAT",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "CREATEDBY",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "DELETEDAT",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "DELETEDBY",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "ISDELETED",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "UPDATEDAT",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "UPDATEDBY",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "CREATEDAT",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CREATEDBY",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DELETEDAT",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DELETEDBY",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ISDELETED",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UPDATEDAT",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UPDATEDBY",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CREATEDAT",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CREATEDBY",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DELETEDAT",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DELETEDBY",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ISDELETED",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UPDATEDAT",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UPDATEDBY",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "ImgUrl",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
