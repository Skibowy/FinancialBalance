using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilansPieniezny
{
    public partial class BilansPieniezny : Form
    {
        //15945,27
        decimal accountBalance;
        decimal savingsBalance;
        decimal cryptoBalance;
        decimal owedToMe;
        decimal myDebt;
        decimal creditCardAccountBalance;
        decimal loanAmount;
        decimal secondLoanAmount;
        decimal creditCardLimit;
        List<CheckBox> checkBoxesSecond = new List<CheckBox>();
        List<CheckBox> checkBoxesFirst = new List<CheckBox>();
        string filePath;
        public BilansPieniezny()
        {
            InitializeComponent();
            InitializeLists();
            using (SettingsForm settingsForm = new SettingsForm())
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    filePath = settingsForm.filePath;
                    creditCardLimit = settingsForm.creditCardLimit;
                    readFile();
                }
            }
        }

        private void btnSetLoanAmounts_Click(object sender, EventArgs e)
        {
            foreach (var item in checkBoxesFirst)
            {
                item.Text = txtLoanAmount.Text;
            }
            foreach (var item in checkBoxesSecond)
            {
                item.Text = txtSecondLoanAmount.Text;
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            accountBalance = decimal.Parse(txtAccountBalance.Text);
            savingsBalance = decimal.Parse(txtSavingsBalance.Text);
            cryptoBalance = decimal.Parse(txtCryptoBalance.Text);
            owedToMe = decimal.Parse(txtOwedToMe.Text);
            myDebt = decimal.Parse(txtMyDebt.Text);
            creditCardAccountBalance = decimal.Parse(txtCreditCardAccountBalance.Text);
            loanAmount = decimal.Parse(txtLoanAmount.Text);
            secondLoanAmount = decimal.Parse(txtSecondLoanAmount.Text);

            int firstLoanCount = checkBoxesFirst.Count(cb => cb.Checked);
            decimal firstLoanSum = (decimal)firstLoanCount * decimal.Parse(txtLoanAmount.Text);
            int secondLoanCount = checkBoxesSecond.Count(cb => cb.Checked);
            decimal secondLoanSum = (decimal)secondLoanCount * decimal.Parse(txtSecondLoanAmount.Text);
            decimal loanSum = firstLoanSum + secondLoanSum;

            decimal finalBalance = accountBalance + savingsBalance + cryptoBalance + owedToMe - myDebt - (creditCardLimit - creditCardAccountBalance) - loanSum;
            listBoxHistory.Items.Add($"Nowy bilans: {finalBalance:C}");
            writeFile(filePath, accountBalance, savingsBalance, cryptoBalance, owedToMe, myDebt, creditCardAccountBalance, loanAmount, secondLoanAmount);
        }

        private void readFile()
        {
            try
            {
                string[] financeData = File.ReadAllLines(filePath);
                if (financeData.Length >= 4)
                {
                    accountBalance = decimal.Parse(financeData[0]);
                    savingsBalance = decimal.Parse(financeData[1]);
                    cryptoBalance = decimal.Parse(financeData[2]);
                    owedToMe = decimal.Parse(financeData[3]);
                    myDebt = decimal.Parse(financeData[4]);
                    creditCardAccountBalance = decimal.Parse(financeData[5]);
                    loanAmount = decimal.Parse(financeData[6]);
                    secondLoanAmount = decimal.Parse(financeData[7]);

                    txtAccountBalance.Text = accountBalance.ToString();
                    txtSavingsBalance.Text = savingsBalance.ToString();
                    txtCryptoBalance.Text = cryptoBalance.ToString();
                    txtOwedToMe.Text = owedToMe.ToString();
                    txtMyDebt.Text = myDebt.ToString();
                    txtCreditCardAccountBalance.Text = creditCardAccountBalance.ToString();
                    txtLoanAmount.Text = loanAmount.ToString();
                    txtSecondLoanAmount.Text = secondLoanAmount.ToString();
                    foreach (var item in checkBoxesFirst)
                    {
                        item.Text = txtLoanAmount.Text;
                    }
                    foreach (var item in checkBoxesSecond)
                    {
                        item.Text = txtSecondLoanAmount.Text;
                    }
                }
                else
                {
                    MessageBox.Show("Plik nie zawiera wystarczających informacji!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        private void writeFile(string filePath, decimal accountBalance, decimal savingsBalance, decimal cryptoBalance, decimal owedToMe, decimal myDebt, decimal creditCardAccountBalance, decimal loanAmount, decimal secondLoanAmount)
        {
            string[] financeData = new string[]
            {
                accountBalance.ToString(),
                savingsBalance.ToString(),
                cryptoBalance.ToString(),
                owedToMe.ToString(),
                myDebt.ToString(),
                creditCardAccountBalance.ToString(),
                loanAmount.ToString(),
                secondLoanAmount.ToString()
            };
            try
            {
                File.WriteAllLines(filePath, financeData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        private void InitializeLists()
        {
            txtLoanAmount.Text = "0,00";
            checkBoxesFirst.Add(cbxLoan1);
            checkBoxesFirst.Add(cbxLoan2);
            checkBoxesFirst.Add(cbxLoan3);
            checkBoxesFirst.Add(cbxLoan4);
            checkBoxesFirst.Add(cbxLoan5);
            checkBoxesFirst.Add(cbxLoan6);
            checkBoxesFirst.Add(cbxLoan7);
            checkBoxesFirst.Add(cbxLoan8);
            checkBoxesFirst.Add(cbxLoan9);
            checkBoxesFirst.Add(cbxLoan10);
            foreach (var item in checkBoxesFirst)
            {
                item.Text = txtLoanAmount.Text;
            }

            txtSecondLoanAmount.Text = "0,00";
            checkBoxesSecond.Add(cbxSecondLoan1);
            checkBoxesSecond.Add(cbxSecondLoan2);
            checkBoxesSecond.Add(cbxSecondLoan3);
            checkBoxesSecond.Add(cbxSecondLoan4);
            checkBoxesSecond.Add(cbxSecondLoan5);
            checkBoxesSecond.Add(cbxSecondLoan6);
            checkBoxesSecond.Add(cbxSecondLoan7);
            checkBoxesSecond.Add(cbxSecondLoan8);
            checkBoxesSecond.Add(cbxSecondLoan9);
            checkBoxesSecond.Add(cbxSecondLoan10);
            foreach (var item in checkBoxesSecond)
            {
                item.Text = txtSecondLoanAmount.Text;
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm())
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    filePath = settingsForm.filePath;
                    creditCardLimit = settingsForm.creditCardLimit;
                    readFile();
                }
            }
        }
    }
}
