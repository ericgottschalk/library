namespace Library.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.author",
                c => new
                {
                    id = c.Long(nullable: false, identity: true),
                    name = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    is_active = c.Boolean(nullable: false),
                    created_at = c.DateTime(nullable: false, precision: 0),
                    updated_at = c.DateTime(precision: 0),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.book",
                c => new
                {
                    id = c.Long(nullable: false, identity: true),
                    title = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    summary = c.String(nullable: false, maxLength: 4096, storeType: "nvarchar"),
                    isbn = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    language = c.Int(nullable: false),
                    publication_date = c.DateTime(nullable: false, precision: 0),
                    author_id = c.Long(nullable: false),
                    publisher_id = c.Long(nullable: false),
                    is_active = c.Boolean(nullable: false),
                    created_at = c.DateTime(nullable: false, precision: 0),
                    updated_at = c.DateTime(precision: 0),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.author", t => t.author_id, cascadeDelete: true)
                .ForeignKey("dbo.publisher", t => t.publisher_id, cascadeDelete: true);

            Sql("CREATE index  `IX_title` on `book` (`title`) using HASH");
            Sql("CREATE index  `IX_author_id` on `book` (`author_id`) using HASH");
            Sql("CREATE index  `IX_publisher_id` on `book` (`publisher_id`) using HASH");

            CreateTable(
                "dbo.publisher",
                c => new
                {
                    id = c.Long(nullable: false, identity: true),
                    name = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    is_active = c.Boolean(nullable: false),
                    created_at = c.DateTime(nullable: false, precision: 0),
                    updated_at = c.DateTime(precision: 0),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.rental",
                c => new
                {
                    id = c.Long(nullable: false, identity: true),
                    book_id = c.Long(nullable: false),
                    member_id = c.Long(nullable: false),
                    return_date = c.DateTime(precision: 0),
                    is_active = c.Boolean(nullable: false),
                    created_at = c.DateTime(nullable: false, precision: 0),
                    updated_at = c.DateTime(precision: 0),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.book", t => t.book_id, cascadeDelete: true)
                .ForeignKey("dbo.member", t => t.member_id, cascadeDelete: true);

            Sql("CREATE index  `IX_book_id` on `rental` (`book_id`) using HASH");
            Sql("CREATE index  `IX_member_id` on `rental` (`member_id`) using HASH");

            CreateTable(
                "dbo.member",
                c => new
                {
                    id = c.Long(nullable: false, identity: true),
                    email = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    password = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                    name = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    is_active = c.Boolean(nullable: false),
                    created_at = c.DateTime(nullable: false, precision: 0),
                    updated_at = c.DateTime(precision: 0),
                })
                .PrimaryKey(t => t.id);

            Sql("CREATE UNIQUE index  `IX_email` on `member` (`email`) using HASH");
        }

        public override void Down()
        {
            DropForeignKey("dbo.rental", "member_id", "dbo.member");
            DropForeignKey("dbo.rental", "book_id", "dbo.book");
            DropForeignKey("dbo.book", "publisher_id", "dbo.publisher");
            DropForeignKey("dbo.book", "author_id", "dbo.author");
            DropIndex("dbo.member", new[] { "email" });
            DropIndex("dbo.rental", new[] { "member_id" });
            DropIndex("dbo.rental", new[] { "book_id" });
            DropIndex("dbo.book", new[] { "publisher_id" });
            DropIndex("dbo.book", new[] { "author_id" });
            DropIndex("dbo.book", new[] { "title" });
            DropTable("dbo.member");
            DropTable("dbo.rental");
            DropTable("dbo.publisher");
            DropTable("dbo.book");
            DropTable("dbo.author");
        }
    }
}
