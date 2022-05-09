using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Mineswepeer;
using Mineswepeer.Properties;
using static System.Windows.Forms.FlatStyle;

namespace Game
{
    public partial class GameForm : Form
    {
        public GameForm(GameProparties gameProparties)
        {
            InitializeComponent();
            Other other = new Other
            {
                Row = gameProparties.Row,
                Column = gameProparties.Column,
                TopStart = 50,
                LeftStart = 50,
                NumberFlaged = 0,
                CountMine = gameProparties.MineCount,
                CheckFinishGame = 0,
                FinishStartFlag = true,
                LeftClick = false,
                RightClick = false,
                //RunMiddleClick = false,
                FlagCounterLabel = new Label
                {
                    Height = 32,
                    Width = 32,
                    Left = gameProparties.CounterLocation,
                    Top = 15
                }
            };
            other.FlagCounterLabel.Text = other.CountMine.ToString();
            Controls.Add(other.FlagCounterLabel);
            other.NumberOpened = ((other.Row * other.Column) - other.CountMine);
            int top = other.TopStart;
            int left = other.LeftStart;
            Size sizeBlock = new Size(32, 32);
            ClientSize = new System.Drawing.Size(100 + (other.Column * sizeBlock.Width), 100 + (other.Row * sizeBlock.Height));
            other.SizeBlock = sizeBlock;

            //ImageList statusImageList = new ImageList();
            //statusImageList.Images.SetKeyName(0, "Blank.png");
            //statusImageList.Images.SetKeyName(1, "RedFlag.png");
            //statusImageList.Images.SetKeyName(2, "QuestionMark.png");
            MineButton[,] buttons = new MineButton[other.Row, other.Column];
            for (int i = 0; i < other.Row; i++)
            {
                for (int j = 0; j < other.Column; j++)
                {
                    #region Button define old

                    //buttons[i, j] = new MineButton();
                    //buttons[i, j].Width = sizeBlock;
                    //buttons[i, j].Height = sizeBlock;
                    //buttons[i, j].Top = top;
                    //buttons[i, j].Left = left;
                    //buttons[i, j].X = i;
                    //buttons[i, j].Y = j;
                    //buttons[i, j].Clicked = false;
                    //buttons[i, j].NumberMine = 0;
                    //buttons[i, j].BackColor = Color.Aqua;
                    //buttons[i, j].FlatStyle = FlatStyle.Flat;
                    //buttons[i, j].FlatAppearance.BorderColor = Color.DarkSlateBlue;
                    //buttons[i, j].FlatAppearance.BorderSize = 1;

                    #endregion
                    buttons[i, j] = new MineButton
                    {
                        Width = other.SizeBlock.Width,
                        Height = other.SizeBlock.Height,
                        Top = top,
                        Left = left,
                        X = i,
                        Y = j,
                        
                        BackColor = Color.Aqua,
                        FlatStyle = Flat,
                        Status = 0,
                        FlatAppearance =
                        {
                            BorderColor = Color.DarkSlateBlue,
                            BorderSize = 1
                        }
                    };
                    //buttons[i, j].Click += new System.EventHandler(MineButton_Click);
                    //buttons[i, j].Click += delegate (object senderC, EventArgs eC) { MineButton_Click(senderC, eC, i, j); };
                    //buttons[i, j].Click += delegate (object sender, EventArgs e) { MineButton_Click(sender, e, buttons, row, column); };
                    //buttons[i, j].MouseClick += delegate (object sender, MouseEventArgs e) { MineButton_Click(sender, e, buttons, row, column); };
                    //buttons[i, j].MouseDown += MouseDownEvent;
                    //buttons[i, j].MouseDown += new System.Windows.Forms.MouseEventHandler(MouseDownEvent);
                    buttons[i, j].MouseDown += delegate (object sender, MouseEventArgs e) { MouseDownEvent(sender, e, buttons, other); };
                    //buttons[i, j].MouseUp += MouseUpEvent;
                    //buttons[i, j].MouseUp += new System.Windows.Forms.MouseEventHandler(MouseUpEvent);
                    buttons[i, j].MouseUp += delegate (object sender, MouseEventArgs e) { MouseUpEvent(sender, e, buttons, other); };

                    //buttons[i, j].UseVisualStyleBackColor = true;
                    //this.Controls.Add(buttons[i, j]);
                    Controls.Add(buttons[i, j]);
                    left += other.SizeBlock.Width;
                }
                top += other.SizeBlock.Height;
                left = other.LeftStart;
            }
            //SetupMine(buttons, other);
            //MineLocationNumber(buttons, other);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private static void SetupMineLocation(MineButton btn, MineButton[,] buttons, Other other)
        {
            int[,] locationMine = new int[other.CountMine, 2];
            Random random = new Random();
            for (int i = 0; i < other.CountMine; i++)
            {
                bool flagLocationMine;
                do
                {
                    flagLocationMine = false;
                    locationMine[i, 0] = random.Next(other.Row);
                    locationMine[i, 1] = random.Next(other.Column);
                    for (int j = 0; j < i; j++)
                    {
                        if ((locationMine[i, 0] == locationMine[j, 0] && locationMine[i, 1] == locationMine[j, 1]) || (btn.X - 1 == locationMine[i, 0] && btn.Y - 1 == locationMine[i, 1]) || (btn.X - 1 == locationMine[i, 0] && btn.Y == locationMine[i, 1]) || (btn.X - 1 == locationMine[i, 0] && btn.Y + 1 == locationMine[i, 1]) || (btn.X == locationMine[i, 0] && btn.Y - 1 == locationMine[i, 1]) || (btn.X == locationMine[i, 0] && btn.Y == locationMine[i, 1]) || (btn.X == locationMine[i, 0] && btn.Y + 1 == locationMine[i, 1]) || (btn.X + 1 == locationMine[i, 0] && btn.Y - 1 == locationMine[i, 1]) || (btn.X + 1 == locationMine[i, 0] && btn.Y == locationMine[i, 1]) || (btn.X + 1 == locationMine[i, 0] && btn.Y + 1 == locationMine[i, 1]))
                        {
                            flagLocationMine = true;
                        }
                    }
                } while (flagLocationMine);
            }

            for (int i = 0; i < other.Row; i++)
            {
                for (int j = 0; j < other.Column; j++)
                {
                    for (int k = 0; k < other.CountMine; k++)
                    {
                        if (locationMine[k, 0] == i && locationMine[k, 1] == j)
                        {
                            //buttons[i, j].Name = "mine";
                            buttons[i, j].NumberMine = -1;
                        }
                    }
                }
            }
        }

        #region MineLocationNumber old

        //private static void MineLocationNumber(int row, int column, MineButton[,] buttons)/*old*/
        //{
        //    //int temp;
        //    for (int i = 0; i < row; i++)
        //    {
        //        for (int j = 0; j < column; j++)
        //        {
        //            if (buttons[i, j].NumberMine != -1)/*(buttons[i, j].Name != "mine")*/
        //            {
        //                if (i == 0) //üst satır
        //                {
        //                    if (j == 0) //üst satır sol koşe
        //                    {
        //                        for (int k = i; k < i + 2; k++)
        //                        {
        //                            for (int l = j; l < j + 2; l++)
        //                            {
        //                                if (k == i && l == j)
        //                                {
        //                                    l++;
        //                                }

        //                                if (buttons[k, l].NumberMine == -1)/*(buttons[k, l].Name == "mine")*/
        //                                {
        //                                    //temp = Convert.ToInt32(buttons[i, j].Name);
        //                                    //temp++;
        //                                    //buttons[i, j].Name = temp.ToString();
        //                                    buttons[i, j].NumberMine++;

        //                                }
        //                            }
        //                        }
        //                    }
        //                    else if (j == column - 1) //üst satır sağ köşe
        //                    {
        //                        for (int k = i; k < i + 2; k++)
        //                        {
        //                            for (int l = j - 1; l < j + 1; l++)
        //                            {
        //                                if (k == i && l == j)
        //                                {
        //                                    k++;
        //                                    l = j - 1;
        //                                }

        //                                if (buttons[k, l].NumberMine == -1)/*(buttons[k, l].Name == "mine")*/
        //                                {
        //                                    //temp = Convert.ToInt32(buttons[i, j].Name);
        //                                    //temp++;
        //                                    //buttons[i, j].Name = temp.ToString();
        //                                    buttons[i, j].NumberMine++;
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else //üst satır orta
        //                    {
        //                        for (int k = i; k < i + 2; k++)
        //                        {
        //                            for (int l = j - 1; l < j + 2; l++)
        //                            {
        //                                if (k == i && l == j)
        //                                {
        //                                    l++;
        //                                }

        //                                if (buttons[k, l].NumberMine == -1)/*(buttons[k, l].Name == "mine")*/
        //                                {
        //                                    //temp = Convert.ToInt32(buttons[i, j].Name);
        //                                    //temp++;
        //                                    //buttons[i, j].Name = temp.ToString();
        //                                    buttons[i, j].NumberMine++;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                else if (i == row - 1) //alt satır
        //                {
        //                    if (j == 0) //alt satır sol koşe
        //                    {
        //                        for (int k = i - 1; k < i + 1; k++)
        //                        {
        //                            for (int l = j; l < j + 2; l++)
        //                            {
        //                                if (k == i && l == j)
        //                                {
        //                                    l++;
        //                                }

        //                                if (buttons[k, l].NumberMine == -1)/*(buttons[k, l].Name == "mine")*/
        //                                {
        //                                    //temp = Convert.ToInt32(buttons[i, j].Name);
        //                                    //temp++;
        //                                    //buttons[i, j].Name = temp.ToString();
        //                                    buttons[i, j].NumberMine++;
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else if (j == column - 1) //alt satır sağ köşe
        //                    {
        //                        for (int k = i - 1; k < i + 1; k++)
        //                        {
        //                            for (int l = j - 1; l < j + 1; l++)
        //                            {
        //                                if (k == i && l == j)
        //                                {
        //                                    break;
        //                                }

        //                                if (buttons[k, l].NumberMine == -1)/*(buttons[k, l].Name == "mine")*/
        //                                {
        //                                    //temp = Convert.ToInt32(buttons[i, j].Name);
        //                                    //temp++;
        //                                    //buttons[i, j].Name = temp.ToString();
        //                                    buttons[i, j].NumberMine++;
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else //üst satır orta
        //                    {
        //                        for (int k = i - 1; k < i + 1; k++)
        //                        {
        //                            for (int l = j - 1; l < j + 2; l++)
        //                            {
        //                                if (k == i && l == j)
        //                                {
        //                                    l++;
        //                                }

        //                                if (buttons[k, l].NumberMine == -1)/*(buttons[k, l].Name == "mine")*/
        //                                {
        //                                    //temp = Convert.ToInt32(buttons[i, j].Name);
        //                                    //temp++;
        //                                    //buttons[i, j].Name = temp.ToString();
        //                                    buttons[i, j].NumberMine++;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                else //orta satırlar
        //                {
        //                    if (j == 0) //orta satırlar sol sütün
        //                    {
        //                        for (int k = i - 1; k < i + 2; k++)
        //                        {
        //                            for (int l = j; l < j + 2; l++)
        //                            {
        //                                if (k == i && l == j)
        //                                {
        //                                    l++;
        //                                }

        //                                if (buttons[k, l].NumberMine == -1)/*(buttons[k, l].Name == "mine")*/
        //                                {
        //                                    //temp = Convert.ToInt32(buttons[i, j].Name);
        //                                    //temp++;
        //                                    //buttons[i, j].Name = temp.ToString();
        //                                    buttons[i, j].NumberMine++;
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else if (j == column - 1) //orta satırlar sağ sütün
        //                    {
        //                        for (int k = i - 1; k < i + 2; k++)
        //                        {
        //                            for (int l = j - 1; l < j + 1; l++)
        //                            {
        //                                if (k == i && l == j)
        //                                {
        //                                    k++;
        //                                    l = j - 1;
        //                                }

        //                                if (buttons[k, l].NumberMine == -1)/*(buttons[k, l].Name == "mine")*/
        //                                {
        //                                    //temp = Convert.ToInt32(buttons[i, j].Name);
        //                                    //temp++;
        //                                    //buttons[i, j].Name = temp.ToString();
        //                                    buttons[i, j].NumberMine++;
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else //orta satırlar orta sütünlar
        //                    {
        //                        for (int k = i - 1; k < i + 2; k++)
        //                        {
        //                            for (int l = j - 1; l < j + 2; l++)
        //                            {
        //                                if (k == i && l == j)
        //                                {
        //                                    l++;
        //                                }

        //                                if (buttons[k, l].NumberMine == -1)/*(buttons[k, l].Name == "mine")*/
        //                                {
        //                                    //temp = Convert.ToInt32(buttons[i, j].Name);
        //                                    //temp++;
        //                                    //buttons[i, j].Name = temp.ToString();
        //                                    buttons[i, j].NumberMine++;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        #endregion
        #region MineLocationNumber old old

        //protected static void MineLoctionNumber(int row, int column, Button[,] buttons) /*old old*/
        //{
        //    int temp;
        //    for (int i = 0; i < row; i++)
        //    {
        //        for (int j = 0; j < column; j++)
        //        {
        //            if (buttons[i, j].Name != "mine")
        //            {
        //                if (i == 0) //üst satır
        //                {
        //                    if (j == 0) //üst satır sol koşe
        //                    {
        //                        for (int k = i; k < i + 2; k++)
        //                        {
        //                            for (int l = j; l < j + 2; l++)
        //                            {
        //                                if (k == i && l == j)
        //                                {
        //                                    l++;
        //                                }

        //                                if (buttons[k, l].Name == "mine")
        //                                {
        //                                    temp = Convert.ToInt32(buttons[i, j].Name);
        //                                    temp++;
        //                                    buttons[i, j].Name = temp.ToString();
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else if (j == column - 1) //üst satır sağ köşe
        //                    {
        //                        for (int k = i; k < i + 2; k++)
        //                        {
        //                            for (int l = j - 1; l < j + 1; l++)
        //                            {
        //                                if (k == i && l == j)
        //                                {
        //                                    k++;
        //                                    l = j - 1;
        //                                }

        //                                if (buttons[k, l].Name == "mine")
        //                                {
        //                                    temp = Convert.ToInt32(buttons[i, j].Name);
        //                                    temp++;
        //                                    buttons[i, j].Name = temp.ToString();
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else //üst satır orta
        //                    {
        //                        for (int k = i; k < i + 2; k++)
        //                        {
        //                            for (int l = j - 1; l < j + 2; l++)
        //                            {
        //                                if (k == i && l == j)
        //                                {
        //                                    l++;
        //                                }

        //                                if (buttons[k, l].Name == "mine")
        //                                {
        //                                    temp = Convert.ToInt32(buttons[i, j].Name);
        //                                    temp++;
        //                                    buttons[i, j].Name = temp.ToString();
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                else if (i == row - 1) //alt satır
        //                {
        //                    if (j == 0) //alt satır sol koşe
        //                    {
        //                        for (int k = i - 1; k < i + 1; k++)
        //                        {
        //                            for (int l = j; l < j + 2; l++)
        //                            {
        //                                if (k == i && l == j)
        //                                {
        //                                    l++;
        //                                }

        //                                if (buttons[k, l].Name == "mine")
        //                                {
        //                                    temp = Convert.ToInt32(buttons[i, j].Name);
        //                                    temp++;
        //                                    buttons[i, j].Name = temp.ToString();
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else if (j == column - 1) //alt satır sağ köşe
        //                    {
        //                        for (int k = i - 1; k < i + 1; k++)
        //                        {
        //                            for (int l = j - 1; l < j + 1; l++)
        //                            {
        //                                if (k == i && l == j)
        //                                {
        //                                    break;
        //                                }

        //                                if (buttons[k, l].Name == "mine")
        //                                {
        //                                    temp = Convert.ToInt32(buttons[i, j].Name);
        //                                    temp++;
        //                                    buttons[i, j].Name = temp.ToString();
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else //üst satır orta
        //                    {
        //                        for (int k = i - 1; k < i + 1; k++)
        //                        {
        //                            for (int l = j - 1; l < j + 2; l++)
        //                            {
        //                                if (k == i && l == j)
        //                                {
        //                                    l++;
        //                                }

        //                                if (buttons[k, l].Name == "mine")
        //                                {
        //                                    temp = Convert.ToInt32(buttons[i, j].Name);
        //                                    temp++;
        //                                    buttons[i, j].Name = temp.ToString();
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                else //orta satırlar
        //                {
        //                    if (j == 0) //orta satırlar sol sütün
        //                    {
        //                        for (int k = i - 1; k < i + 2; k++)
        //                        {
        //                            for (int l = j; l < j + 2; l++)
        //                            {
        //                                if (k == i && l == j)
        //                                {
        //                                    l++;
        //                                }

        //                                if (buttons[k, l].Name == "mine")
        //                                {
        //                                    temp = Convert.ToInt32(buttons[i, j].Name);
        //                                    temp++;
        //                                    buttons[i, j].Name = temp.ToString();
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else if (j == column - 1) //orta satırlar sağ sütün
        //                    {
        //                        for (int k = i - 1; k < i + 2; k++)
        //                        {
        //                            for (int l = j - 1; l < j + 1; l++)
        //                            {
        //                                if (k == i && l == j)
        //                                {
        //                                    k++;
        //                                    l = j - 1;
        //                                }

        //                                if (buttons[k, l].Name == "mine")
        //                                {
        //                                    temp = Convert.ToInt32(buttons[i, j].Name);
        //                                    temp++;
        //                                    buttons[i, j].Name = temp.ToString();
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else //orta satırlar orta sütünlar
        //                    {
        //                        for (int k = i - 1; k < i + 2; k++)
        //                        {
        //                            for (int l = j - 1; l < j + 2; l++)
        //                            {
        //                                if (k == i && l == j)
        //                                {
        //                                    l++;
        //                                }

        //                                if (buttons[k, l].Name == "mine")
        //                                {
        //                                    temp = Convert.ToInt32(buttons[i, j].Name);
        //                                    temp++;
        //                                    buttons[i, j].Name = temp.ToString();
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        #endregion
        private static void SetupMineLocationNumber(MineButton[,] buttons, Other other)
        {
            for (int i = 0; i < other.Row; i++)
            {
                for (int j = 0; j < other.Column; j++)
                {
                    if (buttons[i, j].NumberMine != -1)
                    {
                        int startX;
                        int finishX;
                        int startY;
                        int finishY;
                        if (i == 0)
                        {
                            startX = i;
                            finishX = i + 1;
                        }
                        else if (i == other.Row - 1)
                        {
                            startX = i - 1;
                            finishX = i;
                        }
                        else
                        {
                            startX = i - 1;
                            finishX = i + 1;
                        }

                        if (j == 0)
                        {
                            startY = j;
                            finishY = j + 1;
                        }
                        else if (j == other.Column - 1)
                        {
                            startY = j - 1;
                            finishY = j;
                        }
                        else
                        {
                            startY = j - 1;
                            finishY = j + 1;
                        }

                        for (int k = startX; k <= finishX; k++)
                        {
                            for (int l = startY; l <= finishY; l++)
                            {
                                if (k == i && l == j)
                                {
                                    if (j == other.Column - 1)
                                    {
                                        if (i == other.Row - 1)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            k++;
                                            l = j - 1;
                                        }
                                    }
                                    else
                                    {
                                        l++;
                                    }
                                }
                                if (buttons[k, l].NumberMine == -1)
                                {
                                    buttons[i, j].NumberMine++;
                                }
                            }
                        }
                    }
                }
            }
        }

