using System.IO;

namespace SnakeGame.Classes
{
    public static class GerenciadorArquivoIni
    {
        private static ArquivoIni ini = new ArquivoIni("Cfg.ini");

        public static void CriarSeNaoExistir()
        {
            if (!File.Exists(ini.Path))
            {
                ini.Write("pontosmax", "0", "SnakeGame");
                ini.Write("colidir", "0", "SnakeGame");
            }
        }

        public static void Salvar(int pontos, Tabuleiro.ColidirComAsBordas colidir)
        {
            if (PontuacaoMaxima(pontos))
                ini.Write("pontosmax", pontos.ToString(), "SnakeGame");

            ini.Write("colidir", ((int)colidir).ToString(), "SnakeGame");
        }

        public static bool PontuacaoMaxima(int pontos)
        {
            return (pontos > int.Parse(ini.Read("pontosmax", "SnakeGame")));
        }

        public static Tabuleiro.ColidirComAsBordas GetColidir()
        {
            return (Tabuleiro.ColidirComAsBordas)int.Parse(ini.Read("colidir", "SnakeGame"));
        }
    }
}
