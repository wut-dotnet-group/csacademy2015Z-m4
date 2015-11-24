using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MojeOkienko
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // nowy, wygodny sposób inicjalizacji właściwości nowego obiektu
            Sprawa nowaSprawa = new Sprawa
            {
                Nazwa = textBox1.Text,
                Data = dateTimePicker1.Value,
                Opis = richTextBox1.Text
            };
            checkedListBox1.Items.Add(nowaSprawa);
        }

        // ten 'event handler' będzie wykonany przy każdej zmianie zawartości textBox1
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains("dupa"))
            {
                MessageBox.Show("NIE WOLNO!");
                // UWAGA: ponieważ string jest 'immutable', metoda string.Replace
                // nie zmienia stringa na którym została wywołana (żadna metoda string nie zmienia
                // obiektu na którym jest wołana)
                // Z tego powodu musimy nowy, zdewulgaryzowany obiekt string przypisać z powrotem na pole 'Text'.
                textBox1.Text = textBox1.Text.Replace("dupa", "****");
            }
        }

        private void wyczyscButton_Click(object sender, EventArgs e)
        {
            // zabawa w rzutowanie obiektu będącego źródłem wydarzenia:
            var przycisk = sender as Button; // bezpieczne rzutowanie (przycisk == null gdy sender nie jest typu Button)
            if (przycisk != null)
            {
                // możemy używać właściwości przycisku
                var result = MessageBox.Show("Kliknięto przycisk: " + przycisk.Text);
                if (result != DialogResult.OK)
                {
                    // jeśli nie OK, to nie chcemy czyścić listy
                    // możemy na przykład podnieść (wywołać) nasz EventPrywatny i wyjść:
                    PodniesEventPrywatny();
                    return;
                }
            }
            else
            {
                // 'event' nie został wysłany przez przycisk
                MessageBox.Show("jest źle");
            }
            // czyszczenie listy:
            checkedListBox1.Items.Clear();
        }

        #region EventPrywatny

        private event EventHandler EventPrywatny;

        private void PodniesEventPrywatny()
        {
            // wersja C# 6.0 (obsługiwana w Visual Studio 2015)
            EventPrywatny?.Invoke(this, EventArgs.Empty);

            // wersja C# 5.0- (obsługiwana w poprzednich wersjach Visual Studio)
            var temp = EventPrywatny;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }

            // obie wersje są równoważne: pierwsza jest po prostu nowsza i czytelniejsza
            // obie są zabezpieczone przed 'wyścigiem' w dostępie do zmiennej
        }

        private void GdyEventPrywatny(object sender, EventArgs e)
        {
            MessageBox.Show("Obsługa eventu '" + nameof(EventPrywatny) + "'");
        }

        #endregion EventPrywatny
    }
}