        #region Mine Button Click (transfer to mine button down)
        //private void MineButton_Click(object sender, MouseEventArgs e, MineButton[,] buttons, int row, int column)/*(object sender, EventArgs e)*//*(object senderC, EventArgs eC, int x, int y)*/
        //{
        //    MineButton btn = (MineButton)sender;
        //    int x = btn.X;
        //    int y = btn.Y;
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        if (btn.NumberMine == -1)
        //        {
        //            //MessageBox.Show("bomba");
        //        }

        //        else if (btn.NumberMine == 0 && !btn.Clicked)
        //        {
        //            //btn.Enabled = false;
        //            btn.BackColor = Color.AliceBlue;
        //            btn.Clicked = true;
        //            //MessageBox.Show(btn.NumberMine.ToString());
        //            //btn.Text = btn.NumberMine.ToString();
        //            OpenForVoid(e, buttons, row, column, x, y);
        //        }
        //        else if (btn.NumberMine != 0)
        //        {
        //            btn.Text = btn.NumberMine.ToString();
        //            //MessageBox.Show(btn.NumberMine.ToString());
        //            //btn.Enabled = false;
        //            btn.BackColor = Color.AliceBlue;
        //            btn.Clicked = true;
        //        }
        //    }
        //}



        #endregion

        #region OpenForVoid Old

