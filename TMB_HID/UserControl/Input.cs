using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMB.UserControls
{
    class Input
    {
        /// <summary>
        /// Coleción de  valores pareados contiene un String y un Double
        /// </summary>
        public ObservableCollection<KeyValuePair<string, Double>> ValueList { get; private set; }
        public int max { get; set; }
        /// <summary>
        /// Contructor de la clase
        /// </summary>
        public Input()
        {
            this.ValueList = new ObservableCollection<KeyValuePair<string, Double>>();
            max = 10;
        }


        /// <summary>
        /// Este método agrega un nuevo elemento a la lista 
        /// </summary>
        /// <param name="value">Valor dependiente</param>
        /// <param name="key">Valor independiente</param>
        public void Add(String value, Double key)
        {
            //elimina el primer valor de la colección
            //manteniendo siempre 10 registros
            if (ValueList.Count > max)
            {
                ValueList.RemoveAt(0);
            }
            ValueList.Add(new KeyValuePair<string, Double>(value, key));

        }


        /// <summary>
        /// Quita todos los elementos de la colección
        /// </summary>
        public void clear()
        {
            this.ValueList.Clear();
        }

    }
}