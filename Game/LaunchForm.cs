using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Game
{
    public partial class LaunchForm : Form
    {
        public LaunchForm()
        {
            InitializeComponent();
            Button easy = new Button
            {
                Top = 50,
                Left = 50,
                Width = 100,
                Height = 50,
                Text = "Kolay Mod: \n9x9 Alan \n10 Mayın",
                TextAlign = ContentAlignment.MiddleLeft
            };

            easy.Click += new System.EventHandler(EasyClick);
            Controls.Add(easy);
            Button medium = new Button
            {
                Top = 100,
                Left = 50,
                Width = 100,
                Height = 50,
                Text = "Orta Mod: \n16x16 Alan \n40 Mayın",
                TextAlign = ContentAlignment.MiddleLeft
            };
            medium.Click += new System.EventHandler(MediumClick);
            Controls.Add(medium);
            Button hard = new Button
            {
                Top = 150,
                Left = 50,
                Width = 100,
                Height = 50,
                Text = "Zor Mod: \n16x30 Alan \n99 Mayın",
                TextAlign = ContentAlignment.MiddleLeft
            };
            hard.Click += new System.EventHandler(HardClick);
            Controls.Add(hard);
            Button custom = new Button
            {
                Top = 200,
                Left = 50,
                Width = 100,
                Height = 50,
                Text = "Özel Mod",
                TextAlign = ContentAlignment.MiddleLeft
            };
            custom.Click += new System.EventHandler(CustomClick);
            Controls.Add(custom);
        }

        private void LaunchForm_Load(object sender, EventArgs e)
        {

        }

        private void EasyClick(object sender, EventArgs e)
        {
            Form game = new GameForm(gameProparties: new GameProparties(9, 9, 10));
            this.Hide();
            game.ShowDialog();
            this.Show();
        }

        private void MediumClick(object sender, EventArgs e)
        {
            Form game = new GameForm(gameProparties: new GameProparties(16, 16, 40));
            this.Hide();
            game.ShowDialog();
            this.Show();
        }

        private void HardClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Form game = new GameForm(gameProparties: new GameProparties(16, 30, 99));
            this.Hide();
            game.ShowDialog();
            this.Show();
        }

        private void CustomClick(object sender, EventArgs e)
        {
            this.ClientSize = new Size(400, 300);
            Label rowLabel = new Label
            {
                Text = "Yükesklik:\nEn küçük 9, \nEn büyük 24.",
                Height = 50,
                Width = 100,
                Top = 50,
                Left = 150
            };
            Controls.Add(rowLabel);
            TextBox rowTextBox = new TextBox
            {
                Height = 20,
                Width = 100,
                Top = 50,
                Left = 250,
                MaxLength = 2
            };
            rowTextBox.KeyPress += delegate (object senderT, KeyPressEventArgs eT) { textBox_Keypress(senderT, eT); };
            Controls.Add(rowTextBox);
            Label columnLabel = new Label
            {
                Text = "Genişlik:\nEn küçük 9, \nEn büyük 30.",
                Height = 50,
                Width = 100,
                Top = 100,
                Left = 150
            };
            Controls.Add(columnLabel);
            TextBox columnTextBox = new TextBox
            {
                Height = 20,
                Width = 100,
                Top = 100,
                Left = 250
            };
            rowTextBox.MaxLength = 2;
            columnTextBox.KeyPress += delegate (object senderT, KeyPressEventArgs eT) { textBox_Keypress(senderT, eT); };
            Controls.Add(columnTextBox);
            Button setSizeButton = new Button
            {
                Top = 200,
                Left = 150,
                Width = 100,
                Height = 50,
                Text = "Boyutu onayla",
                TextAlign = ContentAlignment.MiddleLeft
            };
            setSizeButton.Click += delegate (object senderT, EventArgs eT) { SetSizeClick(senderT, eT, rowTextBox, columnTextBox); };
            Controls.Add(setSizeButton);
            //Form game = new GameForm(gameProparties: new GameProparties(16, 30, 99));
            //this.Hide();
            //game.ShowDialog();
            //this.Show();
        }

        private void SetSizeClick(object sender, EventArgs e, TextBox rowTextBox, TextBox columnTextBox)
        {
            bool flagRow;
            bool flagColumn;
            if (rowTextBox.Text == "")
            {
                MessageBox.Show("Lütfen yükseklik değerini boş bırakmayınız!");
                flagRow = false;
            }
            else if (Convert.ToInt32(rowTextBox.Text) < 9 || Convert.ToInt32(rowTextBox.Text) > 24)
            {
                MessageBox.Show("Lütfen yükseklik değerini 9 ile 24 arasında yazınız!");
                flagRow = false;
            }
            else
            {
                flagRow = true;
            }
            if (columnTextBox.Text == "")
            {
                MessageBox.Show("Lütfen genişlik değerini boş bırakmayınız!");
                flagColumn = false;
            }
            else if (Convert.ToInt32(columnTextBox.Text) < 9 || Convert.ToInt32(columnTextBox.Text) > 30)
            {
                MessageBox.Show("Lütfen genişlik değerini 9 ile 30 arasında yazınız!");
                flagColumn = false;
            }
            else
            {
                flagColumn = true;
            }

            if (flagColumn && flagRow)
            {
                rowTextBox.Enabled = false;
                columnTextBox.Enabled = false;
                Label mineLabel = new Label
                {
                    Text = "Mayın Sayısı:\nEn küçük 10, \nEn büyük " + ((Convert.ToInt32(rowTextBox.Text) - 1) * (Convert.ToInt32(columnTextBox.Text) - 1)).ToString(),
                    Height = 50,
                    Width = 100,
                    Top = 150,
                    Left = 150
                };
                Controls.Add(mineLabel);
                TextBox MineTextBox = new TextBox
                {
                    Height = 20,
                    Width = 100,
                    Top = 150,
                    Left = 250,
                    MaxLength = 3
                };
                MineTextBox.KeyPress += delegate (object senderT, KeyPressEventArgs eT) { textBox_Keypress(senderT, eT); };
                Controls.Add(MineTextBox);
                Button runGameButton = new Button
                {
                    Top = 200,
                    Left = 250,
                    Width = 100,
                    Height = 50,
                    Text = "Oyunu başlat",
                    TextAlign = ContentAlignment.MiddleLeft
                };
                runGameButton.Click += delegate (object senderT, EventArgs eT) { CustomRunGame(senderT, eT, Convert.ToInt32(rowTextBox.Text), Convert.ToInt32(columnTextBox.Text), MineTextBox.Text); };
                Controls.Add(runGameButton);
            }
        }

        private void CustomRunGame(object sender, EventArgs e, int row, int column,  string countMine)
        {
            if (countMine == "")
            {
                MessageBox.Show("Lütfen mayın sayısını boş bırakmayınız!");
            }
            else if (Convert.ToInt32(countMine) < 10 || Convert.ToInt32(countMine) > ((row-1)* (column - 1)))
            {
                MessageBox.Show("Lütfen mayın sayısını 10 ile" + ((row - 1) * (column - 1)).ToString() + " 24 arasında yazınız!");
            }
            else
            {
                Form game = new GameForm(gameProparties: new GameProparties(row, column, Convert.ToInt32(countMine)));
                this.Hide();
                game.ShowDialog();
                this.Show();
            }
            
        }
        

        private void textBox_Keypress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            #region only allow one decimal point

            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}

            #endregion
        }
        
    }
}