        //private void OpenForVoid(EventArgs eC, MineButton[,] buttons, int row, int column, int x, int y)/*old*/
        //{
        //    if (x == 0)
        //    {
        //        if (y == 0)
        //        {
        //            for (int i = x; i < x + 2; i++)
        //            {
        //                for (int j = y; j < y + 2; j++)
        //                {
        //                    if (i == x && j == y)
        //                    {
        //                        j++;
        //                    }

        //                    mineButton_Click(buttons[i, j], eC, buttons, row, column);
        //                }
        //            }
        //        }
        //        else if (y == column - 1)
        //        {
        //            for (int i = x; i < x + 2; i++)
        //            {
        //                for (int j = y - 1; j < y + 1; j++)
        //                {
        //                    if (i == x && j == y)
        //                    {
        //                        i++;
        //                        j = y - 1;
        //                    }

        //                    mineButton_Click(buttons[i, j], eC, buttons, row, column);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            for (int i = x; i < x + 2; i++)
        //            {
        //                for (int j = y - 1; j < y + 2; j++)
        //                {
        //                    if (i == x && j == y)
        //                    {
        //                        j++;
        //                    }

        //                    mineButton_Click(buttons[i, j], eC, buttons, row, column);
        //                }
        //            }
        //        }
        //    }
        //    else if (x == row - 1)
        //    {
        //        if (y == 0)
        //        {
        //            for (int i = x - 1; i < x + 1; i++)
        //            {
        //                for (int j = y; j < y + 2; j++)
        //                {
        //                    if (i == x && j == y)
        //                    {
        //                        j++;
        //                    }

