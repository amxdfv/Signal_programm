using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    static class Program
    {

        public class Signal
        {
            public int number;               //количество сигналов
            /*public int type;               // тип сигнала
            public int param1;             // параметры сигнала
            public int param2;
            public int param3;*/
            public int procduration;         // длимтельность процедуры
            
            public int[,] signals = new int [100,5];       // массив формирования процедуры

            public void addSignal(int number, int type, int param1, int param2, int param3, int duration)            // метод добавления сигналав процедуру
            {
                signals[number, 0] = type;
                signals[number, 1] = param1;
                signals[number, 2] = param2;
                signals[number, 3] = param3;
                signals[number, 4] = duration;
                               
            }

            public void procDuration()            // суммарная длительность процедуры
            {
                procduration = 0;
                for (int i = 0; i < number; i++)
                {
                    procduration = procduration + signals[i, 4];
                }
            }

            public void signalClean()
            {
                for (int i =0; i < number; i++)
                {
                    signals[number, 0] = 0;
                    signals[number, 1] = 0;
                    signals[number, 2] = 0;
                    signals[number, 3] = 0;
                    signals[number, 4] = 0;

                }
                number = 0;
                procduration = 0;
            }

        }
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]

        

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
