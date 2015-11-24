using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojeOkienko
{
   public class Sprawa
    {
        public DateTime Data { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public bool Wykonana { get; set; }

        public override string ToString()
        {
            // ta metoda jest domyślnie wywoływana przez CheckedListBox przy wyświetlaniu obiektu
            return Data.ToShortDateString() + " " + Nazwa + " : " + Opis;
        }
    }
}