        //                    mineButton_Click(buttons[i, j], eC, buttons, row, column);
        //                }
        //            }
        //        }
        //        else if (y == column - 1)
        //        {
        //            for (int i = x - 1; i < x + 1; i++)
        //            {
        //                for (int j = y - 1; j < y + 1; j++)
        //                {
        //                    if (i == x && j == y)
        //                    {
        //                        break;
        //                    }

        //                    mineButton_Click(buttons[i, j], eC, buttons, row, column);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            for (int i = x - 1; i < x + 1; i++)
        //            {
        //                for (int j = y - 1; j < y + 2; j++)
        //                {
        //                    if (i == x && j == y)
        //                    {
        //                        j++;
        //                    }

        //                    mineButton_Click(buttons[i, j], eC, buttons, row, column);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (y == 0)
        //        {
        //            for (int i = x - 1; i < x + 2; i++)
        //            {
        //                for (int j = y; j < y + 2; j++)
        //                {
        //                    if (i == x && j == y)
        //                    {
        //                        j++;
        //                    }

        //                    mineButton_Click(buttons[i, j], eC, buttons, row, column);
        //                }
        //            }
        //        }
        //        else if (y == column - 1)
        //        {
        //            for (int i = x - 1; i < x + 2; i++)
        //            {
        //                for (int j = y - 1; j < y + 1; j++)
        //                {
        //                    if (i == x && j == y)
        //                    {
        //                        i++;
        //                        j = y - 1;
        //                    }

