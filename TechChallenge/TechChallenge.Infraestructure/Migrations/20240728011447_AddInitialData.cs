using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechChallenge.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Contato]
                                        ([Nome]
                                        ,[Telefone]
                                        ,[EMail]
                                        ,[DDD]
                                        ,[DataCriacao])
                                    VALUES
                                        ('Joao'
                                        ,'999999999'
                                        ,'joao@teste.com'
                                        ,16
                                        ,GETDATE()),
                                        ('Maria'
                                        ,'999999991'
                                        ,'maria@teste.com'
                                        ,11
                                        ,GETDATE())");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE [dbo].[Contato]");
        }
    }
}
