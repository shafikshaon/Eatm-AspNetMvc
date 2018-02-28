namespace Eatm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountTableSeed : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Accounts VALUES (111, 1111, 1000)");
            Sql("INSERT INTO Accounts VALUES (222, 2222, 5000)");
            Sql("INSERT INTO Accounts VALUES (333, 3333, 4500)");
            Sql("INSERT INTO Accounts VALUES (444, 4444, 500)");
        }
        
        public override void Down()
        {
        }
    }
}