        //                    mineButton_Click(buttons[i, j], eC, buttons, row, column);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            for (int i = x - 1; i < x + 2; i++)
        //            {
        //                for (int j = y - 1; j < y + 2; j++)
        //                {
        //                    if (i == x && j == y)
        //                    {
        //                        j++;
        //                    }

        //                    mineButton_Click(buttons[i, j], eC, buttons, row, column);
        //                }
        //            }
        //        }
        //    }
        //}

        #endregion
        private void OpenForVoid(MineButton btn, MineButton[,] buttons, Other other)
        {
            int startX;
            int finishX;
            int startY;
            int finishY;
            if (btn.X == 0)
            {
                startX = btn.X;
                finishX = btn.X + 1;
            }
            else if (btn.X == other.Row - 1)
            {
                startX = btn.X - 1;
                finishX = btn.X;
            }
            else
            {
                startX = btn.X - 1;
                finishX = btn.X + 1;
            }

            if (btn.Y == 0)
            {
                startY = btn.Y;
                finishY = btn.Y + 1;
            }
            else if (btn.Y == other.Column - 1)
            {
                startY = btn.Y - 1;
                finishY = btn.Y;
            }
            else
            {
                startY = btn.Y - 1;
                finishY = btn.Y + 1;
            }

            for (int i = startX; i <= finishX; i++)
            {
                for (int j = startY; j <= finishY; j++)
                {
                    if (i == btn.X && j == btn.Y)
                    {
                        if (btn.Y == other.Column - 1)
                        {
                            if (btn.X == other.Row - 1)
                            {
                                continue;
                            }
                            else
                            {
                                i++;
                                j = btn.Y - 1;
                            }
                        }
                        else
                        {
                            j++;
                        }
                    }

                    if (!buttons[i, j].Clicked)
                    {
                        //MineButton_Click(buttons[i, j], e, buttons, row, column);
                        //MouseDownEvent(buttons[i, j], e, buttons, other);
                        MineButtonLeftClickUp(buttons[i, j], buttons, other);
                    }
                }
            }
        }

