namespace Eatm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionModelAdded1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.Transactions", new[] { "Account_Id" });
            RenameColumn(table: "dbo.Transactions", name: "Account_Id", newName: "AccountId");
            AlterColumn("dbo.Transactions", "AccountId", c => c.Int(nullable: false));
            CreateIndex("dbo.Transactions", "AccountId");
            AddForeignKey("dbo.Transactions", "AccountId", "dbo.Accounts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Transactions", new[] { "AccountId" });
            AlterColumn("dbo.Transactions", "AccountId", c => c.Int());
            RenameColumn(table: "dbo.Transactions", name: "AccountId", newName: "Account_Id");
            CreateIndex("dbo.Transactions", "Account_Id");
            AddForeignKey("dbo.Transactions", "Account_Id", "dbo.Accounts", "Id");
        }
    }
}
