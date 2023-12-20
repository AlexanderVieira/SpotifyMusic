using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVS.SpotifyMusic.Infra.Data.Migrations
{
    public partial class DatabaseInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bandas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    Foto = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DateTime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bandas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValorPagamento = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DateTime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "varchar(254)", nullable: false),
                    Cpf = table.Column<string>(type: "varchar(11)", nullable: false),
                    Senha = table.Column<string>(type: "varchar(150)", nullable: false),
                    Foto = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    Ativo = table.Column<bool>(type: "Bit", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DateTime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Albuns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    Foto = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    BandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DateTime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albuns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albuns_Bandas_BandaId",
                        column: x => x.BandaId,
                        principalTable: "Bandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assinaturas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ativo = table.Column<bool>(type: "Bit", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DateTime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assinaturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assinaturas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cartoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<string>(type: "varchar(19)", maxLength: 19, nullable: false),
                    Nome = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    DataExpiracao = table.Column<string>(type: "varchar(7)", maxLength: 7, nullable: false),
                    Cvv = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    Ativo = table.Column<bool>(type: "Bit", nullable: false),
                    Limite = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PagamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DateTime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cartoes_Pagamentos_PagamentoId",
                        column: x => x.PagamentoId,
                        principalTable: "Pagamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cartoes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notificacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Mensagem = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    DestinoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RemetenteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TipoNotificacao = table.Column<int>(type: "int", nullable: false),
                    DataNotificacao = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DateTime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificacoes_Usuarios_DestinoId",
                        column: x => x.DestinoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notificacoes_Usuarios_RemetenteId",
                        column: x => x.RemetenteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    Foto = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    Publica = table.Column<bool>(type: "Bit", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DateTime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Playlists_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Musicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Duracao = table.Column<int>(type: "int", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DateTime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Musicas_Albuns_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albuns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Planos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    ValorPlano = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TipoPlano = table.Column<int>(type: "int", nullable: false),
                    AssinaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DateTime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planos_Assinaturas_AssinaturaId",
                        column: x => x.AssinaturaId,
                        principalTable: "Assinaturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    NomeMerchant = table.Column<string>(type: "varchar(50)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    PagamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataTransacao = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DateTime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacoes_Cartoes_CartaoId",
                        column: x => x.CartaoId,
                        principalTable: "Cartoes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transacoes_Pagamentos_PagamentoId",
                        column: x => x.PagamentoId,
                        principalTable: "Pagamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicaPlaylist",
                columns: table => new
                {
                    MusicasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicaPlaylist", x => new { x.MusicasId, x.PlaylistsId });
                    table.ForeignKey(
                        name: "FK_MusicaPlaylist_Musicas_MusicasId",
                        column: x => x.MusicasId,
                        principalTable: "Musicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicaPlaylist_Playlists_PlaylistsId",
                        column: x => x.PlaylistsId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albuns_BandaId",
                table: "Albuns",
                column: "BandaId");

            migrationBuilder.CreateIndex(
                name: "IX_Assinaturas_UsuarioId",
                table: "Assinaturas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Cartoes_PagamentoId",
                table: "Cartoes",
                column: "PagamentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cartoes_UsuarioId",
                table: "Cartoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicaPlaylist_PlaylistsId",
                table: "MusicaPlaylist",
                column: "PlaylistsId");

            migrationBuilder.CreateIndex(
                name: "IX_Musicas_AlbumId",
                table: "Musicas",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacoes_DestinoId",
                table: "Notificacoes",
                column: "DestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacoes_RemetenteId",
                table: "Notificacoes",
                column: "RemetenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Planos_AssinaturaId",
                table: "Planos",
                column: "AssinaturaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_UsuarioId",
                table: "Playlists",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_CartaoId",
                table: "Transacoes",
                column: "CartaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_PagamentoId",
                table: "Transacoes",
                column: "PagamentoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicaPlaylist");

            migrationBuilder.DropTable(
                name: "Notificacoes");

            migrationBuilder.DropTable(
                name: "Planos");

            migrationBuilder.DropTable(
                name: "Transacoes");

            migrationBuilder.DropTable(
                name: "Musicas");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Assinaturas");

            migrationBuilder.DropTable(
                name: "Cartoes");

            migrationBuilder.DropTable(
                name: "Albuns");

            migrationBuilder.DropTable(
                name: "Pagamentos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Bandas");
        }
    }
}