        private void MouseDownEvent(object sender, MouseEventArgs e, MineButton[,] buttons, Other other)
        {
            MineButton btn = (MineButton)sender;
            if (e.Button == MouseButtons.Left)
            {
                other.LeftClick = true;
                if (other.RightClick == true)
                {
                    MineButtonMiddleClickDown(btn, buttons, other);
                }
            }
            if (e.Button == MouseButtons.Middle)
            {
                MineButtonMiddleClickDown(btn, buttons, other);
            }
            if (e.Button == MouseButtons.Right)
            {
                other.RightClick = true;
                if (other.LeftClick == true)
                {
                    MineButtonMiddleClickDown(btn, buttons, other);
                }
            }

        }
        private void MouseUpEvent(object sender, MouseEventArgs e, MineButton[,] buttons, Other other)
        {
            MineButton btn = (MineButton)sender;
            if (e.Button == MouseButtons.Left)
            {
                other.LeftClick = false;
                if (other.RightClick == true)
                {
                    MineButtonMiddleClickUp(btn, buttons, other);
                }
                else
                {
                    if (e.X >= 0 && e.X <= other.SizeBlock.Width && e.Y >= 0 && e.Y <= other.SizeBlock.Height)
                    {
                        if (!other.FirstClick)
                        {
                            SetupMineLocation(btn, buttons, other);
                            SetupMineLocationNumber(buttons, other);
                            other.FirstClick = true;
                        }
                        MineButtonLeftClickUp(btn, buttons, other);
                    }
                }
            }

            if (e.Button == MouseButtons.Middle)
            {
                MineButtonMiddleClickUp(btn, buttons, other);
            }

            if (e.Button == MouseButtons.Right)
            {
                other.RightClick = false;
                if (other.LeftClick == true)
                {
                    MineButtonMiddleClickUp(btn, buttons, other);
                }
                else
                {
                    MineButtonRightClickUp(btn, buttons, other);
                }
            }
        }

