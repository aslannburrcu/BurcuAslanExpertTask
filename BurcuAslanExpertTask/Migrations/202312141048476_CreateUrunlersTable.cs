namespace BurcuAslanExpertTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUrunlersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kategoris",
                c => new
                    {
                        KategoriId = c.Int(nullable: false, identity: true),
                        KategoriAdi = c.String(),
                    })
                .PrimaryKey(t => t.KategoriId);
            
            CreateTable(
                "dbo.Urunlers",
                c => new
                    {
                        UrunId = c.Int(nullable: false, identity: true),
                        UrunAdi = c.String(),
                        UrunMarka = c.String(),
                        UrunKategori = c.String(),
                        UrunStok = c.Int(nullable: false),
                        Aciklama = c.String(),
                        Kategori_KategoriId = c.Int(),
                    })
                .PrimaryKey(t => t.UrunId)
                .ForeignKey("dbo.Kategoris", t => t.Kategori_KategoriId)
                .Index(t => t.Kategori_KategoriId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Urunlers", "Kategori_KategoriId", "dbo.Kategoris");
            DropIndex("dbo.Urunlers", new[] { "Kategori_KategoriId" });
            DropTable("dbo.Urunlers");
            DropTable("dbo.Kategoris");
        }
    }
}
