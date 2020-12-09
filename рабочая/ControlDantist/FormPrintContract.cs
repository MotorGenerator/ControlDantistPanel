﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControlDantist.ReceptionDocuments;
using ControlDantist.BalanceContract;

namespace ControlDantist
{
    public partial class FormPrintContract : Form
    {

        public Registr Registr { get; set; }
       

        public FormPrintContract()
        {
            InitializeComponent();
        }

        private void FormPrintContract_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.Registr.GetDisplayContract();

            this.dataGridView1.Columns["NumberContract"].Width = 150;
            this.dataGridView1.Columns["NumberContract"].DisplayIndex = 0;
            this.dataGridView1.Columns["NumberContract"].HeaderText = "Номер проекта договора";

            this.dataGridView1.Columns["SummContract"].Width = 150;
            this.dataGridView1.Columns["SummContract"].DisplayIndex = 1;
            this.dataGridView1.Columns["SummContract"].HeaderText = "Сумма проекта договора";

            this.dataGridView1.Columns["FioPerson"].Width = 250;
            this.dataGridView1.Columns["FioPerson"].DisplayIndex = 2;
            this.dataGridView1.Columns["FioPerson"].HeaderText = "ФИО льготника";

            this.dataGridView1.Columns["FlagRegion"].Width = 80;
            this.dataGridView1.Columns["FlagRegion"].DisplayIndex = 3;
            this.dataGridView1.Columns["FlagRegion"].HeaderText = "Район области";

            // Кооличество  договоров в реестре.
            this.label1.Text = "Количество проектов договоров - " + Registr.GetCountContracts().ToString();

            // Сумма реестра проектов договоров.
            this.label2.Text = "Сумма реестра проектов договоров - " + Registr.SumContracts().ToString("c");

            // Выведим льготную категорию.
            this.label3.Text = Registr.GetPrivelegetCategory();

            decimal balance = 0.0m;

            if(balance == 0.0m)
            {
                this.labelLimit.Text = "";
            }

            /*

            // Баланс денежных средств.
            BalanceDisplay bc = new BalanceDisplay();

            

            // Количество израсходованных денежных средств.
            decimal limitMoney = bc.GetLimitPreferentyCategory(DateTime.Today.Year,this.Registr.GetPrivelegetCategory());// 600000;

            // Количество денежных средств с ранее израсходованные + сумма реестра проектов договоров.
            IPrintBalance printBalance = new PrintBalance(limitMoney, Registr.SumContracts());

            // Получим текущий год.
            int year = DateTime.Now.Year; 

            // Предполагаемый расход денежных средств с учетеом текущео реестра проектов договоров.
            var balanceRegistr = printBalance.PrintBalance();

            // Остаток денежных средств.
            ReisedueMoney rm = new ReisedueMoney(Registr.GetPrivelegetCategory(),year );
            decimal limitMoneySum = rm.PrintBalance();

            decimal balance = Math.Round(limitMoneySum - balanceRegistr, 4);

            if(balance < 0)
            {
                this.labelLimit.Text = "Лимит исчерпан = " + balance.ToString("c");
                this.labelLimit.ForeColor = Color.Red;
            }
            else
            {
                this.labelLimit.Text = "Лимит средств = " + balance.ToString("c");
            }

            if(this.Registr.FlagErrorRegionRegistr == true)
            {
                this.LabelError.Visible = true;
                this.btnYes.Enabled = false;
            }

            */

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            //var color = this.dataGridView1.CurrentRow.DefaultCellStyle.BackColor;

            ////if (this.dataGridView1.CurrentRow.DefaultCellStyle.BackColor != Color.BlueViolet)
            //if (color != Color.BlueViolet)
            //{
            //    this.dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.BlueViolet;
            //}
            //else
            //{
            //    this.dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.FromArgb(0, 0, 0, 0).;
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {

        }
    }
}
