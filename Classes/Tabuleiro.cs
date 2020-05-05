using System.Drawing;

namespace SnakeGame.Classes
{
    public class Tabuleiro
    {
        private Rectangle _tabuleiroRetangulo;
        private SolidBrush Pincel;
        private int x, y, largura, altura;

        public ColidirComAsBordas Colidir;

        public enum ColidirComAsBordas
        {            
            Nao = 0,
            Sim = 1
        };

        public Rectangle TabuleiroRetangulo 
        { 
            get
            {
                return _tabuleiroRetangulo;
            }
        }

        public Tabuleiro()
        {
            Pincel = new SolidBrush(Color.Black);

            x = 10;
            y = 10;
            largura = 780;
            altura = 390;

            _tabuleiroRetangulo = new Rectangle(x, y, largura, altura);
        }

        public void DesenharTabuleiro(Graphics graphics)
        {
            _tabuleiroRetangulo.X = x;
            _tabuleiroRetangulo.Y = y;

            graphics.FillRectangle(Pincel, _tabuleiroRetangulo);
        }
    }
}
