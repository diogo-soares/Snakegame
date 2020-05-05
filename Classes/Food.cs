using System;
using System.Drawing;

namespace SnakeGame.Classes
{
    public class Food
    {
        private Rectangle _foodRetangulo;
        private SolidBrush Pincel;
        private int x, y, largura, altura;

        public Rectangle FoodRetangulo
        {
            get
            {
                return _foodRetangulo;
            }
        }

        public Food(Random random)
        {
            FoodPosicao(random);

            Pincel = new SolidBrush(Color.LimeGreen);

            largura = 10;
            altura = 10;

            _foodRetangulo = new Rectangle(x, y, largura, altura);
        }

        public void FoodPosicao(Random random)
        {
            x = random.Next(1, 39) * 10;
            y = random.Next(1, 39) * 10;
        }

        public void DesenharFood(Graphics graphics)
        {
            _foodRetangulo.X = x;
            _foodRetangulo.Y = y;

            graphics.FillRectangle(Pincel, _foodRetangulo);
        }
    }
}
