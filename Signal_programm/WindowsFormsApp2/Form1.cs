using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static WindowsFormsApp2.Program;

namespace WindowsFormsApp2
{

    public partial class Form1 : Form
    {
       // int number = 0;
        int m = 0;  // счетчик для чекбокса

        int timecode = 0;

        Signal sig = new Signal();
        // Thread thread = new Thread(RunProc);
        public string[] tape = new string [100];
        
        public Form1()
        {
            InitializeComponent();

        }

        

        public void TimeRem()
        {
            for (int k = 0; k < sig.procduration; k++)
            {
               
                Action bar = () => progressBar1.Value=k; 
                if (InvokeRequired)
                    Invoke(bar);
                else
                    bar();

                Thread.Sleep(1);
            }


        } 

        private void RunProc()             // функция запуска потока
        {
            
            int i;
            for (i = 0; i < sig.number; i++)
            {
                Action full = () => listBox2.Items.Insert(0, tape[i]);
                if (InvokeRequired)
                    Invoke(full);
                else
                    full();
                Thread.Sleep(sig.signals[i, 4]);
            }

            Action end = () => listBox2.Items.Insert(0, "End of procedure");
            if (InvokeRequired)
                Invoke(end);
            else
                end();

            //var add = new Form1();
            Action action = () => button1.Enabled = true;
            if (InvokeRequired)
                Invoke(action);
            else
                action();

            Action del = () => button3.Enabled = true;
            if (InvokeRequired)
                Invoke(del);
            else
                del();

            Action group = () => groupBox1.Enabled = true;
            if (InvokeRequired)
                Invoke(group);
            else
                group();
            timecode = 0;

        }


         
        private void checkBox1_CheckedChanged(object sender, EventArgs e)      // меняем тип сигнала
        {
            if (m == 0)
            {
                groupBox1.Text = "FM-сигнал";
                label1.Text = "Частота несущей, Гц";
                label2.Text = "Амплитуда несущей, В";
                label3.Text = "Частота модулированного сигнала, Гц";
                m = 1;
            }
            else
            {
                groupBox1.Text = "Синусоидальный сигнал";
                label1.Text = "Частота, Гц";
                label2.Text = "Амплитуда, В";
                label3.Text = "Фазовый сдвиг, гр";
                m = 0;
            }
            
        }

        public void tradeSignal(int nums)
        {
            //string tape[];
            int num;
           // int timecode = 0;
            for (num = 0; num < nums; num++)
            {
                if (sig.signals[num, 0] == 0)
                {
                    tape[num] = "SIN, Freq " + sig.signals[num, 1].ToString() + " Hz, Ampl" + sig.signals[num, 2].ToString() + " V, Phase shift " + sig.signals[num, 3].ToString() + " deg, Time code " + timecode.ToString() + " ms";


                }
                else
                {
                    tape[num] = "FM, Freq main " + sig.signals[num, 1].ToString() + " Hz, Ampl main" + sig.signals[num, 2].ToString() + " V, Freq mod " + sig.signals[num, 3].ToString() + " Hz, Time code " + timecode.ToString() + " ms";
                }
                timecode = timecode + sig.signals[num, 4];
            }
            
        }

        public void addSignal(int num)
        {
            
            string tape;
            
            if (sig.signals[num, 0] == 0)
            {
                 tape = "SIN, Freq " + sig.signals[num, 1].ToString() + " Hz, Ampl" + sig.signals[num, 2].ToString() + " V, Phase shift " + sig.signals[num, 3].ToString() + " deg, Dur " + sig.signals[num, 4].ToString() + " ms";

               
            }
            else
            {
                 tape = "FM, Freq main " + sig.signals[num, 1].ToString() + " Hz, Ampl main" + sig.signals[num, 2].ToString() + " V, Freq mod " + sig.signals[num, 3].ToString() + " Hz, Dur " + sig.signals[num, 4].ToString() + " ms";
            }
            listBox1.Items.Insert(num, tape);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int type = 0;                            // обычный сигнал или ФМ
            if (checkBox1.Checked == true)
            {
                type = 1;
            }
                       
            int param1 = Convert.ToInt32(textBox1.Text);
            int param2 = Convert.ToInt32(textBox2.Text);
            int param3 = Convert.ToInt32(textBox3.Text);
            int duration = Convert.ToInt32(textBox4.Text);

            sig.addSignal(sig.number,type, param1, param2, param3, duration);
            
            addSignal(sig.number);
            
            sig.number++;
            
        }

        private void button2_Click(object sender, EventArgs e)     // запуск процедуры
        {
            button1.Enabled = false;
            groupBox1.Enabled = false;
            label6.Visible = true;
            listBox2.Visible = true;
            progressBar1.Visible = true;
            button3.Enabled = false;


            tradeSignal(sig.number);
            sig.procDuration();
            progressBar1.Maximum = sig.procduration;
            Thread TTime = new Thread(TimeRem);
            Thread thread = new Thread(RunProc);
            thread.Start();
            TTime.Start();
           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            sig.signalClean();
            listBox1.Items.Clear();
            
        }
    }
}