        private void MineButtonLeftClickUp(MineButton btn, MineButton[,] buttons, Other other)
        {
            if (btn.Status == 0 && !btn.Clicked)
            {
                if (btn.NumberMine == -1)
                {
                    //MessageBox.Show("bomba");
                    other.CheckFinishGame = -1;
                }

                else if (btn.NumberMine == 0)
                {
                    //btn.Enabled = false;
                    btn.BackColor = Color.AliceBlue;
                    btn.Clicked = true;
                    other.NumberOpened--;
                    //MessageBox.Show(btn.NumberMine.ToString());
                    //btn.Text = btn.NumberMine.ToString();
                    OpenForVoid(btn, buttons, other);
                }
                else
                {
                    btn.Text = btn.NumberMine.ToString();
                    //MessageBox.Show(btn.NumberMine.ToString());
                    //btn.Enabled = false;
                    btn.BackColor = Color.AliceBlue;
                    btn.Clicked = true;
                    other.NumberOpened--;
                }
            }

            FinishGame(buttons, other);

        }

        private void MineButtonMiddleClickDown(MineButton btn, MineButton[,] buttons, Other other)
        {
            int x = btn.X;
            int y = btn.Y;
            int startX;
            int finishX;
            int startY;
            int finishY;
            if (x == 0)
            {
                startX = x;
                finishX = x + 1;
            }
            else if (x == other.Row - 1)
            {
                startX = x - 1;
                finishX = x;
            }
            else
            {
                startX = x - 1;
                finishX = x + 1;
            }

            if (y == 0)
            {
                startY = y;
                finishY = y + 1;
            }
            else if (y == other.Column - 1)
            {
                startY = y - 1;
                finishY = y;
            }
            else
            {
                startY = y - 1;
                finishY = y + 1;
            }

            btn.NumberFlag = 0;
            for (int i = startX; i <= finishX; i++)
            {
                for (int j = startY; j <= finishY; j++)
                {
                    if (buttons[i, j].Status == 1)
                    {
                        btn.NumberFlag++;
                    }
                }
            }

            for (int i = startX; i <= finishX; i++)
            {
                for (int j = startY; j <= finishY; j++)
                {
                    if (btn.NumberMine <= btn.NumberFlag && btn.Clicked && !buttons[i, j].Clicked && buttons[i, j].Status != 1)
                    {
                        //MineButton_Click(buttons[i,j], e, buttons, row, column);
                        //MouseEventArgs es = new MouseEventArgs(MouseButtons.Left, e.Clicks, e.X, e.Y, e.Delta);
                        //MouseDownEvent(buttons[i, j], es, buttons, other);
                        MineButtonLeftClickUp(buttons[i, j], buttons, other);
                    }
                    if (!buttons[i, j].Clicked && buttons[i, j].Status == 0)
                    {
                        buttons[i, j].BackColor = Color.CornflowerBlue;
                    }
                }
            }
        }
        private void MineButtonMiddleClickUp(MineButton btn, MineButton[,] buttons, Other other)
        {
            int x = btn.X;
            int y = btn.Y;
            int startX;
            int finishX;
            int startY;
            int finishY;
            if (x == 0)
            {
                startX = x;
                finishX = x + 1;
            }
            else if (x == other.Row - 1)
            {
                startX = x - 1;
                finishX = x;
            }
            else
            {
                startX = x - 1;
                finishX = x + 1;
            }

            if (y == 0)
            {
                startY = y;
                finishY = y + 1;
            }
            else if (y == other.Column - 1)
            {
                startY = y - 1;
                finishY = y;
            }
            else
            {
                startY = y - 1;
                finishY = y + 1;
            }

            for (int i = startX; i <= finishX; i++)
            {
                for (int j = startY; j <= finishY; j++)
                {
                    if (!buttons[i, j].Clicked && buttons[i, j].Status == 0)
                    {
                        buttons[i, j].BackColor = Color.Aqua;
                    }
                }
            }
        }

