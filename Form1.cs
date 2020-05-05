using SnakeGame.Classes;
using System;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        Snake snake = new Snake();
        Tabuleiro tabuleiro = new Tabuleiro();
        Food food;

        Random random = new Random();

        bool ESQUERDA = false;
        bool DIREITA = false;
        bool BAIXO = false;
        bool CIMA = false;

        int pontos = 0;

        bool GAMEINICIADO = false;

        public void ResetGame()
        {
            timer1.Enabled = false;

            MessageBox.Show("GAME-OVER!");

            if (GerenciadorArquivoIni.PontuacaoMaxima(pontos))
            {
                MessageBox.Show("Parabéns! Você conquistou um novo record!");
            }

            GerenciadorArquivoIni.Salvar(pontos, tabuleiro.Colidir);

            snake = new Snake();

            pontos = 0;

            ESQUERDA = false;
            DIREITA = false;
            BAIXO = false;
            CIMA = false;

            GAMEINICIADO = false;

            lbl_Inicio.Visible = true;

            food.FoodPosicao(random);
        }

        public void DetectarColisao()
        {
            #region Auto Colisão
            for (int i = 1; i < snake.SnakeRetangulo.Length; i++)
            {
                if (snake.SnakeRetangulo[0].IntersectsWith(snake.SnakeRetangulo[i]))
                {
                    ResetGame();
                }
            }
            #endregion

            #region Colisão com as Bordas
            switch (tabuleiro.Colidir)
            {
                case Tabuleiro.ColidirComAsBordas.Nao:
                    {
                        #region NÃO
                        for (int i = 0; i < snake.SnakeRetangulo.Length; i++)
                        {
                            // Colisão com o lado esquerdo
                            if (snake.SnakeRetangulo[i].X < tabuleiro.TabuleiroRetangulo.Left)
                            {
                                snake.SnakeRetangulo[i].X = tabuleiro.TabuleiroRetangulo.Right - snake.SnakeRetangulo[i].Width;
                            }

                            // Colisão com o lado direito
                            if (snake.SnakeRetangulo[i].X > tabuleiro.TabuleiroRetangulo.Right - snake.SnakeRetangulo[i].Width)
                            {
                                snake.SnakeRetangulo[i].X = tabuleiro.TabuleiroRetangulo.Left;
                            }

                            // Colisão com topo
                            if (snake.SnakeRetangulo[i].Y < tabuleiro.TabuleiroRetangulo.Top)
                            {
                                snake.SnakeRetangulo[i].Y = tabuleiro.TabuleiroRetangulo.Bottom - snake.SnakeRetangulo[i].Height;
                            }

                            // Colisão com o fundo
                            if (snake.SnakeRetangulo[i].Y > tabuleiro.TabuleiroRetangulo.Bottom - snake.SnakeRetangulo[i].Height)
                            {
                                snake.SnakeRetangulo[i].Y = tabuleiro.TabuleiroRetangulo.Top;
                            }
                        }
                        #endregion
                    }
                    break;
                case Tabuleiro.ColidirComAsBordas.Sim:
                    {
                        #region SIM
                        if (snake.SnakeRetangulo[0].X < tabuleiro.TabuleiroRetangulo.Left ||
                            snake.SnakeRetangulo[0].X > (tabuleiro.TabuleiroRetangulo.Right - snake.SnakeRetangulo[0].Width))
                        {
                            ResetGame();
                        }

                        if (snake.SnakeRetangulo[0].Y < tabuleiro.TabuleiroRetangulo.Top ||
                            snake.SnakeRetangulo[0].Y > (tabuleiro.TabuleiroRetangulo.Bottom - snake.SnakeRetangulo[0].Height))
                        {
                            ResetGame();
                        }
                        #endregion
                    }
                    break;
                default:
                    break;
            }
            #endregion
        }

        public Form1()
        {
            InitializeComponent();

            food = new Food(random);

            lbl_Inicio.Top = (tabuleiro.TabuleiroRetangulo.Height / 2) - (lbl_Inicio.Height / 2);
            lbl_Inicio.Left = (tabuleiro.TabuleiroRetangulo.Right / 2) - (lbl_Inicio.Width / 2);

            GerenciadorArquivoIni.CriarSeNaoExistir();

            tabuleiro.Colidir = GerenciadorArquivoIni.GetColidir();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    {
                        if (!GAMEINICIADO)
                        {
                            GAMEINICIADO = true;
                            timer1.Enabled = true;
                            lbl_Inicio.Visible = false;

                            ESQUERDA = false;
                            DIREITA = true;
                            BAIXO = false;
                            CIMA = false;
                        }
                    }
                    break;
                case Keys.Left:
                    {
                        if (!DIREITA)
                        {
                            ESQUERDA = true;
                            DIREITA = false;
                            BAIXO = false;
                            CIMA = false;
                        }
                    }
                    break;
                case Keys.Right:
                    {
                        if (!ESQUERDA)
                        {
                            ESQUERDA = false;
                            DIREITA = true;
                            BAIXO = false;
                            CIMA = false;
                        }
                    }
                    break;
                case Keys.Down:
                    {
                        if (!CIMA)
                        {
                            ESQUERDA = false;
                            DIREITA = false;
                            BAIXO = true;
                            CIMA = false;
                        }
                    }
                    break;
                case Keys.Up:
                    {
                        if (!BAIXO)
                        {
                            ESQUERDA = false;
                            DIREITA = false;
                            BAIXO = false;
                            CIMA = true;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ESQUERDA)
                snake.MovimentarSnake(Snake.Direcao.ESQUERDA);
            if (DIREITA)
                snake.MovimentarSnake(Snake.Direcao.DIREITA);
            if (BAIXO)
                snake.MovimentarSnake(Snake.Direcao.BAIXO);
            if (CIMA)
                snake.MovimentarSnake(Snake.Direcao.CIMA);

            if (snake.SnakeRetangulo[0].IntersectsWith(food.FoodRetangulo))
            {
                pontos++;
                snake.AlimentarSnake();
                food.FoodPosicao(random);
            }

            DetectarColisao();

            lbl_Pontos.Text = String.Format("Pontos: {0}", pontos);

            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            tabuleiro.DesenharTabuleiro(e.Graphics);
            snake.DesenharSnake(e.Graphics);
            food.DesenharFood(e.Graphics);
        }
    }
}
