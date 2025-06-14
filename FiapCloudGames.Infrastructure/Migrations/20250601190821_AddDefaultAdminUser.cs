﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiapCloudGames.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "PasswordHash", "Role", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2025, 6, 1, 19, 8, 20, 814, DateTimeKind.Utc).AddTicks(3791), "admin@fiap.com.br", "Admin", "$2a$11$5UDUbDUZdohhBRUSVdBK4OJTBNQPxZv1g0czKDT5SyHiDrx3od0ie", "Admin", new DateTime(2025, 6, 1, 19, 8, 20, 814, DateTimeKind.Utc).AddTicks(3792) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