        private void MineButtonRightClickUp(MineButton btn, MineButton[,] buttons, Other other)
        {
            if (!btn.Clicked)
            {
                btn.Status++;
                btn.Status %= 3;
                if (btn.Status == 0)
                {
                    btn.BackgroundImage = ResizeImage(Resources.Blank, other.SizeBlock);
                }
                else if (btn.Status == 1)
                {
                    btn.BackgroundImage = ResizeImage(Resources.RedFlag, other.SizeBlock);
                    other.NumberFlaged++;
                    other.FlagCounterLabel.Text = (other.CountMine - other.NumberFlaged).ToString();
                }
                else if (btn.Status == 2)
                {
                    btn.BackgroundImage = ResizeImage(Resources.QuestionMark, other.SizeBlock);
                    other.NumberFlaged--;
                    other.FlagCounterLabel.Text = (other.CountMine - other.NumberFlaged).ToString();
                }
            }
        }

        public static Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void FinishGame(MineButton[,] buttons, Other other)
        {
            if (other.FinishStartFlag)
            {
                if (other.NumberOpened == 0)
                {
                    other.CheckFinishGame = 1;
                }
                if (other.CheckFinishGame == -1 || other.CheckFinishGame == 1)
                {
                    other.FinishStartFlag = false;
                    for (int i = 0; i < other.Row; i++)
                    {
                        for (int j = 0; j < other.Column; j++)
                        {
                            if (buttons[i, j].NumberMine != -1)
                            {
                                MineButtonLeftClickUp(buttons[i, j], buttons, other);
                            }
                            else if (buttons[i, j].Status == 1)
                            {
                                buttons[i, j].BackgroundImage = ResizeImage(Resources.FlagedMine, other.SizeBlock);
                                buttons[i, j].Clicked = true;
                            }
                            else if (other.CheckFinishGame == -1)
                            {
                                buttons[i, j].BackgroundImage = ResizeImage(Resources.RedMine, other.SizeBlock);
                                buttons[i, j].BackColor = Color.AliceBlue;
                                buttons[i, j].Clicked = true;
                            }
                        }
                    }

                    if (other.CheckFinishGame == 1)
                    {
                        MessageBox.Show("Kazandınız");
                    }
                    else if (other.CheckFinishGame == -1)
                    {
                        MessageBox.Show("Kaybettiniz");
                    }
                }
            }
        }

    }
}